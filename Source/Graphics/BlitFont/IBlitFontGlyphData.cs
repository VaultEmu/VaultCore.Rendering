namespace VaultCore.Rendering.Internal;

public interface IBlitFontGlyphData
{
    public int GlyphWidth { get; }
    public int GlyphHeight { get; }
    public int GlyphDescender { get; }
    public uint[] GlyphData { get; }
    public uint GetExtraBits(uint glyph);
}