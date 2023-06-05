using System.Diagnostics.CodeAnalysis;

namespace LegacyApp;

public readonly struct CreditLimit
{
    private readonly uint? limit = 0;

    public CreditLimit(uint? limit)
    {
        this.limit = limit;
    }

    public CreditLimit(int? limit)
    {
        this.limit = (uint?)limit;
    }

    public readonly bool HasLimit => limit is not null;

    public readonly uint Limit => limit ?? 0;

    public static implicit operator CreditLimit(uint? limit)
    {
        return new CreditLimit(limit);
    }

    public static implicit operator uint(CreditLimit limit)
    {
        return limit.Limit;
    }

    public static implicit operator CreditLimit(int? limit)
    {
        if (limit is not null && limit < 0)
        {
            throw new ArgumentException($"{nameof(limit)} must be greater than zero, current is {limit}");
        }

        return new CreditLimit(limit);
    }

    public static implicit operator int(CreditLimit limit)
    {
        return (int)limit.Limit;
    }

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is CreditLimit creditLimit
            && this.limit == creditLimit.limit;

    public override int GetHashCode()
    {
        return (int?)limit ?? -1;
    }

    public static bool operator ==(CreditLimit left, CreditLimit right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(CreditLimit left, CreditLimit right)
    {
        return !(left == right);
    }
}
