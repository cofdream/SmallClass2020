using System;
using UnityEngine;

public class LayerArchiectureExample : MonoBehaviour
{
    private ILoginController loginController;

    private IUserInputManager userInputManager;

    private void Start()
    {
        loginController = ArchitectureConfig.Instance.LogicModuleLayer.GetModule<ILoginController>();
        
        userInputManager = ArchitectureConfig.Instance.LogicModuleLayer.GetModule<IUserInputManager>();
        
        loginController.Login();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            userInputManager.OnInput(KeyCode.Space);
        }
    }
}