using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VaultCore.Rendering;

/// <summary>
/// Factor to apply to source/destination colors when using SetPixelBlended
/// </summary>
public enum PixelBlendFactor
{
    Zero,                       //Multiplied the color by 0
    One,                        //Multiplies the color by 1 
    
    SourceColor,                //Multiplies each color component by the corresponding component in the source color
    OneMinusSourceColor,        //Multiplies each color component by 1 - the corresponding component in the source color
    SourceAlpha,                //Multiplies each color component by the source alpha component
    OneMinusSourceAlpha,        //Multiplies each color component by 1 - the source alpha component
    
    DestinationColor,           //Multiplies each color component by the corresponding component in the destination color
    OneMinusDestinationColor,   //Multiplies each color component by 1 - the corresponding component in the destination color
    DestinationAlpha,           //Multiplies each color component by the destination alpha component
    OneMinusDestinationAlpha    //Multiplies each color component by 1 - the destination alpha component
}

/// <summary>
/// Class wrapping around an array of color32 that represents the pixel data of some width and height
/// Data is defined as pixel 0,0 been in the top left corner with X going right and y going down
/// </summary>
public class PixelData
{
    private Color32[] _pixelDataArray = null!;
    private uint _width;
    private uint _height;
    
    /// <summary>
    /// Width of the Pixel Data
    /// </summary>
    public uint Width => _width;
    
    /// <summary>
    /// Height of the Pixel Data
    /// </summary>
    public uint Height => _height;
    
    /// <summary>
    /// Total Number of pixels in the pixel data
    /// </summary>
    public uint NumPixels => _width * _height;
    
    /// <summary>
    /// The raw Color32 data this PixelData wraps
    /// </summary>
    public Color32[] Data => _pixelDataArray;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PixelData"/> class.
    /// </summary>
    /// <param name="width">Width of this PixelData</param>
    /// <param name="height">Height of this PixelData</param>
    public PixelData(uint width, uint height)
    {
        Resize(width, height);
    }
    
    /// <summary>
    /// Sets a pixel at an x and y position
    /// </summary>
    /// <param name="pixel">Color to set the pixel</param>
    /// <param name="x">X position of the pixel</param>
    /// <param name="y">Y position of the pixel</param>
    public void SetPixel(Color32 pixel, uint x, uint y)
    {
        if(x >= _width)
        {
            throw new ArgumentException("x should be less then Width");
        }

        if(y >= _height)
        {
            throw new ArgumentException("y should be less then Height");
        }

        var index = x + y * _width;
        
        SetPixel(pixel, index);
    }
    
    /// <summary>
    /// Sets a pixel at an pixel index
    /// </summary>
    /// <param name="pixel">Color to set the pixel</param>
    /// <param name="index">Index of the pixel</param>
    public void SetPixel(Color32 pixel, uint index)
    {
        if(index >= NumPixels)
        {
            throw new ArgumentException("index should be less then NumPixels");
        }

        _pixelDataArray[index] = pixel;
    }

    /// <summary>
    /// Sets a pixel at an x and y position using a blending mode.
    /// Blending is performed as
    ///     SourceColor * sourceBlendFactor + DestColor * destBlendFactor
    /// Where SourceColor is the color passed in (pixel)
    /// and DestColor is the existing color on that pixel
    /// See PixelBlendFactor enum for what the factors mean
    /// If doing simple replacing of the pixel color, SetPixel is much faster
    /// </summary>
    /// <param name="pixel">Color to set the pixel</param>
    /// <param name="x">X position of the pixel</param>
    /// <param name="y">Y position of the pixel</param>
    /// <param name="sourceBlendFactor">the source factor to use on the pixel parameter before been added to the destination color</param>
    /// <param name="destBlendFactor">the destination factor to use on the pixel parameter before been added to the destination color</param>
    public void SetPixelBlended(Color32 pixel, uint x, uint y, PixelBlendFactor sourceBlendFactor, PixelBlendFactor destBlendFactor)
    {
        if(x >= _width)
        {
            throw new ArgumentException("x should be less then Width");
        }

        if(y >= _height)
        {
            throw new ArgumentException("y should be less then Height");
        }

        var index = x + y * _width;
        
        SetPixelBlended(pixel, index, sourceBlendFactor, destBlendFactor);
    }
    
    /// <summary>
    /// Sets a pixel at an x and y position using a blending mode.
    /// Blending is performed as
    ///     SourceColor * sourceBlendFactor + DestColor * destBlendFactor
    /// Where SourceColor is the color passed in (pixel)
    /// and DestColor is the existing color on that pixel
    /// See PixelBlendFactor enum for what the factors mean
    /// If doing simple replacing of the pixel color, SetPixel is much faster
    /// </summary>
    /// <param name="pixel">Color to set the pixel</param>
    /// <param name="index">Index of the pixel</param>
    /// <param name="sourceBlendFactor">the source factor to use on the pixel parameter before been added to the destination color</param>
    /// <param name="destBlendFactor">the destination factor to use on the pixel parameter before been added to the destination color</param>
    
    public void SetPixelBlended(Color32 pixel, uint index, PixelBlendFactor sourceBlendFactor, PixelBlendFactor destBlendFactor)
    {
        if(index >= NumPixels)
        {
            throw new ArgumentException("index should be less then NumPixels");
        }
        
        var source = PerformBlending(pixel, pixel, _pixelDataArray[index], sourceBlendFactor);
        var dest = PerformBlending( _pixelDataArray[index], pixel, _pixelDataArray[index], destBlendFactor);

        _pixelDataArray[index] = new Color32(
                source.R + dest.R,
                source.G + dest.G,
                source.B + dest.B,
                source.A + dest.A);
    }

    /// <summary>
    /// Gets a pixel at an x and y position
    /// </summary>
    /// <param name="x">X position of the pixel</param>
    /// <param name="y">Y position of the pixel</param>
    public Color32 GetPixel(uint x, uint y)
    {
        if(x >= _width)
        {
            throw new ArgumentException("x should be less then Width");
        }

        if(y >= _height)
        {
            throw new ArgumentException("y should be less then Height");
        }

        var index = x + y * _width;

        return _pixelDataArray[index];
    }
    
    /// <summary>
    /// Gets a pixel at a pixel index
    /// </summary>
    /// <param name="index">Index of the pixel</param>
    public Color32 GetPixel(uint index)
    {
        if(index >= NumPixels)
        {
            throw new ArgumentException("index should be less then NumPixels");
        }

        return _pixelDataArray[index];
    }
    
    /// <summary>
    /// Clear all data to a single color
    /// </summary>
    /// <param name="clearColor">Color to clear the data to</param>
    public void Clear(Color32 clearColor)
    {
        unsafe
        {
            _pixelDataArray[0]= clearColor;
            
            uint index = 1;
            uint block = 1;
            var arraySpan = new Span<Color32>(_pixelDataArray);
            
            fixed (Color32* sourcePtr = &MemoryMarshal.GetReference(arraySpan))
            {
                var itemSize = sizeof(Color32);
                
                while (index < arraySpan.Length) 
                {
                    var targetPtr = sourcePtr + index;

                    Unsafe.CopyBlock(targetPtr, sourcePtr, (uint)Math.Min(block * itemSize, (arraySpan.Length - index) * itemSize));
                    index += block;
                    block *= 2;
                }
            }
        }
    }
    
    /// <summary>
    /// Resized this PixelData. This will discard all existing data
    /// </summary>
    /// <param name="width">New width of the PixelData</param>
    /// <param name="height">New height of the PixelData</param>
    public void Resize(uint width, uint height)
    {
        _width = width;
        _height = height;
        _pixelDataArray = new Color32[width * height];
    }
    
    /// <summary>
    /// Copies data from an PixelData to this PixelData.
    /// SourceRect defines the area inside the PixelData to copy from, and it will be copied to this
    /// PixelData starting at targetX, targetY
    /// sourceRect x and y defines top left corner of source region
    /// </summary>
    /// <param name="sourcePixelData">Source data to copy from</param>
    /// <param name="sourceRect">Area in source data to copy from</param>
    /// <param name="targetX">Target X Position to copy data to in this PixelData</param>
    /// <param name="targetY">Target Y Position to copy data to in this PixelData</param>
    public void CopyFromPixelData(
        PixelData sourcePixelData,
        Rect sourceRect,
        uint targetX, uint targetY)
    {
        CopyFromColor32Array(
            sourcePixelData._pixelDataArray, 
            sourcePixelData._width, sourcePixelData._height,
            sourceRect, 
            targetX, targetY);
    }
    
    /// <summary>
    /// Copies data from an PixelData to this PixelData.
    /// Copies the whole of the source pixel data and it will be copied to this
    /// PixelData starting at targetX, targetY
    /// </summary>
    /// <param name="sourcePixelData">Source data to copy from</param>
    /// <param name="targetX">Target X Position to copy data to in this PixelData</param>
    /// <param name="targetY">Target Y Position to copy data to in this PixelData</param>
    public void CopyFromPixelData(
        PixelData sourcePixelData,
        uint targetX, uint targetY)
    {
        CopyFromPixelData(
            sourcePixelData,
            new Rect(0,0,sourcePixelData._width, sourcePixelData._height), 
            targetX, targetY);
    }
    
    /// <summary>
    /// Copies data from an array of Color32 representing pixel data.
    /// This version can be used replace all the pixel data quickly and requires sourceColorData
    /// Length to be the exactly Width * Height
    /// </summary>
    /// <param name="sourceColorData">Source data to copy from</param>
    public void CopyFromColor32Array(Color32[] sourceColorData)
    {
        if(sourceColorData.Length != NumPixels)
        {
            throw new ArgumentException($"This Overload requires sourceColorData to be exactly of Length Width * Height");
        }
        
        CopyFromColor32Array(
            sourceColorData, 
            _width, _height,
            new Rect(0,0,_width,_height), 
            0, 0);
    }
    
    /// <summary>
    /// Copies data from an array of Color32 representing pixel data of sourceColorDataWidth and
    /// sourceColorDataHeight to this PixelData.
    /// SourceRect defines the area inside the color32 array data to copy from, and it will be copied to
    /// this PixelData starting at targetX, targetY
    /// sourceRect x and y defines top left corner of source region
    /// </summary>
    /// <param name="sourceColorData">Source data to copy from</param>
    /// <param name="sourceColorDataWidth">Width of the source data</param>
    /// <param name="sourceColorDataHeight">Height of the source data</param>
    /// <param name="sourceRect">area inside the color32 array to copy from</param>
    /// <param name="targetX">Target X Position to copy data to in this PixelData</param>
    /// <param name="targetY">Target Y Position to copy data to in this PixelData</param>
    public void CopyFromColor32Array(
        Color32[] sourceColorData,
        uint sourceColorDataWidth, uint sourceColorDataHeight,
        Rect sourceRect,
        uint targetX, uint targetY)
    {
        var sourcePixelSpan = new Span<Color32>(sourceColorData);
        var destPixelSpan = new Span<Color32>(_pixelDataArray);
        
        //Checking source Data
        if(sourceRect.X >= sourceColorDataWidth)
        {
            throw new ArgumentException($"{nameof(sourceRect)} X is outside Source Data Width");
        }
        
        if(sourceRect.Y >= sourceColorDataHeight)
        {
            throw new ArgumentException($"{nameof(sourceRect)} Y is outside Source Data Height");
        }
        
        if(sourceRect.X + sourceRect.Width > sourceColorDataWidth)
        {
            throw new ArgumentException($"{nameof(sourceRect)} X + {nameof(sourceRect)} Width is outside Source Data Width");
        }
        
        if(sourceRect.Y + sourceRect.Height > sourceColorDataHeight)
        {
            throw new ArgumentException($"{nameof(sourceRect)} Y + {nameof(sourceRect)} Height is outside Source Data Height");
        }
        
        //Checking Dest Data
        if(targetX >= _width)
        {
            throw new ArgumentException($"{nameof(targetX)} is outside PixelData Width");
        }
            
        if(targetY >= _height)
        {
            throw new ArgumentException($"{nameof(targetY)} is outside PixelData Width");
        }
        
        if(targetX + sourceRect.Width > _width)
        {
            throw new ArgumentException($"{nameof(targetX)} + {nameof(sourceRect)} Width is outside PixelData Width");
        }
        
        if(targetY + sourceRect.Height > _height)
        {
            throw new ArgumentException($"{nameof(targetY)} + {nameof(sourceRect)} Height is outside PixelData Height");
        }
        
        unsafe
        {
            var itemSize = (uint)sizeof(Color32);
            
            fixed (void* sourcePin = &MemoryMarshal.GetReference(sourcePixelSpan), destPin = &MemoryMarshal.GetReference(destPixelSpan))
            {
                if(sourceColorDataWidth == _width && sourceRect.Width == _width)
                {
                    //Performing a full width copy, we can copy in one operation as a quick version
                    var dstStart = (byte*)destPin + targetY * _width * itemSize;
                    Unsafe.CopyBlock(dstStart, sourcePin, sourceColorDataHeight * sourceColorDataWidth * itemSize);
                }
                else
                {
                    //Width mismatch, have to copy each row separately 
                    for(var row = 0; row < sourceRect.Height; ++row)
                    {
                        var dstStart = (byte*)destPin + targetX * itemSize + (targetY + row) * _width * itemSize;
                        var srcStart = (byte*)sourcePin + sourceRect.X * itemSize + (sourceRect.Y + row) * sourceColorDataWidth * itemSize;
                        Unsafe.CopyBlock(dstStart, srcStart, sourceRect.Width * itemSize);
                    } 
                }
            }
        }
    }
    
    private Color32 PerformBlending(Color32 factorTargetColor, Color32 sourceColor, Color32 destColor, PixelBlendFactor blendFactor)
    {
        switch(blendFactor)
        {
            case PixelBlendFactor.Zero:
                return Color32.Clear;
            
            case PixelBlendFactor.One:
                return factorTargetColor;
            
            case PixelBlendFactor.SourceColor:
                return new Color32(
                    factorTargetColor.R * sourceColor.R,
                    factorTargetColor.G * sourceColor.G,
                    factorTargetColor.B * sourceColor.B,
                    factorTargetColor.A * sourceColor.A);
            
            case PixelBlendFactor.OneMinusSourceColor:
                return new Color32(
                    factorTargetColor.R * (255 - sourceColor.R),
                    factorTargetColor.G * (255 - sourceColor.G),
                    factorTargetColor.B * (255 - sourceColor.B),
                    factorTargetColor.A * (255 - sourceColor.A));
            
            case PixelBlendFactor.SourceAlpha:
                return new Color32(
                    factorTargetColor.R * sourceColor.A,
                    factorTargetColor.G * sourceColor.A,
                    factorTargetColor.B * sourceColor.A,
                    factorTargetColor.A * sourceColor.A);
            
            case PixelBlendFactor.OneMinusSourceAlpha:
                return new Color32(
                    factorTargetColor.R * (255 - sourceColor.A),
                    factorTargetColor.G * (255 - sourceColor.A),
                    factorTargetColor.B * (255 - sourceColor.A),
                    factorTargetColor.A * (255 - sourceColor.A));
            
            case PixelBlendFactor.DestinationColor:
                return new Color32(
                    factorTargetColor.R * destColor.R,
                    factorTargetColor.G * destColor.G,
                    factorTargetColor.B * destColor.B,
                    factorTargetColor.A * destColor.A);
            
            case PixelBlendFactor.OneMinusDestinationColor:
                return new Color32(
                    factorTargetColor.R * (255 - destColor.R),
                    factorTargetColor.G * (255 - destColor.G),
                    factorTargetColor.B * (255 - destColor.B),
                    factorTargetColor.A * (255 - destColor.A));
            
            case PixelBlendFactor.DestinationAlpha:
                return new Color32(
                    factorTargetColor.R * destColor.A,
                    factorTargetColor.G * destColor.A,
                    factorTargetColor.B * destColor.A,
                    factorTargetColor.A * destColor.A);
            
            case PixelBlendFactor.OneMinusDestinationAlpha:
                return new Color32(
                    factorTargetColor.R * (255 - destColor.A),
                    factorTargetColor.G * (255 - destColor.A),
                    factorTargetColor.B * (255 - destColor.A),
                    factorTargetColor.A * (255 - destColor.A));
            
            default:
                throw new ArgumentOutOfRangeException(nameof(blendFactor), blendFactor, null);
        }
    }
}