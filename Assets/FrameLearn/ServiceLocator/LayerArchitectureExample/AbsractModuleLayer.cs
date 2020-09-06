using FrameLearn;

public abstract class AbsractModuleLayer : IModuleLayer
{
    private ModuleContainer moduleContainer;

    protected abstract IModuleFactory ModuleFactory { get; }

    protected virtual IModuleCache ModuleCache { get; } = new DefaultModuleCache();

    public AbsractModuleLayer()
    {
        moduleContainer = new ModuleContainer(ModuleCache, ModuleFactory);
    }


    public T GetModule<T>() where T : class
    {
        return moduleContainer.GetModule<T>();
    }
}