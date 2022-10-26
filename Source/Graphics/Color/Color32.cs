using System.Runtime.InteropServices;

namespace VaultCore.Rendering;

//Representation of RGBA colors in 32 bit format.
[StructLayout(LayoutKind.Explicit)]
public struct Color32
{
    [FieldOffset(0)]
    public byte R;
    
    [FieldOffset(1)]
    public byte G;
    
    [FieldOffset(2)]
    public byte B;
    
    [FieldOffset(3)]
    public byte A;
    
    //Creates a Color32 from rgb byte values
    public Color32(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
        A = 255;
    }
    
    //Creates a Color32 from rgba byte values
    public Color32(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
    
    //Creates a Color32 from rgb float values
    //Input components will be clamped between 0-1
    public Color32(float r, float g, float b)
    {
        R = (byte)(Math.Round(Math.Clamp(r, 0.0f, 1.0f) * 255.0f));
        G = (byte)(Math.Round(Math.Clamp(g, 0.0f, 1.0f) * 255.0f));
        B = (byte)(Math.Round(Math.Clamp(b, 0.0f, 1.0f) * 255.0f));
        A = 255;
    }
    
    //Creates a Color32 from rgba float values
    //Input components will be clamped between 0-1
    public Color32(float r, float g, float b, float a)
    {
        R = (byte)(Math.Round(Math.Clamp(r, 0.0f, 1.0f) * 255.0f));
        G = (byte)(Math.Round(Math.Clamp(g, 0.0f, 1.0f) * 255.0f));
        B = (byte)(Math.Round(Math.Clamp(b, 0.0f, 1.0f) * 255.0f));
        A = (byte)(Math.Round(Math.Clamp(a, 0.0f, 1.0f) * 255.0f));
    }
}