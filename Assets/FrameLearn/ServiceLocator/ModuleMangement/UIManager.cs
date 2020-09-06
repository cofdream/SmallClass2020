using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIManager : IModule
{
    void DoSomething();
}

public class UIManager : IUIManager
{
    public IResManager ResManager { get; set; }
    public IEventManager EventManager { get; set; }


    public void DoSomething()
    {
        Debug.Log("UIManager DoSomething");

        ResManager.DoSomething();
        EventManager.DoSomething();
    }

    public void InitModule()
    {
        ResManager = ModuleContainerConfig.ModuleContainer.GetModule<IResManager>();
        EventManager = ModuleContainerConfig.ModuleContainer.GetModule<IEventManager>();
    }
}