using FrameLearn;

public interface ILogicController
{
}

public class LogicModuleLayer : AbsractModuleLayer, ILogicModuleLayer
{
    private IModuleFactory moduleFactory =
        new AssmeblyModuleFactory(typeof(ILogicController).Assembly, typeof(ILogicController));

    protected override IModuleFactory ModuleFactory
    {
        get { return moduleFactory; }
    }
}