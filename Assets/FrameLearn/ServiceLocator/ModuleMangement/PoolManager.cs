using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPoolManager : IModule
{
    void DoSomething();
}

public class PoolManager : IPoolManager
{
    public void InitModule()
    {
        
    }
    public void DoSomething()
    {
        Debug.Log("PoolManager DoSomething");
    }
}

