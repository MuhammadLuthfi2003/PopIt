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

    private Animator mainMenuAnim;
    private Animator controlsMenuAnim;
    private Animator creditsMenuAnim;
    private Animator playMenuAnim;
    private Animator settingsMenuAnim;

    bool isMainMenu = true;
    bool isControlsMenu = false;
    bool isCreditsMenu = false;
    bool isPlayMenu = false;
    bool isSettingsMenu = false;


    // Start is called before the first frame update
    void Start()
    {
        mainMenuAnim = mainMenu.GetComponent<Animator>();
        controlsMenuAnim = controlsMenu.GetComponent<Animator>();
        creditsMenuAnim = creditsMenu.GetComponent<Animator>();
        playMenuAnim = playMenu.GetComponent<Animator>();
        settingsMenuAnim = settingsMenu.GetComponent<Animator>();

        //controlsMenu.SetActive(false);
        //creditsMenu.SetActive(false);
        //playMenu.SetActive(false);
        //settingsMenu.SetActive(false);

        controlsMenuAnim.SetTrigger("Disable");
        creditsMenuAnim.SetTrigger("Disable");
        playMenuAnim.SetTrigger("Disable");
        settingsMenuAnim.SetTrigger("Disable");
    }
    
    public void PlayButtonPressed()
    {
        playMenuAnim.SetTrigger("Enable");
        mainMenuAnim.SetTrigger("Disable");

        isMainMenu = false;
        isPlayMenu = true;
    }

    public void SettingsButtonPressed()
    {
        settingsMenuAnim.SetTrigger("Enable");
        mainMenuAnim.SetTrigger("Disable");

        isMainMenu = false;
        isSettingsMenu = true;
    }

    public void CreditsButtonPressed()
    {
        creditsMenuAnim.SetTrigger("Enable");
        mainMenuAnim.SetTrigger("Disable");

        isMainMenu = false;
        isCreditsMenu = true;
    }

    public void ControlButtonPressed()
    {
        mainMenuAnim.SetTrigger("Disable");
        controlsMenuAnim.SetTrigger("Enable");

        isMainMenu = false;
        isControlsMenu = true;
    }

    public void BackToMainMenu()
    {
        mainMenuAnim.SetTrigger("Enable");
        
        isMainMenu = true;

        if (isControlsMenu)
        {
            controlsMenuAnim.SetTrigger("Disable");
            isControlsMenu = false;
        }
        else if (isCreditsMenu)
        {
            creditsMenuAnim.SetTrigger("Disable");
            isCreditsMenu = false;
        }
        else if (isPlayMenu)
        {
            playMenuAnim.SetTrigger("Disable");
            isPlayMenu = false;
        }
        else if (isSettingsMenu)
        {
            settingsMenuAnim.SetTrigger("Disable");
            isSettingsMenu = false;
        }

    }
}
