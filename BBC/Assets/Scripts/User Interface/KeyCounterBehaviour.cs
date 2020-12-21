using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCounterBehaviour : MonoBehaviour
{
    public int keyCount = 0;
    public GameObject CloseDoor;
    public GameObject OpenDoor;
    private Vector3 doorPosition;
    private bool isDoorClose = true;

    private void Update()
    {
        gameObject.GetComponent<Text>().text = keyCount.ToString();
        if(keyCount >= 4 && isDoorClose)
        {
            doorPosition = CloseDoor.transform.position;
            OpenDoor.transform.position = doorPosition;
            CloseDoor.transform.position = new Vector3(1000,200,10);
            isDoorClose = false;
        }
    }
}
