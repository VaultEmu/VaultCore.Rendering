using VaultCore.CoreAPI;

namespace VaultCore.Rendering;

/// <summary>
/// Feature for creating and working with Textures
/// </summary>
public interface ITextureManager : IVaultCoreFeature<ITextureManager.FeatureApi>
{
    public interface FeatureApi : IVaultCoreFeatureApi
    {
        /// <summary>
        /// Creates a texture
        /// Use setupForFastCpuWrite if you plan to update the texture often from the CPU
        /// </summary>
        /// <param name="width">Width of the texture to create</param>
        /// <param name="height">Height of the texture to create</param>
        /// <param name="textureFormat">Format of the texture to create</param>
        /// <param name="mipmaps">if true then the texture is created with mipmaps</param>
        /// <param name="setupForFastCpuWrite">
        /// Sets up the texture for fast cpu writes. Should be used if the texture is
        /// Updated often
        /// </param>
        /// <returns>The created Texture2D</returns>
        public Texture2D CreateTexture(uint width, uint height, 
            TextureFormat textureFormat = TextureFormat.Default, bool mipmaps = false, bool setupForFastCpuWrite = false);

        /// <summary>
        /// Loads a texture from disk
        /// </summary>
        /// <param name="path">path on disk to the texture to load</param>
        /// <param name="srgb">if true, the use a srgb format for the texture</param>
        /// <param name="mipmaps">if true then the texture is created with mipmaps</param>
        /// <param name="setupForFastCpuWrite">
        /// Sets up the texture for fast cpu writes. Should be used if the texture is
        /// Updated often
        /// </param>
        /// <returns>Texture2D containing the texture data loaded from disk</returns>
        public Texture2D LoadTextureFromDisk(string path, bool srgb = true, bool mipmaps = true, bool setupForFastCpuWrite = false);
        
        /// <summary>
        /// Loads a texture from disk as an array of ColorFloat
        /// </summary>
        /// <param name="path">path on disk to the texture to load</param>
        /// <param name="width">the width of the color data loaded</param>
        /// <param name="height">the height of the color data loaded</param>
        /// <returns>ColorFloat containing the texture data loaded from disk </returns>
        public ColorFloat[] LoadTextureFromDiskAsColorFloatArray(string path, out uint width, out uint height);
        
        /// <summary>
        /// Loads a texture from disk as an array of Color32
        /// </summary>
        /// <param name="path">path on disk to the texture to load</param>
        /// <param name="width">the width of the color data loaded</param>
        /// <param name="height">the height of the color data loaded</param>
        /// <returns>Color32 containing the texture data loaded from disk </returns>
        public Color32[] LoadTextureFromDiskAsColor32Array(string path, out uint width, out uint height);
    }
}