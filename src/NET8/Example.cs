namespace NET8;

public class Example
{
    private readonly string _exampleField = "example field";

    // The private property is compiled to 2 methods: set_ExampleProperty and get_ExampleProperty
    private string ExampleProperty { get; set; } = "Example Property default value";

    private string ExamplePrivateMethod()
    {
        return "private method response";
    }
}
