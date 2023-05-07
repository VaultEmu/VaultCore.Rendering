namespace VaultCore.Rendering;

/// <summary>
/// How text that goes pass the end of the target buffer should be wrapped
/// </summary>
public enum WrapModes
{
    Clip,   //Stops printing
    Wrap,   //Wraps the next character to the next line
}

/// <summary>
/// Simple bitmap font where each character fits in a 3x5 area
/// </summary>
public class BlitFontSmall : Internal.BlitFont<Internal.BlitFontSmallGlyphData> { }

/// <summary>
/// Simple bitmap font where each character fits in a 5x6 area
/// </summary>
public class BlitFontLarge : Internal.BlitFont<Internal.BlitFontSmallGlyphData> { }