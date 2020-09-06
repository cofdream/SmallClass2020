using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FrameLearn
{
    public class DefaultModuleCache : IModuleCache
    {
        private Dictionary<Type, List<object>> modulesByType = new Dictionary<Type, List<object>>();

        public void AddModule(ModuleSearchKey key, object module)
        {
            if (modulesByType.ContainsKey(key.Type))
            {
                modulesByType[key.Type].Add(module);
            }
            else
            {
                modulesByType.Add(key.Type, new List<object>() {module});
            }
        }

        public object GetModule(ModuleSearchKey key)
        {
            List<object> modules;

            if (modulesByType.TryGetValue(key.Type, out modules))
            {
                return modules.FirstOrDefault();
            }

            return null;
        }

        public IEnumerable<object> GetAllModules(ModuleSearchKey key)
        {
            return modulesByType.Values.SelectMany(list => list);
            // foreach (var value in modulesByType.Values)
            // {
            //     foreach (var type in value)
            //     {
            //         Debug.Log(type.ToString());
            //     }
            // }
            //
            // Debug.Log("-----------------------");
            // var temp = modulesByType.Values.SelectMany(list => list);
            // foreach (var value in temp)
            // {
            //     Debug.Log(value.ToString());
            // }
        }
    }
}