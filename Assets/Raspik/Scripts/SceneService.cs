using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;
    public void StartGame()
    {
        SceneLoader("BattleScene");
    }

    public void OpenOptions()
    {
        Animator animator = OptionsPanel.GetComponent<Animator>();
        if (animator != null)
        {
            bool isOpen = animator.GetBool("Open");
            animator.SetBool("Open",!isOpen);
        }
    }

    public void BackToMain()
    {
        SceneLoader("Main");
    }

    public void OpenCredits()
    {
        Animator animator = CreditsPanel.GetComponent<Animator>();
        if (animator != null)
        {
            bool isOpen = animator.GetBool("Open");
            animator.SetBool("Open",!isOpen);
        }
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