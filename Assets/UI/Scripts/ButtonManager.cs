using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAY_BUTTON_SFX);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_START_GAME);
    }

    public void QuitGame()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAY_BUTTON_SFX);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_QUIT_GAME);
    }

    public void PauseGame()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.ON_STOP_ALL_SFX);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAY_BUTTON_SFX);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PAUSE_GAME);
    }

    public void ResumeGame()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAY_BUTTON_SFX);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_RESUME_GAME);
    }

    public void GameOptions()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAY_BUTTON_SFX);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_OPTIONS_MENU);
    }

    public void QuitToMenu()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAY_BUTTON_SFX);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_QUIT_TO_MENU);
    }

    public void OpenHelp()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAY_BUTTON_SFX);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_OPEN_HELP);
    }

    public void CloseHelp()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAY_BUTTON_SFX);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_CLOSE_HELP);
    }

}
