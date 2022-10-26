using System.Runtime.InteropServices;

namespace VaultCore.Rendering;

//Representation of RGBA colors in float format
[StructLayout(LayoutKind.Explicit)]
public struct ColorFloat
{
    [FieldOffset(0)]
    public float R;
    
    [FieldOffset(4)]
    public float G;
    
    [FieldOffset(8)]
    public float B;
    
    [FieldOffset(12)]
    public float A;
    
    public ColorFloat(float r, float g, float b)
    {
        R = r;
        G = g;
        B = b;
        A = 1.0f;
    }
    
    public ColorFloat(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}