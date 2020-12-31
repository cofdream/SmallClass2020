using strange.extensions.command.impl;
using UnityEngine;

namespace FrameLearn.StrangeIOC
{
    public class DecreaseCommand : EventCommand
    {
        public const string EVENT = "DECREASE_EVENT";

        [Inject]
        public CounterAppModel model { get; set; }

        public override void Execute()
        {
            model.Score--;
            dispatcher.Dispatch("UPDATE_SCORE", model.Score);
        }
    }
}