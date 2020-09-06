using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameLearn
{
    public abstract class AbstaractInitalContext
    {
        public abstract IService LookUp(string name);
    }
}
