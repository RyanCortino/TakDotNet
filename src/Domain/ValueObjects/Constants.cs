namespace TakDotNet.Domain.ValueObjects;

internal class Constants
{
    public uint Size;
    public ulong Left, Right, Top, Bottom;
    public ulong Mask;
    public ulong Edge;

    public static Constants PrecomputeConstants(uint size)
    {
        Constants constants = new();

        for (uint i = 0; i < size; i++)
        {
            constants.Right |= 0b1UL << (int)(i * size);
        }

        constants.Size = size;
        constants.Left = constants.Right << (int)(size - 1);
        constants.Top = ((0b1UL << (int)size) - 1) << (int)(size * (size - 1));
        constants.Bottom = (0b1UL << (int)size) - 1;
        constants.Mask = (0b1UL << ((int)size * (int)size)) - 1;
        constants.Edge = (constants.Left | constants.Right | constants.Bottom | constants.Top);

        return constants;
    }
}
