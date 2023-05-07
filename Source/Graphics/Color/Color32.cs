using System.Runtime.InteropServices;

namespace VaultCore.Rendering;

//Representation of RGBA colors in 32 bit format.
[StructLayout(LayoutKind.Explicit)]
public struct Color32
{
    /// <summary>
    /// White (255, 255, 255, 255)
    /// </summary>
    public static Color32 White => new Color32(255,255,255);
    
    /// <summary>
    /// Black (0, 0, 0, 255)
    /// </summary>
    public static Color32 Black => new Color32(0,0,0);
    
    /// <summary>
    /// Red (255, 0, 0, 255)
    /// </summary>
    public static Color32 Red => new Color32(255,0,0);
    
    /// <summary>
    /// Green (0, 255, 0, 255)
    /// </summary>
    public static Color32 Green => new Color32(0,255,0);
    
    /// <summary>
    /// Blue (0, 0, 255, 255)
    /// </summary>
    public static Color32 Blue => new Color32(0,0,255);
    
    /// <summary>
    /// Cyan (0, 255, 255, 255)
    /// </summary>
    public static Color32 Cyan => new Color32(0,255,255);
    
    /// <summary>
    /// Yellow (255, 255, 0, 255)
    /// </summary>
    public static Color32 Yellow => new Color32(255,255,0);
    
    /// <summary>
    /// Magenta (255, 0, 255, 255)
    /// </summary>
    public static Color32 Magenta => new Color32(255,0,255);
    
    /// <summary>
    /// Clear (0, 0, 0, 0)
    /// </summary>
    public static Color32 Clear => new Color32(0,0,0,0);
    
    /// <summary>
    /// Red color component
    /// </summary>
    [FieldOffset(0)]
    public byte R;
    
    /// <summary>
    /// Green Color Component
    /// </summary>
    [FieldOffset(1)]
    public byte G;
    
    /// <summary>
    /// Blue Color Component
    /// </summary>
    [FieldOffset(2)]
    public byte B;
    
    /// <summary>
    /// Alpha Color Component
    /// </summary>
    [FieldOffset(3)]
    public byte A;
    
    /// <summary>
    /// Creates a Color32 from rgba byte values
    /// </summary>
    /// <param name="r">r component (between 0 - 255)</param>
    /// <param name="g">g component (between 0 - 255)</param>
    /// <param name="b">b component (between 0 - 255)</param>
    public Color32(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
        A = 255;
    }
    
    /// <summary>
    /// Creates a Color32 from rgba byte values
    /// </summary>
    /// <param name="r">r component (between 0 - 255)</param>
    /// <param name="g">g component (between 0 - 255)</param>
    /// <param name="b">b component (between 0 - 255)</param>
    /// <param name="a">a component (between 0 - 255)</param>
    public Color32(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}