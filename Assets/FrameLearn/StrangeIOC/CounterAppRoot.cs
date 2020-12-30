using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameLearn.StrangeIOC
{
    public class CounterAppRoot : ContextView
    {
        private void Awake()
        {
            context = new CounterAppContext(this);
        }
    }
}
