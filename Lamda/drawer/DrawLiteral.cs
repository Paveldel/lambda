using System.Drawing;

namespace Lamda.drawer;

public class DrawLiteral(string literal) : Drawable
{
    public void Draw(Bitmap canvas, (int x, int y) offset, Dictionary<string, int> parameters)
    {
        int toY = parameters[literal];
        canvas.DrawVertical(offset.x + 1, offset.y, offset.y - toY);
    }

    public (int x, int y) GetBoundingBox()
    {
        return (3, 0);
    }
}