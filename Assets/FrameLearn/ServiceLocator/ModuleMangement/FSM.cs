using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSM : IModule
{
    void DoSomething();
}


public class FSM : IFSM
{
    public void InitModule()
    {
        
    }
    public void DoSomething()
    {
        Debug.Log("FSM DoSomething");
    }
}
