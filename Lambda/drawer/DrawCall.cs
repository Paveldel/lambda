using System.Drawing;

namespace Lamda.drawer;

public class DrawCall(Drawable function, Drawable value) : Drawable
{
    public void Draw(Bitmap canvas, (int x, int y) offset, Dictionary<string, int> parameters)
    {
        (int fx, int fy) = function.GetBoundingBox();
        (_, int vy) = value.GetBoundingBox();
        
        canvas.DrawHorizontal(offset.x + 1, offset.y, fx + 2);
        canvas.DrawVertical(offset.x + 1, offset.y, 2 + Math.Max(0, vy - fy));
        canvas.DrawVertical(offset.x + 2 + fx, offset.y, 2 + Math.Max(0, fy - vy));
        
        function.Draw(canvas, (offset.x, offset.y - 2 - Math.Max(0, vy - fy)), parameters);
        value.Draw(canvas, (offset.x + fx + 1, offset.y - 2 - Math.Max(0, fy - vy)), parameters);
    }

    public (int x, int y) GetBoundingBox()
    {
        (int x, int y) funcBox = function.GetBoundingBox();
        (int x, int y) valBox = value.GetBoundingBox();
        return (funcBox.x + valBox.x + 1, Math.Max(funcBox.y, valBox.y) + 2);
    }
}