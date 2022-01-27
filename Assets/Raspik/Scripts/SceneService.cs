using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader("Intro");
    }

    public void OpenOptions()
    {
        SceneLoader("Options");
    }

    public void BackToMain()
    {
        SceneLoader("Main");
    }

    public void OpenCredits()
    {
        SceneLoader("Credits");
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void SceneLoader(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        Debug.Log($"Starting {sceneName}");
        SceneManager.LoadScene(sceneName, mode);
    }
}