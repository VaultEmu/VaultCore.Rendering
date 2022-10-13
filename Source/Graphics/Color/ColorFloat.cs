namespace VaultCore.Rendering;

//Representation of RGBA colors in float fomat
public struct Color
{
    public float R;
    public float G;
    public float B;
    public float A;
    
    public Color(float r, float g, float b)
    {
        R = r;
        G = g;
        B = b;
        A = 255;
    }
    
    public Color(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}