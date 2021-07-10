using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetNewActive(GameObject go)
    {
        go.SetActive(true);
    }

    public void SetNewInactive(GameObject go)
    {
        go.SetActive(false);
    }

    public void LoadScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
