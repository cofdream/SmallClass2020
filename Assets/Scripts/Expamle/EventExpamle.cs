using Event;
using UnityEngine;

public class EventExpamle : MonoBehaviour
{
    public EventDispatcher Dispatcher { get; protected set; }
    private EventHandler<Object> TestHandle = null;
    private EventHandler<Object> TestHandle2 = null;

    private void Awake()
    {
        Dispatcher = new EventDispatcher();

        TestHandle = (t, msg) =>
        {
            Debug.Log("A");
        };
        Dispatcher.Subscribe(1, TestHandle);

        EventHandler<Object> OneHandle = null;
        OneHandle = (t, msg) =>
        {
            Debug.Log("One");
            Dispatcher.Unsubscribe(1, TestHandle2);
        };
        Dispatcher.Subscribe(1, OneHandle);

        TestHandle2 = (t, msg) =>
        {
            Debug.Log("B");
        };
        Dispatcher.Subscribe(1, TestHandle2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Object msg = null;
            Dispatcher.SendEvent(1, msg);
        }
    }


    private void OnDestroy()
    {
        {
            Object msg = null;
            Dispatcher.SendEvent(1, msg);
        }

        if (TestHandle != null)
        {
            Dispatcher.Unsubscribe(1, TestHandle);
        }
        TestHandle = null;
        {
            Object msg = null;
            Dispatcher.SendEvent(1, msg);
        }
        Dispatcher = null; 
    }
}
