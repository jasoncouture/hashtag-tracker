using System.Diagnostics.CodeAnalysis;

namespace StrangeSoft.HashTagTracker.Core;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API")]
public readonly record struct Identity
{
    private readonly string _id;

    private Identity(string id)
    {
        ValidateId(id);
        _id = id;
    }
    public const int MaxIdLength = 64;

    private static void ValidateId(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        if (id.Contains(' '))
        {
            throw new ArgumentException("The Id cannot contain spaces.", nameof(id));
        }

        if (id is { Length: > MaxIdLength })
        {
            throw new ArgumentException("The Id cannot be longer than 256 characters.", nameof(id));
        }
    }
    public static Identity FromString(string id) => new Identity(id);

    public static implicit operator string(Identity identity) => identity._id;
    public static explicit operator Identity(string id) => new Identity(id);
    public override string ToString() => _id;
}