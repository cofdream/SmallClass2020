using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameLearn
{
    public class AssmeblyModuleFactory : IModuleFactory
    {
        private Type[] moduleTypes;

        /// <summary>
        /// Key:接口
        /// Value:继承自该接口的类
        /// </summary>
        private Dictionary<Type, Type> mAbstractToConcrete = new Dictionary<Type, Type>();

        public AssmeblyModuleFactory(Assembly assembly, Type baseModuleType)
        {
            var types = assembly.GetTypes();
            //剔除接口、抽象类
            var ienumerableTypes =types.Where(type => baseModuleType.IsAssignableFrom(type) && type.IsAbstract == false);
            moduleTypes = ienumerableTypes.ToArray();

            //获取当前每个类型 对应的接口，并保存
            foreach (var moduleType in moduleTypes)
            {
                var interfaces = moduleType.GetInterfaces();

                foreach (var @inferface in interfaces)
                {
                    if (baseModuleType.IsAssignableFrom(@inferface) && @inferface != baseModuleType)
                    {
                        mAbstractToConcrete.Add(@inferface, moduleType);
                    }
                }
            }
        }


        public object CreateModule(ModuleSearchKey key)
        {
            if (key.Type.IsAbstract)
            {
                Type type;
                if (mAbstractToConcrete.TryGetValue(key.Type, out type))
                {
                    return type.GetConstructors().First().Invoke(null);
                }
            }
            else
            {
                if (mAbstractToConcrete.ContainsKey(key.Type))
                {
                    return key.Type.GetConstructors().First().Invoke(null);
                }
            }

            return null;
        }

        public IEnumerable<object> CreateAllModules(ModuleSearchKey key)
        {
            return moduleTypes.Select(t => t.GetConstructors().First().Invoke(null));
        }
    }
}