using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace FrameLearn.StrangeIOC
{
    public class BtnAddView : EventView
    {
        public const string CLICK_EVENT = "CLICK_EVENT";

        public void Init()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                dispatcher.Dispatch(CLICK_EVENT);
            });
        }
    }
}