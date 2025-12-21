using System.Drawing;

namespace Lamda.drawer;

public class DrawDefinition(string parameter, Drawable body) : Drawable
{
    public void Draw(Bitmap canvas, (int x, int y) offset, Dictionary<string, int> parameters)
    {
        string replacement = Guid.NewGuid().ToString();
        if (parameters.ContainsKey(parameter))
        {
            parameters[replacement] = parameters[parameter];
            parameters.Remove(parameter);
        }
        
        (int x, _) = GetBoundingBox();
        parameters[parameter] = parameters.Count * 2;
        canvas.DrawHorizontal(offset.x, parameters[parameter], x);
        body.Draw(canvas, offset, parameters);
        parameters.Remove(parameter);
        
        if (!parameters.ContainsKey(replacement)) return;
        parameters[parameter] = parameters[replacement];
        parameters.Remove(replacement);
    }

    public (int x, int y) GetBoundingBox()
    {
        (int x, int y) box = body.GetBoundingBox();
        return (box.x, box.y + 2);
    }
}