using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorStateController : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject OptionsScreen;

    [SerializeField] private bool isCursorLocked;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EventBroadcaster.Instance.AddObserver(EventNames.ON_UPDATE_CURSOR_STATE, UpdateCursorState);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_UPDATE_CURSOR_STATE);
    }


    // Update is called once per frame
    void Update()
    {
        

    }

    private void UpdateCursorState()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.visible = !Cursor.visible;

            if (Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (PauseScreen.activeInHierarchy || OptionsScreen.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
