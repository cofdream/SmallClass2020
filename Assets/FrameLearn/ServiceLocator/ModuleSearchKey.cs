using System;
using System.Collections.Generic;

public class ModuleSearchKey
{
    public string Name { get; set; }
    public Type Type { get; set; }

    private ModuleSearchKey()
    {
    }

    private static Stack<ModuleSearchKey> pool = new Stack<ModuleSearchKey>(5);

    public static ModuleSearchKey Allocate<T>()
    {
        ModuleSearchKey key = null;

        key = pool.Count > 0 ? pool.Pop() : new ModuleSearchKey();
        key.Type = typeof(T);

        return key;
    }

    public void ReleasePOol()
    {
        Type = null;
        Name = null;
        pool.Push(this);
    }
}