using System;
using System.Collections.Generic;
using System.Reflection;

namespace FrameLearn
{
    internal static class SingletonCreator
    {
        internal static T CreateSingleton<T>() where T : class, ISingleton
        {
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception("Non Constructor() not found! in " + typeof(T));
            }

            var instance = ctor.Invoke(null) as T;

            return instance;
        }

        internal static T CreateSingletonMono<T>() where T : UnityEngine.MonoBehaviour, ISingleton
        {
            var instance = UnityEngine.Object.FindObjectOfType<T>();

            if (instance != null)
            {
                var instances = UnityEngine.Object.FindObjectsOfType<T>();

                if (instances.Length == 1) throw new Exception(typeof(T) + " Instance Existed!");

                throw new Exception(typeof(T) + " Instance Existed! And Type Count : " + instances.Length.ToString());
            }
            var gameObject = new UnityEngine.GameObject(typeof(T).Name + "Singleton No Copy");

            UnityEngine.Object.DontDestroyOnLoad(gameObject);

            instance = gameObject.AddComponent<T>();

            return instance;
        }
    }
}