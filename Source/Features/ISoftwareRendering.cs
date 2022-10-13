using VaultCore.CoreAPI;

namespace VaultCore.Rendering;

//Allows your core to perform software rendering.
public interface ISoftwareRendering : IFeature
{
    //Called by the core to let the frontend know that there is pixeldata to display
    void OnFrameReadyToDisplay(PixelData pixelData);
}