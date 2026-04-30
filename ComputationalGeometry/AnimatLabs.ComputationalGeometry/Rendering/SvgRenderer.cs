using System.Globalization;
using System.Text;
using NetTopologySuite.Geometries;

namespace AnimatLabs.ComputationalGeometry.Rendering;

public sealed class SvgRenderer
{
    private readonly StringBuilder _body = new();
    private double _minX = double.MaxValue, _minY = double.MaxValue;
    private double _maxX = double.MinValue, _maxY = double.MinValue;

    public void AddPolygon(Geometry geometry, string fill = "#4a90d9", string stroke = "#2c3e50", double opacity = 0.4)
    {
        if (geometry.IsEmpty) return;

        ExpandBounds(geometry.EnvelopeInternal);

        foreach (var coord in ExtractRings(geometry))
        {
            var points = string.Join(" ", coord.Select(c => $"{F(c.X)},{F(c.Y)}"));
            _body.AppendLine(
                $"  <polygon points=\"{points}\" fill=\"{fill}\" stroke=\"{stroke}\" " +
                $"stroke-width=\"1\" opacity=\"{F(opacity)}\" />");
        }
    }

    public void AddPoints(IEnumerable<Coordinate> coords, string fill = "#e74c3c", double radius = 3)
    {
        foreach (var c in coords)
        {
            ExpandBounds(c);
            _body.AppendLine(
                $"  <circle cx=\"{F(c.X)}\" cy=\"{F(c.Y)}\" r=\"{F(radius)}\" fill=\"{fill}\" />");
        }
    }

    public void AddLine(Coordinate from, Coordinate to, string stroke = "#2c3e50", double width = 1)
    {
        ExpandBounds(from);
        ExpandBounds(to);
        _body.AppendLine(
            $"  <line x1=\"{F(from.X)}\" y1=\"{F(from.Y)}\" x2=\"{F(to.X)}\" y2=\"{F(to.Y)}\" " +
            $"stroke=\"{stroke}\" stroke-width=\"{F(width)}\" />");
    }

    public void AddLabel(double x, double y, string text, string fill = "#333", int fontSize = 10)
    {
        ExpandBounds(new Coordinate(x, y));
        _body.AppendLine(
            $"  <text x=\"{F(x)}\" y=\"{F(y)}\" font-size=\"{fontSize}\" fill=\"{fill}\" " +
            $"font-family=\"monospace\">{Escape(text)}</text>");
    }

    public void AddLineString(Geometry lineString, string stroke = "#27ae60", double width = 2)
    {
        if (lineString.IsEmpty) return;
        ExpandBounds(lineString.EnvelopeInternal);

        var points = string.Join(" ", lineString.Coordinates.Select(c => $"{F(c.X)},{F(c.Y)}"));
        _body.AppendLine(
            $"  <polyline points=\"{points}\" fill=\"none\" stroke=\"{stroke}\" stroke-width=\"{F(width)}\" />");
    }

    public string Render()
    {
        const double pad = 20;
        double width = _maxX - _minX + pad * 2;
        double height = _maxY - _minY + pad * 2;

        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine($"<svg xmlns=\"http://www.w3.org/2000/svg\" " +
                       $"viewBox=\"{F(_minX - pad)} {F(_minY - pad)} {F(width)} {F(height)}\" " +
                       $"width=\"{F(Math.Min(width * 2, 800))}\" height=\"{F(Math.Min(height * 2, 600))}\">");
        sb.AppendLine("  <rect width=\"100%\" height=\"100%\" fill=\"#fafafa\" />");
        sb.Append(_body);
        sb.AppendLine("</svg>");
        return sb.ToString();
    }

    public void SaveTo(string path)
    {
        var dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);

        File.WriteAllText(path, Render());
    }

    private void ExpandBounds(Envelope env)
    {
        _minX = Math.Min(_minX, env.MinX);
        _minY = Math.Min(_minY, env.MinY);
        _maxX = Math.Max(_maxX, env.MaxX);
        _maxY = Math.Max(_maxY, env.MaxY);
    }

    private void ExpandBounds(Coordinate c)
    {
        _minX = Math.Min(_minX, c.X);
        _minY = Math.Min(_minY, c.Y);
        _maxX = Math.Max(_maxX, c.X);
        _maxY = Math.Max(_maxY, c.Y);
    }

    private static IEnumerable<Coordinate[]> ExtractRings(Geometry geometry)
    {
        if (geometry is Polygon poly)
        {
            yield return poly.ExteriorRing.Coordinates;
        }
        else if (geometry is MultiPolygon multi)
        {
            for (int i = 0; i < multi.NumGeometries; i++)
            {
                var p = (Polygon)multi.GetGeometryN(i);
                yield return p.ExteriorRing.Coordinates;
            }
        }
    }

    private static string F(double v) => v.ToString("F2", CultureInfo.InvariantCulture);

    private static string Escape(string s) =>
        s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
}
