# Playground - AnimatLabs Code Samples

Welcome to the AnimatLabs playground repository! This is a collection of sample projects, experiments, and demonstrations for various .NET technologies and concepts shared on [animatlabs.com](https://animatlabs.com).

## 📂 Projects

### AllSoapBasedApis

Comprehensive examples demonstrating different SOAP-based web service implementations in .NET:

- **ASMX Services** - Traditional ASP.NET Web Services
- **WCF Services** - Windows Communication Foundation services
- **SoapCore** - Modern SOAP services using SoapCore in ASP.NET Core
- **CoreWCF** - Cross-platform WCF services for .NET Core
- Consumer application showing how to connect to all service types

### AspectOrientedProgramming

Demonstrates Aspect-Oriented Programming (AOP) concepts using PostSharp:

- Logging aspects
- Security aspects with role-based authorization
- Caching aspects for performance optimization

### Benchmarking

Performance benchmarking projects using BenchmarkDotNet (.NET 6.0):

- **Iterations** - Comparing different iteration patterns (for, foreach, LINQ, parallel) across arrays, lists, and enumerables
- **Mapping** - Performance comparison of various data mapping approaches:
  - XML document to model mapping
  - Model to model mapping
  - AutoMapper
  - XSLT transformations (XML, JSON, Text)

- See: [Benchmarking/readme.md](Benchmarking/readme.md)

### HashId.NET

Sample ASP.NET Core Web API demonstrating:

- Using HashidsNet library for ID obfuscation
- Secure ID encoding/decoding for user data
- RESTful API implementation with Swagger

### Microsoft.AspNetCore.DataProtection

Sample ASP.NET Core Web API demonstrating:

- Microsoft's Data Protection API for securing sensitive data
- Encryption and decryption of user information
- Best practices for data protection in ASP.NET Core

### ReactiveProgramming

Examples of reactive programming patterns using Rx.NET:

- Range sorting with reactive streams
- Folder watcher with file system monitoring
- Stock price tracker
- Temperature tracker

### Refit.ApiSdk

Complete API SDK implementation using Refit:

- RESTful API service
- Strongly-typed API client using Refit
- API contracts and models
- Consumer application
- Unit tests

### SourceGenerators

Source generator samples demonstrating:

- Defining custom attributes
- Generating code at compile time
- A small demo app showing the generated output

- See: [SourceGenerators/README.md](SourceGenerators/README.md)

### WorkflowForge

Scheduled workflows sample demonstrating a clean separation of responsibilities:

- **Coravel** handles **when** to run a job (scheduling + overlap prevention)
- **WorkflowForge** handles **what** to run (a multi-step workflow with shared state and automatic compensation)

The sample is “real-world shaped” on purpose: it includes multiple steps (fetch orders → charge payments → reserve inventory → send emails) so you can see the value of orchestration and rollback/compensation when failures occur.

- See: [WorkflowForge/README.md](WorkflowForge/README.md)

### HtmxWorkflowForge

Real-time workflow dashboard with **zero JavaScript frameworks**:

- **HTMX + Server-Sent Events** stream workflow step updates to the browser in real time
- **WorkflowForge 2.1.1** orchestrates a 5-step order processing workflow with automatic compensation
- Click "Run with Failure" to watch compensation cascade live

- See: [HtmxWorkflowForge/README.md](HtmxWorkflowForge/README.md)

### MassTransitWorkflowForge

The saga pattern with actual compensation code:

- **MassTransit** distributes messages (InMemory transport for easy demo, RabbitMQ config included)
- **WorkflowForge 2.1.1** runs a multi-step order saga with automatic rollback on failure
- Orders over $500 simulate payment failure to trigger compensation

- See: [MassTransitWorkflowForge/README.md](MassTransitWorkflowForge/README.md)

### SemanticKernelWorkflowForge

AI agent pipeline with rollback:

- **Semantic Kernel + Ollama** runs a 5-step content pipeline (classify, research, draft, quality check, publish)
- **WorkflowForge 2.1.1** compensates when the quality check rejects the output
- 100% local execution, zero cloud, zero cost

- See: [SemanticKernelWorkflowForge/README.md](SemanticKernelWorkflowForge/README.md)

### SpectreConsole

Beautiful CLI output with Spectre.Console:

- Tables, trees, bar charts, progress spinners, FigletText
- "Project Health Checker" demo showcasing all major Spectre features

- See: [SpectreConsole/README.md](SpectreConsole/README.md)

## 🎯 Purpose

This repository serves as:

- A learning resource for various .NET technologies
- Reference implementations for blog posts on animatlabs.com
- Experimental ground for new technologies and patterns
- Demonstration of best practices and performance comparisons

## 📝 License

Licensed under the Apache License 2.0 - see the [LICENSE](LICENSE) file for details.

## 🔗 Links

- Blog: [animatlabs.com](https://animatlabs.com)
- Each project folder may contain its own README with specific details
