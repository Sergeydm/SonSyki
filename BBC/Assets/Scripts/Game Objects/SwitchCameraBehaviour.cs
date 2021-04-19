using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraBehaviour : MonoBehaviour
{
    [Header ("Камеры для переключения")]
    [Tooltip ("Камера, которая будет выключена")]
    public Camera PreviousCamera;
    [Tooltip("Камера, которая будет включена")]
    public Camera NextCamera;
    [Header("Интерфейс")]
    public GameObject Canvas;

    private void OnTriggerEnter(Collider other)
    {
        PreviousCamera.enabled = false;
        NextCamera.enabled = true;
        Canvas.GetComponent<GameData>().currentSceneCamera = NextCamera;
    }
}
