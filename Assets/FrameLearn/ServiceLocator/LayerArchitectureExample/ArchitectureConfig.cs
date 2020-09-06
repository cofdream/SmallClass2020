using UnityEngine;

public class ArchitectureConfig : IArchitecture
{
    public ILogicModuleLayer LogicModuleLayer { get; private set; }
    public IBusinessModuleLayer BusinessModuleLayer { get; private set; }
    public IPublichModuleLayer PublichModuleLayer { get; private set; }
    public IBasicModuleLayer BasicModuleLayer { get; private set; }
    public IUtiltyModuleLayer UtiltyModuleLayer { get; private set; }

    public static ArchitectureConfig Instance;


    [RuntimeInitializeOnLoadMethod]
    static void Config()
    {
        Instance = new ArchitectureConfig();

        Instance.LogicModuleLayer = new LogicModuleLayer();

        var loginController = Instance.LogicModuleLayer.GetModule<ILoginController>();
        
        var userInputManager = Instance.LogicModuleLayer.GetModule<IUserInputManager>();
    }
}