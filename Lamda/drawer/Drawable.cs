using System.Drawing;

namespace Lamda.drawer;

public interface Drawable
{
    void Draw(Bitmap canvas, (int x, int y) offset, Dictionary<string, int> parameters);
    (int x, int y) GetBoundingBox();
}