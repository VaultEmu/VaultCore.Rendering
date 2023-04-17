using VaultCore.CoreAPI;

namespace VaultCore.Rendering;

/// <summary>
/// Feature for allowing your core to perform software rendering.
/// </summary>
public interface ISoftwareRendering : IVaultCoreFeature, IRendererOutputManager
{
    /// <summary>
    /// Called by the core to let the frontend know that there is pixeldata to display by an output
    /// </summary>
    ///  /// <param name="target">handle of the target output to send the data to</param>
    /// <param name="pixelData">The pixel data to display,</param>
    void OnFrameReadyToDisplayOnOutput(RenderOutputHandle target, PixelData pixelData);
}