using System.Text;
using Lamda.AST;
using Lamda.drawer;
using Lamda.TextInterpreter;
using Lamda.Translator;

Console.OutputEncoding = Encoding.UTF8;

string expression = "λn.λf.n(λc.λa.λb.c b(λx.a (b x)))(λx.λy.x)(λx.x)f";

Node tree = new Interpreter().Interpret(expression);
new Drawer().Draw(tree);
for (int k = 0; k < 5; k++)
{
    string betweenResult = new Translator().Translate(tree);
    Console.WriteLine(betweenResult);
    tree = tree.Reduce();
}
string result = new Translator().Translate(tree);
Console.WriteLine(result);