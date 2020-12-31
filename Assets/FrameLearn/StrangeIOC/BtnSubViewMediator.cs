using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;

namespace FrameLearn.StrangeIOC
{
    public class BtnSubViewMediator : EventMediator
    {
        [Inject]
        public BtnSubView SubView { get; set; }

        public override void OnRegister()
        {
            SubView.Init();
            SubView.dispatcher.AddListener("CLICK_EVENT", OnBtnSubClick);
        }

        private void OnBtnSubClick()
        {
            dispatcher.Dispatch(DecreaseCommand.EVENT);
        }

        public override void OnRemove()
        {
            base.OnRemove();
        }
    }
}