using System.Diagnostics.CodeAnalysis;

namespace PlayFabEmuCore.Models;

public struct FabId : IEquatable<FabId>, IEqualityComparer<FabId>
{
    public static FabId Empty => new FabId(0);
    public static FabId RandomId => new FabId(Random.Shared.NextInt64());

    public string StringId;

    public long LongId;

    public FabId()
    {
        this.LongId = 0;
        this.StringId = LongId.ToString("X");
    }

    public FabId(long longId)
    {
        this.LongId = longId;
        this.StringId = LongId.ToString("X");
    }

    public FabId(string sId)
    {
        sId = sId.ToUpper();
        if (sId.Length != 16)
            throw new ArgumentException("FabId must be 16 character long");
        this.StringId = sId;
        this.LongId = Convert.ToInt64(StringId, 16);
    }

    public override string ToString()
    {
        return StringId;
    }

    public override int GetHashCode()
    {
        return LongId.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is FabId && Equals((FabId)obj);
    }

    public bool Equals(FabId other)
    {
        return other.GetHashCode() == this.GetHashCode();
    }

    public readonly bool Equals(FabId x, FabId y)
    {
        return x.GetHashCode() == y.GetHashCode();
    }

    public int GetHashCode([DisallowNull] FabId obj)
    {
        return obj.GetHashCode();
    }

    public static implicit operator long(FabId fabId)
    {
        return fabId.LongId;
    }

    public static implicit operator string(FabId fabId)
    {
        return fabId.StringId;
    }

    public static implicit operator FabId(long fabId)
    {
        return new(fabId);
    }

    public static implicit operator FabId(string fabId)
    {
        return new(fabId);
    }


}
