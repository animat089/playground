using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace AnimatLabs.SourceGenerators;

[Generator]
public sealed class AnimatLabsSourceGenerators : IIncrementalGenerator
{
    private const string AutoToStringAttributeName = "AnimatLabs.SourceGenerators.Attributes.AutoToStringAttribute";
    private const string GenerateEnumExtensionsAttributeName = "AnimatLabs.SourceGenerators.Attributes.GenerateEnumExtensionsAttribute";
    private const string GenerateMapperAttributeName = "AnimatLabs.SourceGenerators.Attributes.GenerateMapperAttribute";
    private const string GenerateConfigurationAttributeName = "AnimatLabs.SourceGenerators.Attributes.GenerateConfigurationAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var autoToStringProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                AutoToStringAttributeName,
                static (node, _) => node is TypeDeclarationSyntax,
                static (ctx, _) => GetAutoToStringInfo(ctx))
            .Where(static info => info is not null);

        context.RegisterSourceOutput(autoToStringProvider, static (spc, info) =>
        {
            EmitAutoToString(spc, info!);
        });

        var enumExtensionsProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                GenerateEnumExtensionsAttributeName,
                static (node, _) => node is EnumDeclarationSyntax,
                static (ctx, _) => GetEnumInfo(ctx))
            .Where(static info => info is not null);

        context.RegisterSourceOutput(enumExtensionsProvider, static (spc, info) =>
        {
            EmitEnumExtensions(spc, info!);
        });

        var mapperProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                GenerateMapperAttributeName,
                static (node, _) => node is ClassDeclarationSyntax,
                static (ctx, _) => GetMapperInfo(ctx))
            .Where(static info => info is not null);

        context.RegisterSourceOutput(mapperProvider, static (spc, info) =>
        {
            EmitMapper(spc, info!);
        });

        var configurationProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                GenerateConfigurationAttributeName,
                static (node, _) => node is ClassDeclarationSyntax,
                static (ctx, _) => GetConfigurationInfo(ctx))
            .Where(static info => info is not null);

        context.RegisterSourceOutput(configurationProvider, static (spc, info) =>
        {
            EmitConfiguration(spc, info!);
        });
    }

    private static AutoToStringInfo? GetAutoToStringInfo(GeneratorAttributeSyntaxContext context)
    {
        if (context.TargetSymbol is not INamedTypeSymbol typeSymbol)
        {
            return null;
        }

        var includePrivate = false;
        var exclude = ImmutableHashSet<string>.Empty;

        foreach (var attribute in context.Attributes)
        {
            if (attribute.AttributeClass?.ToDisplayString() != AutoToStringAttributeName)
            {
                continue;
            }

            foreach (var namedArgument in attribute.NamedArguments)
            {
                if (namedArgument.Key == "IncludePrivate" && namedArgument.Value.Value is bool include)
                {
                    includePrivate = include;
                }

                if (namedArgument.Key == "Exclude")
                {
                    var excludedValues = new List<string>();
                    foreach (var value in namedArgument.Value.Values)
                    {
                        if (value.Value is string excluded)
                        {
                            excludedValues.Add(excluded);
                        }
                    }

                    exclude = excludedValues.ToImmutableHashSet(StringComparer.Ordinal);
                }
            }
        }

        var properties = typeSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => !p.IsStatic)
            .Where(p => includePrivate || p.DeclaredAccessibility == Accessibility.Public)
            .Where(p => p.GetMethod is not null)
            .Where(p => !exclude.Contains(p.Name))
            .Select(p => new PropertyInfo(p.Name, GetTypeName(p.Type)))
            .ToImmutableArray();

        return new AutoToStringInfo(
            typeSymbol.Name,
            GetNamespace(typeSymbol),
            typeSymbol.TypeKind == TypeKind.Struct,
            GetTypeParameters(typeSymbol),
            properties);
    }

    private static void EmitAutoToString(SourceProductionContext context, AutoToStringInfo info)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"namespace {info.Namespace};");
        builder.AppendLine();
        builder.AppendLine($"partial {(info.IsStruct ? "struct" : "class")} {info.Name}{info.TypeParameters}");
        builder.AppendLine("{");
        builder.AppendLine("    public override string ToString()");
        builder.AppendLine("    {");
        builder.AppendLine("        var builder = new global::System.Text.StringBuilder();");
        builder.AppendLine($"        builder.Append(\"{info.Name} {{ \");");

        for (var i = 0; i < info.Properties.Length; i++)
        {
            var property = info.Properties[i];
            var suffix = i < info.Properties.Length - 1 ? ", " : string.Empty;
            builder.AppendLine($"        builder.Append(\"{property.Name} = \").Append({property.Name}).Append(\"{suffix}\");");
        }

        builder.AppendLine("        builder.Append(\" }\");");
        builder.AppendLine("        return builder.ToString();");
        builder.AppendLine("    }");
        builder.AppendLine("}");

        context.AddSource($"{info.Name}.ToString.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
    }

    private static EnumInfo? GetEnumInfo(GeneratorAttributeSyntaxContext context)
    {
        if (context.TargetSymbol is not INamedTypeSymbol enumSymbol)
        {
            return null;
        }

        if (enumSymbol.TypeKind != TypeKind.Enum)
        {
            return null;
        }

        var members = enumSymbol.GetMembers()
            .OfType<IFieldSymbol>()
            .Where(field => field.HasConstantValue)
            .Select(field => new EnumMemberInfo(field.Name, GetDisplayName(field)))
            .ToImmutableArray();

        return new EnumInfo(enumSymbol.Name, GetNamespace(enumSymbol), GetTypeParameters(enumSymbol), members, GetTypeName(enumSymbol));
    }

    private static string GetDisplayName(IFieldSymbol field)
    {
        foreach (var attribute in field.GetAttributes())
        {
            if (attribute.AttributeClass?.ToDisplayString() != "System.ComponentModel.DataAnnotations.DisplayAttribute")
            {
                continue;
            }

            if (attribute.NamedArguments.FirstOrDefault(a => a.Key == "Name").Value.Value is string name)
            {
                return name;
            }
        }

        return field.Name;
    }

    private static void EmitEnumExtensions(SourceProductionContext context, EnumInfo info)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"namespace {info.Namespace};");
        builder.AppendLine();
        builder.AppendLine($"public static class {info.Name}Extensions");
        builder.AppendLine("{");
        builder.AppendLine($"    public static string ToDisplayName(this {info.FullyQualifiedTypeName} value) => value switch");
        builder.AppendLine("    {");

        foreach (var member in info.Members)
        {
            builder.AppendLine($"        {info.FullyQualifiedTypeName}.{member.Name} => \"{member.DisplayName}\",");
        }

        builder.AppendLine("        _ => value.ToString()"
            + ",");
        builder.AppendLine("    };");
        builder.AppendLine();
        builder.AppendLine($"    public static bool TryParse(string value, out {info.FullyQualifiedTypeName} result)");
        builder.AppendLine("    {");
        builder.AppendLine("        switch (value)");
        builder.AppendLine("        {");

        foreach (var member in info.Members)
        {
            if (member.DisplayName != member.Name)
            {
                builder.AppendLine($"            case \"{member.DisplayName}\":");
            }

            builder.AppendLine($"            case \"{member.Name}\":");
            builder.AppendLine($"                result = {info.FullyQualifiedTypeName}.{member.Name};");
            builder.AppendLine("                return true;");
        }

        builder.AppendLine("            default:");
        builder.AppendLine("                result = default;");
        builder.AppendLine("                return false;");
        builder.AppendLine("        }");
        builder.AppendLine("    }");
        builder.AppendLine();
        builder.AppendLine($"    public static global::System.Collections.Generic.IReadOnlyList<{info.FullyQualifiedTypeName}> GetAll() => new[]");
        builder.AppendLine("    {");

        foreach (var member in info.Members)
        {
            builder.AppendLine($"        {info.FullyQualifiedTypeName}.{member.Name},");
        }

        builder.AppendLine("    };");
        builder.AppendLine("}");

        context.AddSource($"{info.Name}.EnumExtensions.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
    }

    private static MapperInfo? GetMapperInfo(GeneratorAttributeSyntaxContext context)
    {
        if (context.TargetSymbol is not INamedTypeSymbol typeSymbol)
        {
            return null;
        }

        var methods = typeSymbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(method => method.IsPartialDefinition)
            .Where(method => method.Parameters.Length == 1)
            .Where(method => method.ReturnType.SpecialType != SpecialType.System_Void)
            .Select(method => CreateMapperMethod(method))
            .Where(method => method is not null)
            .ToImmutableArray();

        if (methods.Length == 0)
        {
            return null;
        }

        return new MapperInfo(
            typeSymbol.Name,
            GetNamespace(typeSymbol),
            GetTypeParameters(typeSymbol),
            methods!);
    }

    private static MapperMethodInfo? CreateMapperMethod(IMethodSymbol methodSymbol)
    {
        var sourceParameter = methodSymbol.Parameters[0];
        if (sourceParameter.Type is not INamedTypeSymbol sourceType)
        {
            return null;
        }

        if (methodSymbol.ReturnType is not INamedTypeSymbol targetType)
        {
            return null;
        }

        var targetProperties = targetType.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => !p.IsStatic)
            .Where(p => p.SetMethod is not null)
            .Where(p => p.DeclaredAccessibility == Accessibility.Public)
            .ToImmutableArray();

        var sourceProperties = sourceType.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => !p.IsStatic)
            .Where(p => p.GetMethod is not null)
            .ToDictionary(p => p.Name, p => p, StringComparer.Ordinal);

        var assignments = new List<PropertyAssignmentInfo>();
        foreach (var targetProperty in targetProperties)
        {
            if (!sourceProperties.TryGetValue(targetProperty.Name, out var sourceProperty))
            {
                continue;
            }

            if (!SymbolEqualityComparer.Default.Equals(targetProperty.Type, sourceProperty.Type))
            {
                continue;
            }

            assignments.Add(new PropertyAssignmentInfo(targetProperty.Name, sourceParameter.Name));
        }

        return new MapperMethodInfo(
            methodSymbol.Name,
            GetTypeName(targetType),
            GetTypeName(sourceType),
            sourceParameter.Name,
            assignments.ToImmutableArray());
    }

    private static void EmitMapper(SourceProductionContext context, MapperInfo info)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"namespace {info.Namespace};");
        builder.AppendLine();
        builder.AppendLine($"partial class {info.Name}{info.TypeParameters}");
        builder.AppendLine("{");

        foreach (var method in info.Methods)
        {
            builder.AppendLine($"    public partial {method.ReturnTypeName} {method.MethodName}({method.SourceTypeName} {method.SourceParameterName})");
            builder.AppendLine("    {");
            builder.AppendLine($"        if ({method.SourceParameterName} is null)");
            builder.AppendLine("        {");
            builder.AppendLine("            return null!;");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine($"        var target = new {method.ReturnTypeName}();");

            foreach (var assignment in method.Assignments)
            {
                builder.AppendLine($"        target.{assignment.TargetPropertyName} = {assignment.SourceParameterName}.{assignment.TargetPropertyName};");
            }

            builder.AppendLine("        return target;");
            builder.AppendLine("    }");
            builder.AppendLine();
        }

        builder.AppendLine("}");

        context.AddSource($"{info.Name}.Mapper.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
    }

    private static ConfigurationInfo? GetConfigurationInfo(GeneratorAttributeSyntaxContext context)
    {
        if (context.TargetSymbol is not INamedTypeSymbol typeSymbol)
        {
            return null;
        }

        var sectionName = typeSymbol.Name;
        foreach (var attribute in context.Attributes)
        {
            if (attribute.AttributeClass?.ToDisplayString() != GenerateConfigurationAttributeName)
            {
                continue;
            }

            foreach (var namedArgument in attribute.NamedArguments)
            {
                if (namedArgument.Key == "SectionName" && namedArgument.Value.Value is string name)
                {
                    sectionName = name;
                }
            }
        }

        var binder = CreateBinderModel(typeSymbol, sectionName);
        return new ConfigurationInfo(typeSymbol.Name, GetNamespace(typeSymbol), GetTypeParameters(typeSymbol), binder);
    }

    private static void EmitConfiguration(SourceProductionContext context, ConfigurationInfo info)
    {
        var typeQueue = new Queue<ConfigurationBinderModel>();
        var processed = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        var types = new List<ConfigurationBinderModel>();

        typeQueue.Enqueue(info.RootModel);

        while (typeQueue.Count > 0)
        {
            var current = typeQueue.Dequeue();
            if (!processed.Add(current.TypeSymbol))
            {
                continue;
            }

            types.Add(current);

            foreach (var property in current.Properties)
            {
                if (property.Kind == PropertyKind.Complex && property.ComplexType is not null)
                {
                    typeQueue.Enqueue(property.ComplexType);
                }
            }
        }

        var builder = new StringBuilder();
        builder.AppendLine($"namespace {info.Namespace};");
        builder.AppendLine();
        builder.AppendLine($"partial class {info.Name}{info.TypeParameters}");
        builder.AppendLine("{");
        builder.AppendLine($"    public static {GetTypeName(info.RootModel.TypeSymbol)} Bind(global::Microsoft.Extensions.Configuration.IConfiguration configuration)");
        builder.AppendLine("    {");
        builder.AppendLine("        if (configuration is null)");
        builder.AppendLine("        {");
        builder.AppendLine("            throw new global::System.ArgumentNullException(nameof(configuration));");
        builder.AppendLine("        }");
        builder.AppendLine();
        builder.AppendLine($"        var section = configuration.GetSection(\"{info.RootModel.SectionName}\");");
        builder.AppendLine($"        return {GetBindMethodName(info.RootModel.TypeSymbol)}(section);");
        builder.AppendLine("    }");
        builder.AppendLine();

        foreach (var model in types)
        {
            builder.AppendLine($"    private static {GetTypeName(model.TypeSymbol)} {GetBindMethodName(model.TypeSymbol)}(global::Microsoft.Extensions.Configuration.IConfiguration section)");
            builder.AppendLine("    {");
            builder.AppendLine($"        var result = new {GetTypeName(model.TypeSymbol)}();");

            foreach (var property in model.Properties)
            {
                switch (property.Kind)
                {
                    case PropertyKind.String:
                        builder.AppendLine($"        var {property.SafeLocalName} = section[\"{property.Name}\"]; ");
                        builder.AppendLine($"        if (!string.IsNullOrEmpty({property.SafeLocalName}))");
                        builder.AppendLine("        {");
                        builder.AppendLine($"            result.{property.Name} = {property.SafeLocalName}!;");
                        builder.AppendLine("        }");
                        break;
                    case PropertyKind.Int:
                        builder.AppendLine($"        if (int.TryParse(section[\"{property.Name}\"], out var {property.SafeLocalName}))");
                        builder.AppendLine("        {");
                        builder.AppendLine($"            result.{property.Name} = {property.SafeLocalName};");
                        builder.AppendLine("        }");
                        break;
                    case PropertyKind.Bool:
                        builder.AppendLine($"        if (bool.TryParse(section[\"{property.Name}\"], out var {property.SafeLocalName}))");
                        builder.AppendLine("        {");
                        builder.AppendLine($"            result.{property.Name} = {property.SafeLocalName};");
                        builder.AppendLine("        }");
                        break;
                    case PropertyKind.Double:
                        builder.AppendLine($"        if (double.TryParse(section[\"{property.Name}\"], NumberStyles.Float, CultureInfo.InvariantCulture, out var {property.SafeLocalName}))");
                        builder.AppendLine("        {");
                        builder.AppendLine($"            result.{property.Name} = {property.SafeLocalName};");
                        builder.AppendLine("        }");
                        break;
                    case PropertyKind.Decimal:
                        builder.AppendLine($"        if (decimal.TryParse(section[\"{property.Name}\"], NumberStyles.Number, CultureInfo.InvariantCulture, out var {property.SafeLocalName}))");
                        builder.AppendLine("        {");
                        builder.AppendLine($"            result.{property.Name} = {property.SafeLocalName};");
                        builder.AppendLine("        }");
                        break;
                    case PropertyKind.Enum:
                        builder.AppendLine($"        if (global::System.Enum.TryParse<{property.TypeName}>(section[\"{property.Name}\"], out var {property.SafeLocalName}))");
                        builder.AppendLine("        {");
                        builder.AppendLine($"            result.{property.Name} = {property.SafeLocalName};");
                        builder.AppendLine("        }");
                        break;
                    case PropertyKind.Complex:
                        builder.AppendLine($"        result.{property.Name} = {GetBindMethodName(property.ComplexType!.TypeSymbol)}(section.GetSection(\"{property.Name}\"));");
                        break;
                    case PropertyKind.Unsupported:
                        break;
                }
            }

            builder.AppendLine("        return result;");
            builder.AppendLine("    }");
            builder.AppendLine();
        }

        builder.AppendLine("}");

        context.AddSource($"{info.Name}.Configuration.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
    }

    private static string GetNamespace(INamedTypeSymbol typeSymbol)
    {
        return typeSymbol.ContainingNamespace?.ToDisplayString() ?? "";
    }

    private static string GetTypeName(ITypeSymbol typeSymbol)
    {
        return typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }

    private static string GetTypeParameters(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.TypeParameters.Length == 0)
        {
            return string.Empty;
        }

        var parameters = string.Join(", ", typeSymbol.TypeParameters.Select(p => p.Name));
        return $"<{parameters}>";
    }

    private static string GetBindMethodName(INamedTypeSymbol typeSymbol)
    {
        var sanitized = new string(typeSymbol.ToDisplayString().Select(ch => char.IsLetterOrDigit(ch) ? ch : '_').ToArray());
        return $"BindSection_{sanitized}";
    }

    private static ConfigurationBinderModel CreateBinderModel(INamedTypeSymbol typeSymbol, string sectionName)
    {
        var properties = typeSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => !p.IsStatic)
            .Where(p => p.SetMethod is not null)
            .Where(p => p.DeclaredAccessibility == Accessibility.Public)
            .Select(p => CreateBinderProperty(p))
            .ToImmutableArray();

        return new ConfigurationBinderModel(typeSymbol, sectionName, properties);
    }

    private static BinderPropertyInfo CreateBinderProperty(IPropertySymbol property)
    {
        var type = property.Type;
        var typeName = GetTypeName(type);
        var safeLocalName = "value" + property.Name;

        if (type.SpecialType == SpecialType.System_String)
        {
            return new BinderPropertyInfo(property.Name, typeName, PropertyKind.String, safeLocalName, null);
        }

        if (type.SpecialType == SpecialType.System_Int32)
        {
            return new BinderPropertyInfo(property.Name, typeName, PropertyKind.Int, safeLocalName, null);
        }

        if (type.SpecialType == SpecialType.System_Boolean)
        {
            return new BinderPropertyInfo(property.Name, typeName, PropertyKind.Bool, safeLocalName, null);
        }

        if (type.SpecialType == SpecialType.System_Double)
        {
            return new BinderPropertyInfo(property.Name, typeName, PropertyKind.Double, safeLocalName, null);
        }

        if (type.SpecialType == SpecialType.System_Decimal)
        {
            return new BinderPropertyInfo(property.Name, typeName, PropertyKind.Decimal, safeLocalName, null);
        }

        if (type.TypeKind == TypeKind.Enum)
        {
            return new BinderPropertyInfo(property.Name, typeName, PropertyKind.Enum, safeLocalName, null);
        }

        if (type is INamedTypeSymbol namedType && namedType.TypeKind == TypeKind.Class)
        {
            var nestedModel = CreateBinderModel(namedType, property.Name);
            return new BinderPropertyInfo(property.Name, typeName, PropertyKind.Complex, safeLocalName, nestedModel);
        }

        return new BinderPropertyInfo(property.Name, typeName, PropertyKind.Unsupported, safeLocalName, null);
    }

    private sealed record AutoToStringInfo(
        string Name,
        string Namespace,
        bool IsStruct,
        string TypeParameters,
        ImmutableArray<PropertyInfo> Properties);

    private sealed record PropertyInfo(string Name, string TypeName);

    private sealed record EnumInfo(
        string Name,
        string Namespace,
        string TypeParameters,
        ImmutableArray<EnumMemberInfo> Members,
        string FullyQualifiedTypeName);

    private sealed record EnumMemberInfo(string Name, string DisplayName);

    private sealed record MapperInfo(
        string Name,
        string Namespace,
        string TypeParameters,
        ImmutableArray<MapperMethodInfo> Methods);

    private sealed record MapperMethodInfo(
        string MethodName,
        string ReturnTypeName,
        string SourceTypeName,
        string SourceParameterName,
        ImmutableArray<PropertyAssignmentInfo> Assignments);

    private sealed record PropertyAssignmentInfo(string TargetPropertyName, string SourceParameterName);

    private sealed record ConfigurationInfo(
        string Name,
        string Namespace,
        string TypeParameters,
        ConfigurationBinderModel RootModel);

    private sealed record ConfigurationBinderModel(
        INamedTypeSymbol TypeSymbol,
        string SectionName,
        ImmutableArray<BinderPropertyInfo> Properties);

    private sealed record BinderPropertyInfo(
        string Name,
        string TypeName,
        PropertyKind Kind,
        string SafeLocalName,
        ConfigurationBinderModel? ComplexType);

    private enum PropertyKind
    {
        String,
        Int,
        Bool,
        Double,
        Decimal,
        Enum,
        Complex,
        Unsupported
    }
}
