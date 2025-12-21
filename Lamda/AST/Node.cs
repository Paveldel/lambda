namespace Lamda.AST;

public interface Node
{
    Node Clone();
    Node Reduce();
    void Bind(string parameter, Node value);
}