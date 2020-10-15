﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButtonBehaviour : MonoBehaviour
{
    public void ReturnStartText()
    {
        InputField field = GameObject.Find("InputField").GetComponent<InputField>();
        field.text = field.GetComponent<InputFieldBehaviour>().taskSignature;
    }
}
