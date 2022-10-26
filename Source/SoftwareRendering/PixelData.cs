using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VaultCore.Rendering;

//Class wrapping around an array of color32 that represents the pixel data of some width and height
//Data is defined as pixel 0,0 been in the bottom left corner with X going right and y going up
public class PixelData
{
    private Color32[] _pixelDataArray = null!;
    private uint _width;
    private uint _height;
    
    public uint Width => _width;
    
    public uint Height => _height;
    
    public uint NumPixels => _width * _height;
    
    public PixelData(uint width, uint height)
    {
        Resize(width, height);
    }
    
    //Sets a pixel at an x and y position
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

        _pixelDataArray[index] = pixel;
    }
    
    //Sets the color of a pixel at a pixel index
    public void SetPixel(Color32 pixel, uint index)
    {
        if(index >= NumPixels)
        {
            throw new ArgumentException("index should be less then NumPixels");
        }

        _pixelDataArray[index] = pixel;
    }

    //Gets a pixel at an x and y position
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
    
    //Gets a pixel at a pixel index
    public Color32 GetPixel(uint index)
    {
        if(index >= NumPixels)
        {
            throw new ArgumentException("index should be less then NumPixels");
        }

        return _pixelDataArray[index];
    }
    
    //Resized this PixelData. This will discard all existing data
    public void Resize(uint width, uint height)
    {
        _width = width;
        _height = height;
        _pixelDataArray = new Color32[width * height];
    }
    
    // Copies data from an PixelData to this PixelData.
    // SourceRect defines the area inside the PixelData to copy from, and it will be copied to this
    // PixelData starting at targetX, targetY
    // sourceRect x and y defines bottom left corner of source region
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
    
    // Copies data from an PixelData to this PixelData.
    // Copies the whole of the source pixel data and it will be copied to this
    // PixelData starting at targetX, targetY
    public void CopyFromPixelData(
        PixelData sourcePixelData,
        uint targetX, uint targetY)
    {
        CopyFromPixelData(
            sourcePixelData,
            new Rect(0,0,sourcePixelData._width, sourcePixelData._height), 
            targetX, targetY);
    }
    
    // Copies data from an array of Color32 representing pixel data.
    // This version can be used replace all the pixel data quickly and requires sourceColorData Length to be the exactly Width * Height
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
    
    // Copies data from an array of Color32 representing pixel data of sourceColorDataWidth and sourceColorDataHeight to this PixelData.
    // SourceRect defines the area inside the color32 array data to copy from, and it will be copied to this
    // PixelData starting at targetX, targetY
    // sourceRect x and y defines bottom left corner of source region
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
}