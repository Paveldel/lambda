using Lamda.AST;

namespace Lamda.TextInterpreter;

public class Interpreter
{
    public Node Interpret(string lamda)
    {
        return lamda[0] switch
        {
            '(' => InterpretCall(lamda),
            'λ' => InterpretDefinition(lamda),
            _ => InterpretLiteral(lamda)
        };
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
        int split = SpaceForCall(lamda);
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
        throw new Exception($"Couldn't interpret Call \"{call}\"");
    }
}