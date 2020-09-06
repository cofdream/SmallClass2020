using UnityEngine;

public interface IUserInputManager : ILogicController
{
    void OnInput(KeyCode keyCode);
}

public class UserInputManager : IUserInputManager
{
    public void OnInput(KeyCode keyCode)
    {
        Debug.Log("Input " + keyCode.ToString() + " Done.");
    }
}