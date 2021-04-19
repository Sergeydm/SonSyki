using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenuPanelBehaviour : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;

    private InterfaceElements UI;
    private GameObject blackScreenContent;
    private bool isPressed = false;

    public void ReturnToGame() => StartCoroutine(ReturnToGame_COR());

    public void ExitToMenu() => StartCoroutine(ExitToMenu_COR());

    private IEnumerator ReturnToGame_COR()
    {
        UI.ExitToMenuPanel.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        isPressed = false;
    }

    private IEnumerator ExitToMenu_COR()
    {
        UI.ExitToMenuPanel.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        UI.BlackScreen.transform.localScale = new Vector3(1, 1, 1);
        blackScreenContent.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !isPressed)
        {
            UI.ExitToMenuPanel.GetComponent<Animator>().Play("ScaleInterfaceUp");
            isPressed = true;
        }
    }

    private void Start()
    {
        blackScreenContent = UI.BlackScreen.transform.GetChild(0).gameObject;
    }
}
