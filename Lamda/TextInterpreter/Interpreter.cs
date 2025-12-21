using Lamda.AST;

namespace Lamda.TextInterpreter;

public class Interpreter
{
    public Node Interpret(string lamda)
    {
        lamda = lamda.Trim();
        if (lamda[0] == 'λ') return InterpretDefinition(lamda);
        if (!lamda.Contains('(') && !lamda.Contains(' ')) return InterpretLiteral(lamda);
        return InterpretCall(lamda);
    }

    private Node InterpretLiteral(string lamda)
    {
        return new Literal(lamda);
    }

    private Node InterpretDefinition(string lamda)
    {
        int dotLocation = lamda.IndexOf('.');
        return new Definition(
            lamda.Substring(1, dotLocation - 1),
            Interpret(lamda.Substring(dotLocation + 1, lamda.Length - dotLocation - 1)));
    }

    private string[] GetParts(string lamda)
    {
        List<string> parts = [];
        while (lamda.Length > 0)
        {
            lamda = ExtractPart(lamda, parts);
        }
        return parts.ToArray();
    }

    private string ExtractPart(string lamda, List<string> parts)
    {
        string part;
        part = lamda.Substring(0, Math.Min(EndOfPart(lamda) + 1, lamda.Length));
        lamda = lamda.Remove(0, part.Length);
        if (part.Trim() != "") parts.Add(part);
        return lamda;
    }

    private int EndOfPart(string call)
    {
        bool definition = call[0] == 'λ';
        int indent = 0;
        for (int i = 0; i < call.Length; i++)
        {
            if (call[i] == '(')
            {
                if (i != 0 && indent == 0 && !definition) return i - 1;
                indent++;
            }
            if (call[i] == ')')
            {
                indent--;
                if (indent == 0) return i;
            }
            if (indent == 0 && call[i] == ' ') return i;
        }
        return call.Length;
    }
    
    private Node InterpretCall(string lamda)
    {
        int endOfSpacing = EndOfPart(lamda);
        if (endOfSpacing == lamda.Length - 1) return Interpret(lamda.Substring(1, lamda.Length - 2));
        
        string[] parts = GetParts(lamda);
        Node node = new Call(Interpret(parts[0]), Interpret(parts[1]));
        for (int i = 2; i < parts.Length; i++)
        {
            node = new Call(node, Interpret(parts[i]));
        }
        return node;
    }
}