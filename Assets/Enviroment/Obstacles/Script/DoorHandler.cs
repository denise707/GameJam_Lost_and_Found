using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] DoorList;
    [SerializeField] GameObject TrueDoor;
    [SerializeField] GameObject KeyUI;
    [SerializeField] AudioClip FakeDoorSound;
    [SerializeField] AudioClip TrueDoorSound;

    [SerializeField] AudioClip WindSound;

    bool gameOver = false;
    bool antiEarBreaker = false;
    bool delayInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, 5);
        TrueDoor = DoorList[i];
        TrueDoor.GetComponent<AudioSource>().clip = WindSound;
        TrueDoor.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        DoorCheck();
    }

    private void DoorCheck()
    {
        foreach (GameObject door in DoorList)
        {
            if (Vector3.Distance(door.transform.position, player.transform.position) <= 0.4f && KeyUI.GetComponent<Image>().color == Color.yellow && antiEarBreaker == false && Vector3.Distance(TrueDoor.transform.position, player.transform.position) >= 0.6f)
            {
                AudioSource.PlayClipAtPoint(FakeDoorSound, door.transform.position, 1);
                antiEarBreaker = true;
            }
            else if (antiEarBreaker == true && delayInProgress == false)
            {
                delayInProgress = true;
                StartCoroutine(thingy(5f));
            }
        }

        if (Vector3.Distance(TrueDoor.transform.position, player.transform.position) <= 0.4f && gameOver == false && KeyUI.GetComponent<Image>().color == Color.yellow)
        {
            AudioSource.PlayClipAtPoint(TrueDoorSound, TrueDoor.transform.position, 1);
            gameOver = true;

            EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_ESCAPED);
        }
    }

    private IEnumerator thingy(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        antiEarBreaker = false;
        delayInProgress = false;
    }
}
