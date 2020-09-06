//层级接口
public interface IModuleLayer
{
    T GetModule<T>() where T : class;
}
//逻辑层，UI/Game
public interface ILogicModuleLayer : IModuleLayer
{
}
//业务
public interface IBusinessModuleLayer : IModuleLayer
{
}
//公共
public interface IPublichModuleLayer : IModuleLayer
{
}
//基础设施
public interface IBasicModuleLayer : IModuleLayer
{
}
//工具
public interface IUtiltyModuleLayer : IModuleLayer
{
}