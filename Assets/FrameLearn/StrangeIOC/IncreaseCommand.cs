using strange.extensions.command.impl;
using UnityEngine;

namespace FrameLearn.StrangeIOC
{
    public class IncreaseCommand : EventCommand
    {
        public const string EVENT = "EVENT";

        [Inject]
        public CounterAppModel Model { get; set; }

        public override void Execute()
        {
            Model.Score++;
            evt.data = Model.Score;
            dispatcher.Dispatch("UPDATE_SCORE", evt);
        }
    }
}