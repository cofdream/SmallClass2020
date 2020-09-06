using UnityEngine;
using System.Collections;
using FrameLearn;
using System.Linq;
using System.Reflection;

public class Example : MonoBehaviour
{

    public class InitialContext : AbstaractInitalContext
    {
        public override IService LookUp(string name)
        {
            IService service = null;

            if (name == "Bluetooth")
            {
                service = new BluetoothServers();
            }

            return service;
        }

        public class BluetoothServers : IService
        {
            public string Name => "Bluetooth";

            public void Execute()
            {
                Debug.Log("启动");
            }
        }


    }
    void Start()
    {
        var context = new InitialContext();

        var serviceLoactor = new ServiceLocator(context);

        var bluetoothServie = serviceLoactor.GetServiceLocator("Bluetooth");

        // 执行服务
        bluetoothServie.Execute();
    }
}