using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.IMGUI.Controls;

namespace FrameLearn
{
    public class ServiceLocator
    {
        private readonly Cache cache = new Cache();

        private readonly AbstaractInitalContext context;

        public ServiceLocator(AbstaractInitalContext context)
        {
            this.context = context;
        }



        public IService GetServiceLocator(string name)
        {
           var service =  cache.GetService(name);
            if (service ==null)
            {
                service = context.LookUp(name);
                cache.AddService(service);
            }

            //if (service)
            //{

            //}


            return service;
        }

    }
}
