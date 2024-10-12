using PostSharp.Aspects;
using PostSharp.Serialization;

namespace AspectOrientedProgrammingPostSharp.Aspects;

[PSerializable]
public class CachingAspect : MethodInterceptionAspect
{
    private static readonly Dictionary<string, object> Cache = new Dictionary<string, object>();

    public override void OnInvoke(MethodInterceptionArgs args)
    {
        string key = $"{args.Method.Name}_{string.Join("_", args.Arguments)}";

        if (Cache.ContainsKey(key))
        {
            Console.WriteLine($"Returning cached result for {args.Method.Name}");
            args.ReturnValue = Cache[key];
        }
        else
        {
            base.OnInvoke(args);
            Cache[key] = args.ReturnValue;
            Console.WriteLine($"Caching result for {args.Method.Name}");
        }
    }
}
