using UnityEngine;

public interface ILoginController : ILogicController
{
    void Login();
}

public class LoginController : ILoginController
{
    public void Login()
    {
        Debug.Log("Login Done.");
    }
}