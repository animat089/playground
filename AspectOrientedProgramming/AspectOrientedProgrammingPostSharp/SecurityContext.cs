using AspectOrientedProgrammingPostSharp.Models;

namespace AspectOrientedProgrammingPostSharp;

public static class SecurityContext
{
    public static User? CurrentUser { get; set; }
}
