using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenuPanelBehaviour : MonoBehaviour
{
    private bool isPressed = false;
    private GameObject exitToMenuPanel;
    private GameObject blackScreen;

    public void ReturnToGame() => StartCoroutine(ReturnToGame_COR());

    public void ExitToMenu() => StartCoroutine(ExitToMenu_COR());

    private IEnumerator ReturnToGame_COR()
    {
        exitToMenuPanel.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        isPressed = false;
    }

    private IEnumerator ExitToMenu_COR()
    {
        exitToMenuPanel.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        GameObject.Find("BlackScreen_Container").transform.localScale = new Vector3(1, 1, 1);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !isPressed)
        {
            exitToMenuPanel.GetComponent<Animator>().Play("ScaleInterfaceUp");
            isPressed = true;
        }
    }

    private void Start()
    {
        exitToMenuPanel = GameObject.Find("ExitToMenuPanel");
        blackScreen = GameObject.Find("BlackScreen");
    }
}
