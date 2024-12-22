namespace StrangeSoft.HashTagTracker.Core;

public record HashTagServiceUser
{
    public Identity Id { get; }
    public string? Name { get; }
    public const int MaxNameLength = 256;

    private static string? SanitizeName(string? name)
    {
        if (name is not null && (name.StartsWith(' ') || name.EndsWith(' ')))
        {
            name = name.Trim();
        }

        if (name is { Length: 0 })
        {
            return null;
        }

        return name;
    }

    private static void ValidateName(string? name)
    {
        if (name is null)
            return;
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException(
                "The Name cannot be longer than 256 characters.",
                nameof(name)
            );
        }
    }
    public HashTagServiceUser(Identity id, string? name = null)
    {
        name = SanitizeName(name);
        ValidateName(name);

        Id = id;
        Name = name;
    }
}