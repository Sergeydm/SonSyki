using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadBehaviour : MonoBehaviour
{
    public string startCode;
    public Vector3 padPosition;
    private InputField codeField;
    private InputField resultField;
    private InputField outputField;
    private bool isPadTurnedOff; 

    public void TurnOnOff()
    {
        if (isPadTurnedOff)
        {
            codeField.text = "";
            resultField.text = "";
            outputField.text = "";
            isPadTurnedOff = false;
        }
        else 
        {
            codeField.text = startCode;
            isPadTurnedOff = true;
        }
    }

    public void ResetCode()
    {
        codeField.text = startCode;
    }

    private void Start()
    {
        codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        resultField = GameObject.Find("ResultField").GetComponent<InputField>();
        outputField = GameObject.Find("OutputField").GetComponent<InputField>();
        isPadTurnedOff = false;
    }
}
