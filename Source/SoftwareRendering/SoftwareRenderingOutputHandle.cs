namespace VaultCore.Rendering;

/// <summary>
/// Unique Handle representing an output the software renderer feature can output to
/// </summary>
public readonly struct SoftwareRenderingOutputHandle : IEquatable<SoftwareRenderingOutputHandle>
{
    private static uint UniqueIdCount = 1;
    private readonly uint OutputID;
    
    /// <summary>
    /// Handle representing an Invalid Handle
    /// </summary>
    public static SoftwareRenderingOutputHandle InvalidHandle => new SoftwareRenderingOutputHandle(0);
    
    /// <summary>
    /// Call to create a new unique handle
    /// </summary>
    public static SoftwareRenderingOutputHandle Create() => new SoftwareRenderingOutputHandle(UniqueIdCount++);
    
    private SoftwareRenderingOutputHandle(uint outputId)
    {
        OutputID = outputId;
    }

    public bool Equals(SoftwareRenderingOutputHandle other)
    {
        return OutputID == other.OutputID;
    }

    public override bool Equals(object? obj)
    {
        return obj is SoftwareRenderingOutputHandle other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (int)OutputID;
    }

    public static bool operator ==(SoftwareRenderingOutputHandle left, SoftwareRenderingOutputHandle right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SoftwareRenderingOutputHandle left, SoftwareRenderingOutputHandle right)
    {
        return !left.Equals(right);
    }
}