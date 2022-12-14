using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
   public void StartGame()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void ShowOptions()
    {
        UIManager.Instance.ShowOptionsMenu();
    }
    public void ShowCredits()
    {
        UIManager.Instance.ShowCreditsMenu();
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void BackToMenu()
    {
        UIManager.Instance.ReturnMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
