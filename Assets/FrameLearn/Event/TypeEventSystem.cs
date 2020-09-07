using System;
using System.Collections.Generic;
using UnityEngine;

namespace DevilAngel
{
    public class TypeEventSystem
    {
        private static Dictionary<Type, IRegisterations> typeEventDic = new Dictionary<Type, IRegisterations>();

        public static void Register<T>(Action<T> onReceive)
        {
            if (onReceive == null) return;

            var type = typeof(T);

            IRegisterations registerations = null;
            if (typeEventDic.TryGetValue(type, out registerations))
            {
                var reg = registerations as Registerations<T>;
                reg.OnReceives += onReceive;
            }
            else
            {
                var reg = new Registerations<T>();
                reg.OnReceives += onReceive;
                typeEventDic.Add(type, reg);
            }
        }
        public static void UnRegister<T>(Action<T> onReceive)
        {
            if (onReceive == null) return;

            var type = typeof(T);
            IRegisterations registerations = null;
            if (typeEventDic.TryGetValue(type, out registerations))
            {
                var reg = registerations as Registerations<T>;
                reg.OnReceives -= onReceive;
            }
        }
        public static void Send<T>(T t)
        {
            var type = typeof(T);
            IRegisterations registerations = null;
            if (typeEventDic.TryGetValue(type, out registerations))
            {
                var reg = registerations as Registerations<T>;
                reg.OnReceives(t);
            }
        }
    }
}