using System.Diagnostics.CodeAnalysis;

namespace StrangeSoft.HashTagTracker.Core;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API")]
public sealed record HashTag
{
    public Identity Id { get; }
    public string Value { get; }

    public HashTag(Identity id, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value.Contains(' '))
        {
            throw new ArgumentException("Value must not contain spaces", nameof(value));
        }

        if (!value.StartsWith('#'))
        {
            throw new ArgumentException("Value must start with #", nameof(value));
        }

        if (value[1..].Contains('#'))
        {
            throw new ArgumentException("Value must not contain more than one #", nameof(value));
        }

        Value = value;
        Id = id;
    }
}