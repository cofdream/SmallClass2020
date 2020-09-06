using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameLearn
{
    public interface IModuleFactory
    {
        object CreateModule(ModuleSearchKey key);
        IEnumerable<object> CreateAllModules(ModuleSearchKey key);
    }
}
