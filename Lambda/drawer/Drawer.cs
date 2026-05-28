using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Lamda.AST;

namespace Lamda.drawer;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public class Drawer
{
    public void Draw(Node tree)
    {
        Drawable drawable = CreateDrawTree(tree);
        (int x, int y) bounds = drawable.GetBoundingBox();
        bounds = (bounds.x, bounds.y);
        using Bitmap canvas = new(bounds.x, bounds.y);
        canvas.DrawVertical(1, bounds.y - 1, 2);
        drawable.Draw(canvas, (0, bounds.y - 2), []);
        canvas.Save("result.png");
    }

    private Drawable CreateDrawTree(Node tree)
    {
        return tree switch
        {
            Literal literal => new DrawLiteral(literal.Name),
            Definition definition => new DrawDefinition(definition.Parameter, CreateDrawTree(definition.Body)),
            Call call => new DrawCall(CreateDrawTree(call.Function), CreateDrawTree(call.Value)),
        };
    }
}

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public static class Extension
{
    public static void DrawVertical(this Bitmap canvas, int x, int y, int height)
    {
        for (int i = 0; i < height; i++)
        {
            canvas.SetPixel(x, y - i, Color.Black);
        }
    }
    
    public static void DrawHorizontal(this Bitmap canvas, int x, int y, int width)
    {
        for (int i = 0; i < width; i++)
        {
            canvas.SetPixel(x + i, y, Color.Black);
        }
    }
}