using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public Camera currentSceneCamera;
    public int currentTaskNumber;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6)
            currentSceneCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        else currentSceneCamera = GameObject.Find("SceneCamera_1").GetComponent<Camera>();
    }
}
