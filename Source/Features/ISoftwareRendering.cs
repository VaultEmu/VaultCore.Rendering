using VaultCore.CoreAPI;

namespace VaultCore.Rendering;

/// <summary>
/// Feature for allowing your core to perform software rendering.
/// </summary>
public interface ISoftwareRendering : IVaultCoreFeature<ISoftwareRendering.FeatureApi>
{
    public interface FeatureApi : IVaultCoreFeatureApi
    {
        /// <summary>
        /// Called by the core to let the frontend know that there is pixeldata to display
        /// </summary>
        /// <param name="pixelData">The pixel data to display</param>
        void OnFrameReadyToDisplay(PixelData pixelData);
    }
}