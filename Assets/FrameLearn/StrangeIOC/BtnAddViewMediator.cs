using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using UnityEngine;

namespace FrameLearn.StrangeIOC
{
    public class BtnAddViewMediator : EventMediator
    {
        [Inject]
        public BtnAddView BtnAddView { get; set; }

        public override void OnRegister()
        {
            BtnAddView.Init();

            BtnAddView.dispatcher.AddListener(BtnAddView.CLICK_EVENT, OnBtnAddViewClick);
        }

        private void OnBtnAddViewClick()
        {
            dispatcher.Dispatch(IncreaseCommand.EVENT);
        }
        public override void OnRemove()
        {
            BtnAddView.dispatcher.RemoveListener(BtnAddView.CLICK_EVENT, OnBtnAddViewClick);
        }
    }
}
