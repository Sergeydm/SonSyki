using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCounterBehaviour : MonoBehaviour
{
    public int keyCount = 0;
    public GameObject CloseDoor;
    public GameObject OpenDoor;
    public Vector3 positionOpenDoor;
    private void Update()
    {
        gameObject.GetComponent<Text>().text = keyCount.ToString();
        if(keyCount >= 4)
        {
            OpenDoor.transform.position = positionOpenDoor;
            CloseDoor.transform.position = new Vector3(1000,200,10);           
        }
    }
}
