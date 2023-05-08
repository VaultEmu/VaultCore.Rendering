namespace VaultCore.Rendering.Internal;

//Implementation based on https://github.com/azmr/blit-fonts
public abstract class BlitFont<TGlyphData> where TGlyphData : IBlitFontGlyphData, new()
{
    private TGlyphData _glyphData = new TGlyphData();
    
    private int GlyphDescender => _glyphData.GlyphDescender * FontScale;
    

    
    /// <summary>
    /// Width of each Glyph in the font
    /// </summary>
    public int GlyphWidth => _glyphData.GlyphWidth * FontScale;
    
    /// <summary>
    /// Height of each Glyph in the font
    /// </summary>
    public int GlyphHeight => _glyphData.GlyphHeight * FontScale;
    
    /// <summary>
    /// Simple Integer scaling of the pixel font into the target pixel data
    /// </summary>
    public int FontScale {get; set; } = 1;
    
    /// <summary>
    /// How many pixels are advanced when moving from the current character to the next
    /// </summary>
    public int GlyphAdvance => (_glyphData.GlyphWidth + 1) * FontScale;
    
    /// <summary>
    /// How many pixels are advanced when moving from the current line to the next
    /// </summary>
    public int RowAdvance => (_glyphData.GlyphHeight + _glyphData.GlyphDescender + 1) * FontScale;
    
    /// <summary>
    /// How to handle the text falling off the end of the buffer width
    /// </summary>
    public WrapModes WrapMode {get; set; } = WrapModes.Clip;
    
    /// <summary>
    /// Draw some text into the target pixel data buffer
    /// </summary>
    /// <param name="buffer">target pixel data buffer</param>
    /// <param name="color">color to draw the text</param>
    /// <param name="startX">the start x position in the buffer to start drawing</param>
    /// <param name="startY">the start y position in the buffer to start drawing</param>
    /// <param name="text">the text to render</param>
    /// <returns></returns>
    public int DrawText(PixelData buffer, Color32 color, int startX, int startY, string text)
    {
        int linesPrinted = 1;

        var x = startX;
        var y = startY;
		
        var wrap = WrapMode == WrapModes.Wrap;

        for(var index = 0; index < text.Length; ++index)
        {
            var currentChar = text[index];

            int endY = y + GlyphHeight + GlyphDescender;
			
            var BufOverflow  = endY >= buffer.Height || y >= buffer.Height;
            var BufXMinExceed = x < 0;
			
            int endX;
			
            if(currentChar == '\n' || currentChar == '\r')
            {
                endX = startX;
            }
            else if(currentChar == '\t')
            {
                endX = x + 4 * GlyphAdvance;
            }
            else if(currentChar == '\b')
            {
                endX = x - GlyphAdvance;
            }
            else
            {
                endX = x + GlyphAdvance;
            }

            var bufXMaxExceed = endX >= buffer.Width;

            if(BufOverflow)
            {
                /* no point adding extra undrawable lines */
                break;
            }
			
			
            if((!bufXMaxExceed || wrap) || currentChar == '\n')
            {
                if(bufXMaxExceed && currentChar != '\n')
                {
                    /* new line and redo on-screen checks */
                    currentChar = '\n'; 
                    endX = startX;
                    --index; 
					
                } 
                else if(BufXMinExceed)
                {
                    /* skip past character without drawing */
                    currentChar = ' ';
                }  
				
                switch(currentChar)
                {
                    case '\r':
                    case '\b': 
                    case '\t': 
                    case  ' ':
                        //Dont Print anything
                        break;

                    case '\n': 
                        /* new line */
                        y += RowAdvance;
                        ++linesPrinted;
                        break;

                    default:                                                
                    {
                        /* normal character */
                        if(currentChar < 32 || currentChar > 126)
                        {
                            //Unprintable character, switch to space
                            currentChar = ' ';
                        }
                        
                        var Glyph = _glyphData.GlyphData[currentChar - ' '];
                        var OffsetY = y + _glyphData.GetExtraBits(Glyph) * FontScale;

                        var RowStartIndex = (uint)(OffsetY * buffer.Width + x);
						
                        for(var glY = 0; glY < _glyphData.GlyphHeight; ++glY)
                        {
                            for(var pxY = FontScale; pxY > 0; pxY--)
                            {
                                var pixelIndex = RowStartIndex;
                                RowStartIndex += buffer.Width;
								
                                for(var glX = 0; glX < _glyphData.GlyphWidth; ++glX)
                                {
                                    var Shift = glY * _glyphData.GlyphWidth + glX;
                                    var drawPixel = ((Glyph >> Shift) & 1) != 0;
									
                                    if(drawPixel)
                                    {
                                        for(var pxX = FontScale; pxX > 0; pxX--)
                                        {
                                            buffer.SetPixel(color, pixelIndex);
                                            pixelIndex++;
                                        }
                                    }
                                    else
                                    {
                                        pixelIndex += (uint)FontScale;
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
				
                x = endX;
            }
        }
        return linesPrinted;
    }
}