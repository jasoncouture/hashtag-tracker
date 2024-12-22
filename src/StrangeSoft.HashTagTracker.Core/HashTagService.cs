using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace StrangeSoft.HashTagTracker.Core;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API")]
public sealed record HashTagService
{
    private static readonly ConcurrentDictionary<Type, HashTagService> HashTagServices = new();

    private HashTagService(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static HashTagService For(object instance)
    {
        ArgumentNullException.ThrowIfNull(instance);
        return For(instance.GetType());
    }

    public static HashTagService For(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return HashTagServices.GetOrAdd(type, Create);
    }

    public static HashTagService For<T>() where T : class => For(typeof(T));

    private static HashTagService Create(Type arg)
    {
        ArgumentNullException.ThrowIfNull(arg);
        var name = arg.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? arg.Name;
        return new HashTagService(name);
    }
}