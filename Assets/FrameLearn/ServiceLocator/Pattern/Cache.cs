using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FrameLearn
{
    public class Cache
    {
        private List<IService> services = new List<IService>();

        public IService GetService(string serviceName)
        {
            return services.SingleOrDefault(service => service.Name == serviceName);
        }

        public void AddService(IService service)
        {
            services.Add(service);
        }

    }
}
