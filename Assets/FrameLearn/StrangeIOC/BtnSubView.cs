using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace FrameLearn.StrangeIOC
{
    public class BtnSubView : EventView
    {
        public void Init()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                dispatcher.Dispatch("CLICK_EVENT");
            });
        }
    }
}