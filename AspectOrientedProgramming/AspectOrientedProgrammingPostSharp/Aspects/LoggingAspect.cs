using PostSharp.Aspects;
using PostSharp.Serialization;

namespace AspectOrientedProgrammingPostSharp.Aspects;

[PSerializable]
public class LogAspect : OnMethodBoundaryAspect
{
    public override void OnEntry(MethodExecutionArgs args)
    {
        Console.WriteLine($"Starting method {args.Method.Name} with arguments: {string.Join(", ", args.Arguments)}");
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        Console.WriteLine($"Completed method {args.Method.Name}");
    }

    public override void OnException(MethodExecutionArgs args)
    {
        Console.WriteLine($"Exception in method {args.Method.Name}: {args.Exception.Message}");
    }
}
