namespace Lamda.AST;

public class Call(Node function, Node value) : Node
{
    public Node Function { get; set; } = function;
    public Node Value { get; set; } = value;

    public Node Clone()
    {
        return new Call(Function.Clone(), Value.Clone());
    }

    public Node Reduce()
    {
        if (Function is Definition definition)
        {
            definition.Bind(definition.Parameter, Value);
            return definition.Body;
        }
        Function.Reduce();
        Value.Reduce();
        return this;
    }
    
    public void Bind(string parameter, Node value)
    {
        if (Function is Literal lit1 && lit1.Name == parameter) Function = value.Clone();
        else Function.Bind(parameter, value);
        if (Value is Literal lit2 && lit2.Name == parameter) Value = value.Clone();
        else Value.Bind(parameter, value);
    }
}