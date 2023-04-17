namespace VaultCore.Rendering;

//Represents a rectangle defined by its width, height and position as floats
public struct RectFloat
{
    public float X;
    public float Y;
    public float Width;
    public float Height;

    public RectFloat(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
}