using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
