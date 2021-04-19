using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadBehaviour : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public string StartCode;

    private InterfaceElements UI;
    private bool isPadTurnedOff; 

    public void TurnOnOff()
    {
        if (isPadTurnedOff)
        {
            UI.CodeField.text = "";
            UI.ResultField.text = "";
            UI.OutputField.text = "";
            isPadTurnedOff = false;
        }
        else 
        {
            UI.CodeField.text = StartCode;
            isPadTurnedOff = true;
        }
    }

    public void ResetCode() => UI.CodeField.text = StartCode;

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        isPadTurnedOff = false;
    }
}
