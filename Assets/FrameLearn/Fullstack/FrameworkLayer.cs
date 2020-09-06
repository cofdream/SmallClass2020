using System;
using System.Collections.Generic;

namespace FrameLearn.Fullstack
{
    public class FrameworkLayer : IFrameworkLayer
    {
        Dictionary<Type, object> container = new Dictionary<Type, object>();
        public void Register<T>(object obj)
        {
            var type = typeof(T);
            if (container.ContainsKey(type) == false)
            {
                container.Add(type, obj);
            }
            else UnityEngine.Debug.LogWarningFormat("Type: {0} Contains，Don't repeat.", type.ToString());
        }

        public T Get<T>() where T : class
        {
            if (container.TryGetValue(typeof(T),out object value))
            {
                return value as T;
            }
            return null;
        }
        public void Clear()
        {
            container.Clear();
        }
    }
}
