using System.Numerics;
using Vault.Input.Mouse;

namespace VaultCore.Rendering;

public interface IRendererOutputManager
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
    /// Gets the size of the output on the frontend
    /// This may be different to the size of the backbuffer if the frontend has scaled up/down the output
    /// </summary>
    /// <param name="handle">handle of output to get size off</param>
    /// <returns></returns>
    Vector2 GetOutputSize(RenderOutputHandle handle);
    
    /// <summary>
    /// Gets the absolute position of the mouse on the output.
    /// Will be between 0-1 where 0 is left/top and 1 is bottom/right
    /// Returns true if the mouse is over the output
    /// If false, mousePosOut should not be used as it will not be valid
    /// </summary>
    /// <param name="handle">handle of the output to check</param>
    /// <param name="mouseDevice">reference to the mouse device</param>
    /// <returns>true if the mouse is over the output and mousePosOut is valid</returns>
    bool GetMouseAbsolutePosition(RenderOutputHandle handle, IMouseDevice mouseDevice, out Vector2 mousePosOut);
}