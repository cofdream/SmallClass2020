using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResManager : IModule
{
    void DoSomething();
}

public class ResManager : IResManager
{
    public IPoolManager PoolManager { get; set; }
    public void InitModule()
    {
        PoolManager = ModuleContainerConfig.ModuleContainer.GetModule<IPoolManager>();
    }

    public void DoSomething()
    {
        Debug.Log("ResManager DoSomething");
    }
}

