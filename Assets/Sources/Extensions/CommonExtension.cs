using System;
using System.Collections.Generic;
using System.Linq;

public static class CommonExtension {

    public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> onItem) {
        var list = source.ToArray();

        foreach (var item in list) {
            onItem?.Invoke(item);
        }

        return list;
    }
    
    public static IEnumerable<T> For<T>(this IEnumerable<T> source, Action<int, T> onItem) {
        var list = source.ToArray();

        for (var i = 0; i < list.Length; i++) {
            onItem?.Invoke(i, list[i]);
        }

        return list;
    }
    
    public static Type[] GetAssembliesTypes(this AppDomain domain) {
        return domain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract)
            .ToArray();
    }
    
}
