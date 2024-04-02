using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public CommonGameState state;

    public void OnRetry()
    {
        state.Restart();
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
