namespace VaultCore.Rendering;

/// <summary>
/// Unique Handle representing an output the software renderer feature can output to
/// </summary>
public readonly struct RenderOutputHandle : IEquatable<RenderOutputHandle>
{
    private static uint UniqueIdCount = 1;
    private readonly uint OutputID;
    
    /// <summary>
    /// Handle representing an Invalid Handle
    /// </summary>
    public static RenderOutputHandle InvalidHandle => new RenderOutputHandle(0);
    
    /// <summary>
    /// Call to create a new unique handle
    /// </summary>
    public static RenderOutputHandle Create() => new RenderOutputHandle(UniqueIdCount++);
    
    private RenderOutputHandle(uint outputId)
    {
        OutputID = outputId;
    }

    public bool Equals(RenderOutputHandle other)
    {
        return OutputID == other.OutputID;
    }

    public override bool Equals(object? obj)
    {
        return obj is RenderOutputHandle other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (int)OutputID;
    }

    public static bool operator ==(RenderOutputHandle left, RenderOutputHandle right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(RenderOutputHandle left, RenderOutputHandle right)
    {
        return !left.Equals(right);
    }
}