using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorBehaviour : MonoBehaviour
{
    public GameObject Door;
    private Vector3 openPosition;
    private Vector3 closePosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Door.transform.rotation = new Quaternion(0f,180f,0f,0f);
        Door.transform.position = openPosition;
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        Door.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        Door.transform.position = closePosition;
    }*/

    private void Start()
    {
        openPosition = GameObject.Find("OpenPosition").transform.position;
        closePosition = GameObject.Find("ClosePosition").transform.position;
    }
}
