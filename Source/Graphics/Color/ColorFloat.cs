using System.Runtime.InteropServices;

namespace VaultCore.Rendering;

//Representation of RGBA colors in float format
[StructLayout(LayoutKind.Explicit)]
public struct ColorFloat
{
    /// <summary>
    /// White (1.0f, 1.0f, 1.0f, 1.0f)
    /// </summary>
    public static ColorFloat White => new ColorFloat(1.0f,1.0f,1.0f);
    
    /// <summary>
    /// Black (0.0f, 0.0f, 0.0f, 1.0f)
    /// </summary>
    public static ColorFloat Black => new ColorFloat(0.0f,0.0f,0.0f);
    
    /// <summary>
    /// Red (1.0f, 0.0f, 0.0f, 1.0f)
    /// </summary>
    public static ColorFloat Red => new ColorFloat(1.0f,0.0f,0.0f);
    
    /// <summary>
    /// Green (0.0f, 1.0f, 0.0f, 1.0f)
    /// </summary>
    public static ColorFloat Green => new ColorFloat(0.0f,1.0f,0.0f);
    
    /// <summary>
    /// Blue (0.0f, 0.0f, 1.0f, 1.0f)
    /// </summary>
    public static ColorFloat Blue => new ColorFloat(0.0f,0.0f,1.0f);
    
    /// <summary>
    /// Cyan (0.0f, 1.0f, 1.0f, 1.0f)
    /// </summary>
    public static ColorFloat Cyan => new ColorFloat(0.0f,1.0f,1.0f);
    
    /// <summary>
    /// Yellow (1.0f, 1.0f, 0.0f, 1.0f)
    /// </summary>
    public static ColorFloat Yellow => new ColorFloat(1.0f,1.0f,0.0f);
    
    /// <summary>
    /// Magenta (1.0f, 0.0f, 1.0f, 1.0f)
    /// </summary>
    public static ColorFloat Magenta => new ColorFloat(1.0f,0.0f,1.0f);
    
    /// <summary>
    /// Clear (0, 0, 0, 0)
    /// </summary>
    public static ColorFloat Clear => new ColorFloat(0,0,0,0);
    
    /// <summary>
    /// Red color component
    /// </summary>
    [FieldOffset(0)]
    public float R;
    
    /// <summary>
    /// Green color component
    /// </summary>
    [FieldOffset(4)]
    public float G;
    
    /// <summary>
    /// Blue color component
    /// </summary>
    [FieldOffset(8)]
    public float B;
    
    /// <summary>
    /// Alpha color component
    /// </summary>
    [FieldOffset(12)]
    public float A;
    
    /// <summary>
    /// Creates a ColorFloat from rgba float values
    /// Input components will be clamped between 0-1
    /// </summary>
    /// <param name="r">r component (between 0 - 1)</param>
    /// <param name="g">g component (between 0 - 1)</param>
    /// <param name="b">b component (between 0 - 1)</param>
    public ColorFloat(float r, float g, float b)
    {
        R = r;
        G = g;
        B = b;
        A = 1.0f;
    }
    
    /// <summary>
    /// Creates a ColorFloat from rgba float values
    /// Input components will be clamped between 0-1
    /// </summary>
    /// <param name="r">r component (between 0 - 1)</param>
    /// <param name="g">g component (between 0 - 1)</param>
    /// <param name="b">b component (between 0 - 1)</param>
    /// <param name="a">a component (between 0 - 1)</param>
    public ColorFloat(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}