using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    [Header ("Текущая включенная камера на сцене")]
    public Camera currentSceneCamera;
    [Header("Номер текущего задания")]
    public int currentTaskNumber;
}
