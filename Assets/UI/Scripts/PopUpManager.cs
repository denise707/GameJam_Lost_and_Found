using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject CaughtUI;
    [SerializeField] private GameObject EscapedUI;

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        CaughtUI.SetActive(false);
        EscapedUI.SetActive(false);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_PLAYER_CAUGHT, this.OnCaughtTrigger);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_PLAYER_ESCAPED, this.OnEscapedTrigger);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_PLAYER_CAUGHT);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_PLAYER_ESCAPED);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && gameOver)
        {
            Time.timeScale = 1;
            
            //Respawn Player
            EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_RESPAWN);

            //Remove Key UI
            EventBroadcaster.Instance.PostEvent(EventNames.ON_KEY_LOST);


            CaughtUI.SetActive(false);
            Debug.Log("Continue Game");
        }

        if (Input.GetKeyDown(KeyCode.E) && gameOver)
        {
            Debug.Log("Exit Game");
            Application.Quit();
        }
    }

    private void OnCaughtTrigger()
    {
        CaughtUI.SetActive(true);
        gameOver = true;
    }

    private void OnEscapedTrigger()
    {
        Time.timeScale = 0;
        EscapedUI.SetActive(true);
        gameOver = true;        
    }
}
