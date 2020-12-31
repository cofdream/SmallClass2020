using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using UnityEngine;

namespace FrameLearn.StrangeIOC
{
    public class CounterAppContext : MVCSContext
    {
        public CounterAppContext(MonoBehaviour view) : base(view)
        {

        }

        protected override void mapBindings()
        {
            injectionBinder.Bind<CounterAppModel>().To<CounterAppModel>().ToSingleton();

            mediationBinder.Bind<BtnAddView>().To<BtnAddViewMediator>();
            mediationBinder.Bind<BtnSubView>().To<BtnSubViewMediator>();
            mediationBinder.Bind<NumberView>().To<NumberMediator>();

            commandBinder.Bind(IncreaseCommand.EVENT).To<IncreaseCommand>();
            commandBinder.Bind(DecreaseCommand.EVENT).To<DecreaseCommand>();
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<EventCommandBinder>().ToSingleton();
        }
    }
}