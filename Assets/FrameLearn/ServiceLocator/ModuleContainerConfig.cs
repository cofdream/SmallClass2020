using System;
using System.Collections;
using System.Collections.Generic;
using FrameLearn;
using UnityEngine;

public class ModuleContainerConfig : MonoBehaviour
{
    private static ModuleContainer moduleContainer;

    public static ModuleContainer ModuleContainer
    {
        get { return moduleContainer; }
    }

    private void Awake()
    {
        var baseType = typeof(IModule);

        var cache = new DefaultModuleCache();
        var factory = new AssmeblyModuleFactory(baseType.Assembly, baseType);

        moduleContainer = new ModuleContainer(cache, factory);

        var poolManager = moduleContainer.GetModule<IPoolManager>();
        var fsm = moduleContainer.GetModule<IFSM>();
        var resManager = moduleContainer.GetModule<IResManager>();
        var eventManager = moduleContainer.GetModule<IEventManager>();
        var uiManager = moduleContainer.GetModule<IUIManager>();

        uiManager.InitModule();
        uiManager.InitModule();
        eventManager.InitModule();
        resManager.InitModule();
    }

    private void Start()
    {
        var uiManager = moduleContainer.GetModule<IUIManager>();

        uiManager.DoSomething();
    }
}