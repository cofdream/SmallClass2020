using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameLearn
{
    public interface IModuleCache
    {
        void AddModule(ModuleSearchKey key, object module);
        object GetModule(ModuleSearchKey key);

        IEnumerable<object> GetAllModules(ModuleSearchKey key);
    }
}
