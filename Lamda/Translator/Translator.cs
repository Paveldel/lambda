using Lamda.AST;

namespace Lamda.Translator;

public class Translator
{
    public string Translate(Node node)
    {
        return node switch
        {
            Literal literal => literal.Name,
            Definition definition => $"λ{definition.Parameter}.{Translate(definition.Body)}",
            Call call => $"({Translate(call.Function)} {Translate(call.Value)})"
        };
    }
}