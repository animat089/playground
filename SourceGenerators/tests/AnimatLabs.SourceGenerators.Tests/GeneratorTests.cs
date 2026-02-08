using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnimatLabs.SourceGenerators;
using AnimatLabs.SourceGenerators.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace AnimatLabs.SourceGenerators.Tests;

public class GeneratorTests
{
    [Fact]
    public void AutoToString_GeneratesOverride()
    {
        var source = """
            using AnimatLabs.SourceGenerators.Attributes;
            namespace Demo;
            [AutoToString]
            public partial class Person
            {
                public string Name { get; set; } = string.Empty;
            }
            """;

        var output = RunGenerator(source);

        Assert.Contains("public override string ToString()", output, StringComparison.Ordinal);
        Assert.Contains("Name =", output, StringComparison.Ordinal);
    }

    [Fact]
    public void EnumExtensions_GenerateDisplayHelpers()
    {
        var source = """
            using System.ComponentModel.DataAnnotations;
            using AnimatLabs.SourceGenerators.Attributes;
            namespace Demo;
            [GenerateEnumExtensions]
            public enum Status
            {
                [Display(Name = "In Progress")]
                Processing,
                Done
            }
            """;

        var output = RunGenerator(source);

        Assert.Contains("ToDisplayName", output, StringComparison.Ordinal);
        Assert.Contains("TryParse", output, StringComparison.Ordinal);
    }

    [Fact]
    public void Mapper_GeneratesAssignments()
    {
        var source = """
            using AnimatLabs.SourceGenerators.Attributes;
            namespace Demo;
            public class User
            {
                public int Id { get; set; }
                public string Name { get; set; } = string.Empty;
            }
            public class UserDto
            {
                public int Id { get; set; }
                public string Name { get; set; } = string.Empty;
            }
            [GenerateMapper]
            public partial class UserMapper
            {
                public partial UserDto ToDto(User entity);
            }
            """;

        var output = RunGenerator(source);

        Assert.Contains("target.Id =", output, StringComparison.Ordinal);
        Assert.Contains("target.Name =", output, StringComparison.Ordinal);
    }

    [Fact]
    public void Configuration_GeneratesBindMethod()
    {
        var source = """
            using AnimatLabs.SourceGenerators.Attributes;
            namespace Demo;
            [GenerateConfiguration(SectionName = "Database")]
            public partial class Settings
            {
                public string ConnectionString { get; set; } = string.Empty;
                public int Timeout { get; set; }
            }
            """;

        var output = RunGenerator(source);

        Assert.Contains("public static", output, StringComparison.Ordinal);
        Assert.Contains("Bind", output, StringComparison.Ordinal);
        Assert.Contains("Database", output, StringComparison.Ordinal);
    }

    [Fact]
    public void T4_AutoToString_GeneratedFileExists()
    {
        var content = ReadT4GeneratedFile("Person.ToString.cs");

        Assert.Contains("public override string ToString()", content, StringComparison.Ordinal);
        Assert.Contains("Person {", content, StringComparison.Ordinal);
    }

    [Fact]
    public void T4_EnumExtensions_GeneratedFileExists()
    {
        var content = ReadT4GeneratedFile("OrderStatus.Extensions.cs");

        Assert.Contains("public static class OrderStatusExtensions", content, StringComparison.Ordinal);
        Assert.Contains("ToDisplayName", content, StringComparison.Ordinal);
        Assert.Contains("TryParse", content, StringComparison.Ordinal);
    }

    [Fact]
    public void T4_Mapper_GeneratedFileExists()
    {
        var content = ReadT4GeneratedFile("UserMapper.cs");

        Assert.Contains("public partial class UserMapper", content, StringComparison.Ordinal);
        Assert.Contains("target.Id", content, StringComparison.Ordinal);
        Assert.Contains("target.Email", content, StringComparison.Ordinal);
    }

    [Fact]
    public void T4_ConfigurationBinder_GeneratedFileExists()
    {
        var content = ReadT4GeneratedFile("DatabaseSettings.Binder.cs");

        Assert.Contains("public static DatabaseSettings Bind", content, StringComparison.Ordinal);
        Assert.Contains("BindRetry", content, StringComparison.Ordinal);
        Assert.Contains("Database", content, StringComparison.Ordinal);
    }

    private static string RunGenerator(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Latest));
        var references = GetMetadataReferences();

        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var generator = new AnimatLabsSourceGenerators();
        var driver = CSharpGeneratorDriver.Create(generator);

        driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out _);

        return string.Join("\n", outputCompilation.SyntaxTrees
            .Where(tree => tree.FilePath.EndsWith(".g.cs", StringComparison.OrdinalIgnoreCase))
            .Select(tree => tree.ToString()));
    }

    private static IEnumerable<MetadataReference> GetMetadataReferences()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !assembly.IsDynamic)
            .Select(assembly => assembly.Location)
            .Where(location => !string.IsNullOrWhiteSpace(location))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        assemblies.Add(typeof(AutoToStringAttribute).Assembly.Location);
        assemblies.Add(typeof(Microsoft.Extensions.Configuration.IConfiguration).Assembly.Location);

        return assemblies.Select(location => MetadataReference.CreateFromFile(location));
    }

    private static string ReadT4GeneratedFile(string fileName)
    {
        var root = FindSolutionRoot();
        var filePath = Path.Combine(root, "tools", "AnimatLabs.SourceGenerators.T4", "Generated", fileName);

        Assert.True(File.Exists(filePath), $"Missing generated T4 file: {filePath}");

        return File.ReadAllText(filePath);
    }

    private static string FindSolutionRoot()
    {
        var current = new DirectoryInfo(AppContext.BaseDirectory);
        while (current is not null)
        {
            var solutionPath = Path.Combine(current.FullName, "AnimatLabs.SourceGenerators.sln");
            if (File.Exists(solutionPath))
            {
                return current.FullName;
            }

            current = current.Parent;
        }

        throw new DirectoryNotFoundException("Unable to locate AnimatLabs.SourceGenerators.sln");
    }
}
