using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraBehaviour : MonoBehaviour
{
    public Camera PreviousCamera;
    public Camera NextCamera;
    private GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        PreviousCamera.enabled = false;
        NextCamera.enabled = true;
        canvas.GetComponent<GameData>().currentSceneCamera = NextCamera;
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
}
