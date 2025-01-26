using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
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

    [Header("Option Buttons")]
    public Button Button1m;
    public Button Button3m;
    public Button Button5m;

    private SFXPlayer sfxPlayer;


    // Start is called before the first frame update
    void Start()
    {
        sfxPlayer = SFXPlayer.instance;

        mainMenuAnim = mainMenu.GetComponent<Animator>();
        controlsMenuAnim = controlsMenu.GetComponent<Animator>();
        creditsMenuAnim = creditsMenu.GetComponent<Animator>();
        playMenuAnim = playMenu.GetComponent<Animator>();
        settingsMenuAnim = settingsMenu.GetComponent<Animator>();

        //controlsMenu.SetActive(false);
        //creditsMenu.SetActive(false);
        //playMenu.SetActive(false);
        //settingsMenu.SetActive(false);
    }
    
    public void PlayButtonPressed()
    {
        playMenuAnim.SetTrigger("Enable");
        mainMenuAnim.SetTrigger("Disable");

        sfxPlayer.PlayButtonClickSFX();

        isMainMenu = false;
        isPlayMenu = true;
    }

    public void SettingsButtonPressed()
    {
        settingsMenuAnim.SetTrigger("Enable");
        mainMenuAnim.SetTrigger("Disable");

        sfxPlayer.PlayButtonClickSFX();

        isMainMenu = false;
        isSettingsMenu = true;
    }

    public void CreditsButtonPressed()
    {
        creditsMenuAnim.SetTrigger("Enable");
        mainMenuAnim.SetTrigger("Disable");

        sfxPlayer.PlayButtonClickSFX();

        isMainMenu = false;
        isCreditsMenu = true;
    }

    public void ControlButtonPressed()
    {
        mainMenuAnim.SetTrigger("Disable");
        controlsMenuAnim.SetTrigger("Enable");

        sfxPlayer.PlayButtonClickSFX();

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

        sfxPlayer.PlayButtonClickSFX();
    }

    public void PlayGame()
    {
        sfxPlayer.PlayButtonClickSFX();

        StartCoroutine(DelayedLoad());
    }

    IEnumerator DelayedLoad()
    {
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DetermineTime()
    {

        sfxPlayer.PlayButtonClickSFX();
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Button chosenButton = clickedButton.GetComponent<Button>();

        if (chosenButton == Button1m)
        {
            PlayerPrefs.SetFloat("Time", 60f);
            chosenButton.interactable = false;

            Button3m.interactable = true;
            Button5m.interactable = true;
        }
        else if (chosenButton == Button3m)
        {
            PlayerPrefs.SetFloat("Time", 180f);
            chosenButton.interactable = false;

            Button1m.interactable = true;
            Button5m.interactable = true;
        }
        else if (chosenButton == Button5m)
        {
            PlayerPrefs.SetFloat("Time", 300f);
            chosenButton.interactable = false;

            Button1m.interactable = true;
            Button3m.interactable = true;
        }

        
    }

}
