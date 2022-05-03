using TakDotNet.Domain.ValueObjects;

namespace TakDotNet.Domain.Entities;

internal interface IBitboard
{
    public abstract Constants Constants { get; }

    public abstract Dimensions BitDimensions(ulong bits);
    public abstract CoordinatePair BitCoordinates(ulong bits);

    public abstract ulong Flood(ulong within, ulong seed);
    public abstract ulong Grow(ulong within, ulong seed);
    public abstract ulong[] FloodGroups(ulong bits, out ulong[] outValue);
    public abstract int PopulationCount(ulong bits);
    public abstract uint TrailingZeros(ulong x);
}

internal abstract class Bitboard : IBitboard
{
    public abstract Constants Constants { get; }

    #region Methods
    public virtual Dimensions BitDimensions(ulong bits)
    {
        int width = 0;
        int height = 0;

        if (bits == 0b0)
            return new Dimensions(0, 0);

        ulong bitMask = Constants.Left;
        while ((bits & bitMask) == 0b0)
        {
            bitMask >>= 0b1;
        }
        while (bitMask != 0b0 && (bits & bitMask) != 0b0)
        {
            bitMask >>= 0b1;
            width++;
        }

        bitMask = Constants.Top;
        while ((bits & bitMask) == 0b0)
        {
            bitMask >>= (int)Constants.Size;
        }
        while (bitMask != 0b0 && (bits & bitMask) != 0b0)
        {
            bitMask >>= (int)Constants.Size;
            height++;
        }

        return new Dimensions(height, width);
    }

    public virtual CoordinatePair BitCoordinates(ulong bits)
    {
        uint x, y;

        if (bits == 0b0 || (bits & (bits - 0b1)) != 0b0)
            throw (new Exception("Error in BitCoordinates: non-singular"));

        ulong bitMask = TrailingZeros(bits);
        y = (uint)(bitMask / Constants.Size);
        x = (uint)(bitMask % Constants.Size);

        return new CoordinatePair(x, y);
    }

    public virtual ulong Flood(ulong within, ulong seed)
    {
        //TODO: Is this a code smell?
        for (; ; )
        {
            ulong next = Grow(within, seed);

            if (next == seed)
                return next;

            seed = next;
        }
    }

    public virtual ulong Grow(ulong within, ulong seed)
    {
        ulong next = seed;

        next |= (seed << 0b1) & ~Constants.Right;
        next |= (seed >> 0b1) & ~Constants.Left;
        next |= seed >> (int)Constants.Size;
        next |= seed << (int)Constants.Size;

        return next & within;
    }

    public virtual ulong[] FloodGroups(ulong bits, out ulong[] outValue)
    {

        //TODO: Probably a code smell
        List<ulong> retValue = new();
        ulong seen = new();

        while (bits != 0)
        {
            ulong next = bits & (bits - 0b1);
            ulong bit = bits & ~next;

            if ((seen & bit) == 0b0)
            {
                ulong g = Flood(bits, bit);

                if (g != bit)
                    retValue.Add(g);

                seen |= g;
            }

            bits = next;
        }

        outValue = retValue.ToArray();
        return outValue;
    }

    public virtual int PopulationCount(ulong bits)
    {
        if (bits != 0)
        {
            bits -= (bits >> 0b1) & 0x5555_5555_5555_5555;
            bits = (bits >> 0b10) & (0x3333_3333_3333_3333 + bits) & 0x3333_3333_3333_3333;
            bits += bits >> 0b100;
            bits &= 0x0f0f_0f0f_0f0f_0f0f;
            bits *= 0x0101_0101_0101_0101;

            return (int)(bits >> 56);
        }

        return 0;
    }

    public virtual uint TrailingZeros(ulong x)
    {
        for (uint i = 0; i < 64; i++)
        {
            if ((x & 0b1UL << (int)i) != 0b0)
                return i;
        }

        return 0b1000000;
    }
    #endregion

}