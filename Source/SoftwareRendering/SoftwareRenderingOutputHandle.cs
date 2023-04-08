namespace VaultCore.Rendering;

/// <summary>
/// Handle representing an output the software renderer feature can output to
/// </summary>
public readonly struct SoftwareRenderingOutputHandle : IEquatable<SoftwareRenderingOutputHandle>
{
    private readonly uint OutputID;
    
    public SoftwareRenderingOutputHandle(uint outputId)
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