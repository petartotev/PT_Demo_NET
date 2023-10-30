using System.Runtime.CompilerServices;

namespace NET8;

public class Caller
{
    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "ExamplePrivateMethod")]
    public static extern string GetExampleMethod(Example example);

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_exampleField")]
    public static extern ref string GetExampleField(Example example);
}
