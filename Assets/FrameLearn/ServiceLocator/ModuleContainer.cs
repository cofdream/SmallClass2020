using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FrameLearn
{
    public class ModuleContainer
    {
        private IModuleCache moduleCache;

        private IModuleFactory moduleFactory;


        public ModuleContainer(IModuleCache moduleCache, IModuleFactory moduleFactory)
        {
            this.moduleCache = moduleCache;
            this.moduleFactory = moduleFactory;
        }


        public T GetModule<T>() where T : class
        {
            var moduleKey = ModuleSearchKey.Allocate<T>();
            var module = moduleCache.GetModule(moduleKey);

            if (module == null)
            {
                module = moduleFactory.CreateModule(moduleKey);

                moduleCache.AddModule(moduleKey, module);
            }

            moduleKey.ReleasePOol();
            return module as T;
        }

        public IEnumerable<T> GetAllModules<T>() where T : class
        {
            var moduleKey = ModuleSearchKey.Allocate<T>();
            var modules = moduleCache.GetAllModules(moduleKey);

            if (modules == null || modules.Any() == false)//无法获取缓存中的数据
            {
                modules = moduleFactory.CreateAllModules(moduleKey);

                foreach (var module in modules)
                {
                    moduleCache.AddModule(moduleKey, module);
                }
            }

            moduleKey.ReleasePOol();

            return modules.Select(module => module as T);
        }
    }
}