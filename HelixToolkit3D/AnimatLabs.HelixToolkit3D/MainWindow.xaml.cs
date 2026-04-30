using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace AnimatLabs.HelixToolkit3D;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        AddShapes();
    }

    private void AddShapes()
    {
        var cube = new BoxVisual3D
        {
            Center = new Point3D(-3, 0, 0.5),
            Length = 1, Width = 1, Height = 1,
            Fill = new SolidColorBrush(Color.FromRgb(0x89, 0xb4, 0xfa))
        };

        var sphere = new SphereVisual3D
        {
            Center = new Point3D(0, 0, 0.5),
            Radius = 0.6,
            Fill = new SolidColorBrush(Color.FromRgb(0xa6, 0xe3, 0xa1))
        };

        var cone = new TruncatedConeVisual3D
        {
            Origin = new Point3D(3, 0, 0),
            BaseRadius = 0.6,
            TopRadius = 0,
            Height = 1.2,
            Fill = new SolidColorBrush(Color.FromRgb(0xf3, 0x8b, 0xa8))
        };

        var cylinder = new TruncatedConeVisual3D
        {
            Origin = new Point3D(-1.5, 3, 0),
            BaseRadius = 0.5,
            TopRadius = 0.5,
            Height = 1.2,
            Fill = new SolidColorBrush(Color.FromRgb(0xfa, 0xb3, 0x87))
        };

        var torus = new TorusVisual3D
        {
            TorusDiameter = 1.2,
            TubeDiameter = 0.35,
            Fill = new SolidColorBrush(Color.FromRgb(0xcb, 0xa6, 0xf7))
        };
        torus.Transform = new TranslateTransform3D(1.5, 3, 0.5);

        Viewport.Children.Add(cube);
        Viewport.Children.Add(sphere);
        Viewport.Children.Add(cone);
        Viewport.Children.Add(cylinder);
        Viewport.Children.Add(torus);
    }
}
