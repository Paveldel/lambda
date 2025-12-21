using System.Drawing;

namespace Lamda.drawer;

public class DrawDefinition(string parameter, Drawable body) : Drawable
{
    public void Draw(Bitmap canvas, (int x, int y) offset, Dictionary<string, int> parameters)
    {
        (int x, int y) = GetBoundingBox();
        parameters[parameter] = parameters.Count * 2;
        canvas.DrawHorizontal(offset.x, parameters[parameter], x);
        body.Draw(canvas, offset, parameters);
        parameters.Remove(parameter);
    }

    public (int x, int y) GetBoundingBox()
    {
        (int x, int y) box = body.GetBoundingBox();
        return (box.x, box.y + 2);
    }
}