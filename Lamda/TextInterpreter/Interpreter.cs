using Lamda.AST;

namespace Lamda.TextInterpreter;

public class Interpreter
{
    public Node Interpret(string lamda)
    {
        if (lamda[0] == '(') return InterpretCall(lamda);
        if (lamda[0] == 'λ') return InterpretDefinition(lamda);
        if (lamda.Contains('(')) return InterpretCallWithParts(lamda);
        return InterpretLiteral(lamda);
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

    private Node InterpretCall(string lamda)
    {
        int endOfSpacing = EndOfPart(lamda);
        if (endOfSpacing != lamda.Length - 1) return InterpretCallWithParts(lamda);
        int split = SpaceForCall(lamda);
        if (split != -1) return InterpretCallWithSpace(lamda, split);
        lamda = lamda.Substring(1, lamda.Length - 2);
        return Interpret(lamda);
    }

    private Node InterpretCallWithParts(string lamda)
    {
        string[] parts = GetParts(lamda);
        Node node = new Call(Interpret(parts[0]), Interpret(parts[1]));
        for (int i = 2; i < parts.Length; i++)
        {
            node = new Call(node, Interpret(parts[i]));
        }
        return node;
    }

    private string[] GetParts(string lamda)
    {
        List<string> parts = [];
        while (lamda.Length > 0)
        {
            if (lamda[0] == '(')
            {
                int endOfPart = EndOfPart(lamda);
                string part = lamda.Substring(0, endOfPart + 1);
                lamda = lamda.Remove(0, part.Length);
                parts.Add(part);
            }
            else
            {
                int call = lamda.IndexOf('(');
                if (call == -1) call = lamda.Length;
                string part = lamda.Substring(0, call);
                lamda = lamda.Remove(0, part.Length);
                parts.Add(part);
            }
        }
        return parts.ToArray();
    }

    private Node InterpretCallWithSpace(string lamda, int split)
    {
        return new Call(
            Interpret(lamda.Substring(1, split - 1)),
            Interpret(lamda.Substring(split + 1, lamda.Length - split - 2)));
    }

    private int SpaceForCall(string call)
    {
        int subCalls = 0;
        for (int i = 1; i < call.Length; i++)
        {
            if (call[i] == '(') subCalls++;
            if (call[i] != ' ') continue;
            if (subCalls > 0) subCalls--;
            else return i;
        }
        return -1;
    }
    
    private int EndOfPart(string call)
    {
        int subPart = 1;
        for (int i = 1; i < call.Length; i++)
        {
            if (call[i] == '(') subPart++;
            if (call[i] == ')') subPart--;
            if (subPart == 0) return i;
        }
        return -1;
    }
}