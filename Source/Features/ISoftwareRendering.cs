using VaultCore.CoreAPI;

namespace VaultCore.Rendering;

/// <summary>
/// Feature for allowing your core to perform software rendering.
/// </summary>
public interface ISoftwareRendering : IVaultCoreFeature
{
    /// <summary>
    /// Called by the core to initialise an output for the software rendered data on the frontend
    /// </summary>
    /// <returns>Handle for the new output</returns>
    /// /// <param name="outputName">Name of this output</param>
    RenderOutputHandle CreateOutput(string outputName);
    
    /// <summary>
    /// Called by the core to destroy an output for the software rendered data on the frontend
    /// </summary>
    /// <param name="handle">handle of output to destroy</param>
    void DestroyOutput(RenderOutputHandle handle);
    
    /// <summary>
    /// Resets an output to its intial state before any data was sent to it
    /// </summary>
    /// <param name="handle">handle of output to destroy</param>
    void ResetOutput(RenderOutputHandle handle);
    
    /// <summary>
    /// Called by the core to let the frontend know that there is pixeldata to display by an output
    /// </summary>
    ///  /// <param name="target">handle of the target output to send the data to</param>
    /// <param name="pixelData">The pixel data to display,</param>
    void OnFrameReadyToDisplayOnOutput(RenderOutputHandle target, PixelData pixelData);
    
   
}