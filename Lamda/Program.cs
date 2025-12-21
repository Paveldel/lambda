using System.Text;
using Lamda.AST;
using Lamda.TextInterpreter;
using Lamda.Translator;

Console.OutputEncoding = Encoding.UTF8;

string expression = "((λm.λn.λf.λx.m f (n f x)) (λf.λx.f (f x)) (λf.λx.f (f (f x))))";

Node tree = new Interpreter().Interpret(expression);
for (int k = 0; k < 5; k++)
{
    string betweenResult = new Translator().Translate(tree);
    Console.WriteLine(betweenResult);
    tree = tree.Reduce();
}
string result = new Translator().Translate(tree);
Console.WriteLine(result);