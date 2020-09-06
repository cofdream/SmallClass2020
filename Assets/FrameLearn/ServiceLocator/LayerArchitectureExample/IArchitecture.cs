public interface IArchitecture
{
    ILogicModuleLayer LogicModuleLayer { get; }

    IBusinessModuleLayer BusinessModuleLayer { get; }

    IPublichModuleLayer PublichModuleLayer { get; }

    IBasicModuleLayer BasicModuleLayer { get; }

    IUtiltyModuleLayer UtiltyModuleLayer { get; }
}