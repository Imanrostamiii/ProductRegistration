using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Extensions;

public static class productSellExtensions
{
    public static void allEntity<BaseType>(this ModelBuilder builder, params Assembly[] assemblies)
    {
        IEnumerable<Type> types = assemblies
            .SelectMany(p => p.GetExportedTypes())
            .Where(p => p.IsClass && p.IsPublic && !p.IsAbstract && typeof(BaseType).IsAssignableFrom(p));
        //.Where(p => p.IsClass  && p.IsSubclassOf(typeof(BaseType)));
        foreach (Type type in types)
        {
            builder.Entity(type);
        }

    }
}