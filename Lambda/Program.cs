using System.Text;
using Lamda.AST;
using Lamda.drawer;
using Lamda.TextInterpreter;
using Lamda.Translator;

Console.OutputEncoding = Encoding.UTF8;

string expression = "(λc2.λc3.(λc4. c3 c4 (λn. n (λf.λb. b f (λx. x)) (λb.λf. c3 (b f)) c3) c4) (c2 c2)) (λf.λx. f (f x)) (λf.λx. f (f (f x)))";

Node tree = new Interpreter().Interpret(expression);
// for (int k = 0; k < 500; k++)
// {
//     tree = tree.Reduce();
// }
new Drawer().Draw(tree);
string result = new Translator().Translate(tree);
Console.WriteLine(result);