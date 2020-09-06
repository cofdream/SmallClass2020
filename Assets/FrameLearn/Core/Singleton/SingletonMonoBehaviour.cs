using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameLearn
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour, ISingleton where T : SingletonMonoBehaviour<T>
    {
        protected SingletonMonoBehaviour() { }

        static SingletonMonoBehaviour() { onApplicationQuit = false; }

        protected static T instance;

        protected static bool onApplicationQuit;

        public static T Instance
        {
            get
            {
                if (instance == null && onApplicationQuit == false)
                {
                    instance = SingletonCreator.CreateSingletonMono<T>();
                    instance.SingletonInit();
                }
                return instance;
            }
        }

        protected virtual void SingletonInit() { }

        protected virtual void OnApplicationQuit()
        {
            onApplicationQuit = true;
            Dispose();
        }

        protected virtual void Dispose()
        {
            Destroy(gameObject);
            instance = null;
        }

        protected virtual void OnDestroy() { }
    }


}