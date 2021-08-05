using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{

    [SerializeField] private Transform targetKey;
    [SerializeField] private GameObject bookMark;


    // Start is called before the first frame update
    void Awake()
    {
        bookMark.transform.position = new Vector3(this.transform.position.x, -0.38f, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = targetKey.transform.position;
        targetPosition.y = this.transform.position.y;
        transform.LookAt(targetPosition);

        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale != 0)
        {
            bookMark.transform.position = new Vector3(this.transform.position.x, -0.38f, this.transform.position.z);
        }
    }
}
