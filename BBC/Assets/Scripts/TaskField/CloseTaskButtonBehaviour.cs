﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTaskButtonBehaviour : MonoBehaviour
{
    public void CloseTask()
    {
        GameObject player = GameObject.Find("Snowman");
        InputField inputField = GameObject.Find("InputField").GetComponent<InputField>();
        InputField taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        Text resultField = GameObject.Find("ResultField").GetComponent<Text>();
        inputField.text = "";
        resultField.text = "";
        taskField.text = "";
        player.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
        //taskField.gameObject.SetActive(false);
    }
}
