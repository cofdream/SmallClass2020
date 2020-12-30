using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using UnityEngine;

namespace FrameLearn.StrangeIOC
{
    public class NumberMediator : EventMediator
    {
        [Inject]
        public NumberView NumberView { get; set; }

        public override void OnRegister()
        {
            NumberView.Init();

            dispatcher.AddListener("UPDATE_SCORE",OnUpdateScore);
        }

        private void OnUpdateScore(IEvent payload)
        {
            int score = (int)payload.data;

            NumberView.NumberText.text = score.ToString();
        }

        public override void OnRemove()
        {

            dispatcher.RemoveListener("UPDATE_SCORE", OnUpdateScore);
        }
    }
}