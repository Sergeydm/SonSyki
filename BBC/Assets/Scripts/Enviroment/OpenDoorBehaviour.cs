using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorBehaviour : MonoBehaviour
{
    public GameObject Door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Door.transform.rotation = new Quaternion(0f,180f,0f,0f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Door.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }
}
