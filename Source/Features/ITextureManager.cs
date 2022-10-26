using VaultCore.CoreAPI;

namespace VaultCore.Rendering;

//Feature for creating and working with Textures
public interface ITextureManager : IFeature
{
    //Creates a texture for use with imgui
    //Use setupForFastCpuWrite if you plan to update the texture often from the CPU
    public Texture2D CreateTexture(uint width, uint height, 
        TextureFormat textureFormat = TextureFormat.Default, bool mipmaps = false, bool setupForFastCpuWrite = false);

    //Loads a texture from disk
    //Use setupForFastCpuWrite if you plan to update the texture often from the CPU
    public Texture2D LoadTextureFromDisk(string path, bool srgb = true, bool mipmaps = true, bool setupForFastCpuWrite = false);
    
    //Loads a texture from disk as an array of ColorFloat
    public ColorFloat[] LoadTextureFromDiskAsColorFloatArray(string path, out uint width, out uint height);
    
    //Loads a texture from disk as an array of Color32
    public Color32[] LoadTextureFromDiskAsColor32Array(string path, out uint width, out uint height);
}