namespace VaultCore.Rendering;

//Represents a rectangle defined by its width, height and position
public struct Rect
{
    public uint X;
    public uint Y;
    public uint Width;
    public uint Height;

    public Rect(uint x, uint y, uint width, uint height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
}