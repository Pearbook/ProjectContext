﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{

    public CanvasGroup MainMenu;
    public CanvasGroup InstructionsMenu;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCustomize()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenMainMenu()
    {
        MainMenu.alpha = 1;
        MainMenu.interactable = true;
        MainMenu.blocksRaycasts = true;

        InstructionsMenu.alpha = 0;
        InstructionsMenu.interactable = false;
        InstructionsMenu.blocksRaycasts = false;
    }

    public void OpenHowToPlay()
    {
        MainMenu.alpha = 0;
        MainMenu.interactable = false;
        MainMenu.blocksRaycasts = false;

        InstructionsMenu.alpha = 1;
        InstructionsMenu.interactable = true;
        InstructionsMenu.blocksRaycasts = true;
    }
}
