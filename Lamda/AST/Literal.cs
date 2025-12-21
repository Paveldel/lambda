namespace Lamda.AST;

public class Literal(string name) : Node
{
    public string Name { get; set; } = name;


    public Node Clone()
    {
        return new Literal(Name);
    }

    public Node Reduce()
    {
        return this;
    }

    public void Bind(string parameter, Node value)
    {
        
    }
}