using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] private GameObject PausedUI;
    [SerializeField] private GameObject OptionsUI;
    [SerializeField] private GameObject HelpUI;

    [SerializeField] private GameObject KeyUI;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        color = new Color(0.4f, 0.4f, 0.4f);
        PausedUI.SetActive(false);

        EventBroadcaster.Instance.AddObserver(EventNames.ON_START_GAME, this.StartGame);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_OPTIONS_MENU, this.OptionsMenu);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_QUIT_GAME, this.QuitGame);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_PAUSE_GAME, this.PauseGame);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_RESUME_GAME, this.ResumeGame);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_QUIT_TO_MENU, this.QuitToMenu);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_KEY_GET, this.HasKey);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_KEY_LOST, this.LostKey);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_OPEN_HELP, this.OpenHelp);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_CLOSE_HELP, this.CloseHelp);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_START_GAME);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_OPTIONS_MENU);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_QUIT_GAME);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_PAUSE_GAME);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_RESUME_GAME);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_QUIT_TO_MENU);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_KEY_GET);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_KEY_LOST);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_OPEN_HELP);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_CLOSE_HELP);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartGame()
    {
        SceneManager.LoadScene(sceneName:"Intro Scene");
    }
    private void OptionsMenu()
    {
        PausedUI.SetActive(false);
        OptionsUI.SetActive(true);
    }
    private void QuitGame()
    {
        Application.Quit();
    }

    private void QuitToMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(sceneName: "Game Menu");
    }

    private void PauseGame()
    {
        OptionsUI.SetActive(false);
        PausedUI.SetActive(true);
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        PausedUI.SetActive(false);
        Time.timeScale = 1;
    }

    private void HasKey()
    {
        Debug.Log("Has Key");
        KeyUI.GetComponent<Image>().color = Color.yellow;
    }

    private void LostKey()
    {
        KeyUI.GetComponent<Image>().color = color;
        Debug.Log("Lost Key");
    }

    private void OpenHelp()
    {
        HelpUI.SetActive(true);
    }

    private void CloseHelp()
    {
        HelpUI.SetActive(false);
    }
}

