using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventManager : IModule
{
    void DoSomething();
}

public class EventManager : IEventManager
{
    public IPoolManager PoolManager { get; set; }

    public void InitModule()
    {
        PoolManager = ModuleContainerConfig.ModuleContainer.GetModule<IPoolManager>();
    }

    public void DoSomething()
    {
        Debug.Log("FSM EventManager");
    }
}