namespace Lamda.AST;

public class Definition(string parameter, Node body) : Node
{
    public string Parameter { get; set; } = parameter;
    public Node Body { get; set; } = body;


    public Node Clone()
    {
        return new Definition(Parameter, Body.Clone());
    }

    public Node Reduce()
    {
        Body.Reduce();
        return this;
    }

    public void Call(Node value)
    {
        if (Body is Literal lit && lit.Name == Parameter) Body = value.Clone();
        else Body.Bind(Parameter, value);
    }

    public void Bind(string parameter, Node value)
    {
        if (parameter == Parameter) return;
        if (Body is Literal lit && lit.Name == parameter) Body = value.Clone();
        else Body.Bind(parameter, value);
    }
}