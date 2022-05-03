namespace TakDotNet.Domain.ValueObjects;

public record Dimensions(int Height, int Width)
{
    public static implicit operator ValueTuple<int, int>
        (Dimensions record) => (record.Height, record.Width);

    private Dimensions() : this(0,0)
    {
    }
}
public record CoordinatePair(uint X, uint Y)
{
    public static implicit operator ValueTuple<uint, uint>
        (CoordinatePair record) => (record.X, record.Y);

    private CoordinatePair() : this(0, 0)
    {
    }
}
