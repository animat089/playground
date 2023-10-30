using AutoMapper;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace Benchmarking.Mapping;

[MemoryDiagnoser(false)]
public class MappingSamples
{
    private static XmlDocument doc = new();
    private static root root;
    private static IMapper map;
    private static XslCompiledTransform xsltXml = new();
    private static XslCompiledTransform xsltText = new();
    private static XslCompiledTransform xsltJson = new(true);

    [GlobalSetup]
    public void SetupData()
    {
        // Load the original document
        var xmlReader = XmlReader.Create(new StringReader(File.ReadAllText("BaseFiles/Sample.xml")));
        doc.Load(xmlReader);

        // Setting up Automapper Configuration
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<root, Book>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.book.title))
                .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.book.publicationYear))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.book.author));

            cfg.CreateMap<root, Person>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.person.name))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.person.position))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.person.age))
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => new[] { src }));

            cfg.CreateMap<root, Company>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.company.name))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.company.city))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.company.state))
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => new[] { src }));

            cfg.CreateMap<root, Response>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src));

        });
        map = config.CreateMapper();

        // Since we cannot reset an xmlreader and we need to load the target
        xmlReader = XmlReader.Create(new StringReader(File.ReadAllText("BaseFiles/Sample.xml")));
        root = (root)new XmlSerializer(typeof(root)).Deserialize(xmlReader);

        xsltXml.Load("BaseFiles/ConvertXML.xslt");
        xsltText.Load("BaseFiles/ConvertText.xslt");
        xsltJson.Load("BaseFiles/ConvertJson.xslt");
    }

    [Benchmark]
    public void XmlDocToModelMapping()
    {
        var book = new Book()
        {
            Author = GetValue(doc, "/root/book/author"),
            PublicationYear = GetValue(doc, "/root/book/publicationYear"),
            Title = GetValue(doc, "/root/book/title"),
        };
        var person = new Person()
        {
            Age = Convert.ToInt32(GetValue(doc, "/root/person/age")),
            Name = GetValue(doc, "/root/person/name"),
            Position = GetValue(doc, "/root/person/position"),
            Books = new[] { book }
        };
        var company = new Company()
        {
            City = GetValue(doc, "/root/company/city"),
            Name = GetValue(doc, "/root/company/name"),
            State = GetValue(doc, "/root/company/state"),
            Employees = new[] { person }
        };
        var response = new Response()
        {
            Company = company
        };

        var output = JsonConvert.SerializeObject(response);

        PrintJson(output);
    }

    private string GetValue(XmlDocument xmlDocument, string path)
    {
        return xmlDocument.SelectSingleNode(path)?.InnerText;
    }

    [Benchmark]
    public void ModelToModelMapping()
    {
        var book = new Book()
        {
            Author = root.book.author,
            PublicationYear = root.book.publicationYear.ToString(),
            Title = root.book.title
        };
        var person = new Person()
        {
            Age = root.person.age,
            Name = root.person.name,
            Position = root.person.position,
            Books = new[] { book }
        };
        var company = new Company()
        {
            City = root.company.city,
            Name = root.company.name,
            State = root.company.state,
            Employees = new[] { person }
        };
        var response = new Response()
        {
            Company = company
        };

        var output = JsonConvert.SerializeObject(response);

        PrintJson(output);
    }

    [Benchmark]
    public void AutoMapperMapping()
    {
        var output = JsonConvert.SerializeObject(map.Map<Response>(root));

        PrintJson(output);
    }

    [Benchmark]
    public void XsltXMLMapping()
    {
        string output = String.Empty;
        using (StringWriter sw = new StringWriter())
        using (XmlWriter xwo = XmlWriter.Create(sw, xsltXml.OutputSettings))
        {
            xsltXml.Transform(doc, xwo);
            output = JsonConvert.SerializeXNode(XDocument.Parse(sw.ToString()));
        }
        
        PrintJson(output);
    }

    [Benchmark]
    public void XsltJsonMapping()
    {
        string output = String.Empty;
        using (StringWriter sw = new StringWriter())
        using (XmlWriter xwo = XmlWriter.Create(sw, xsltJson.OutputSettings))
        {
            xsltJson.Transform(doc, xwo);
            output = sw.ToString();
        }

        PrintJson(output);
    }

    [Benchmark]
    public void XsltTextMapping()
    {
        string output = String.Empty;
        using (StringWriter sw = new StringWriter())
        using (XmlWriter xwo = XmlWriter.Create(sw, xsltText.OutputSettings))
        {
            xsltText.Transform(doc, xwo);
            output = sw.ToString();
        }

        PrintJson(output);
    }

    private void PrintJson(string jsonString)
    {
        //Console.WriteLine(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(jsonString)));
    }
}
