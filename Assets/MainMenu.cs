using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject creditsMenu;
    public GameObject playMenu;
    public GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        playMenu.SetActive(false);
    }
    
    public void PlayButtonPressed()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void SettingsButtonPressed()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void CreditsButtonPressed()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ControlButtonPressed()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        playMenu.SetActive(false);
    }
}
