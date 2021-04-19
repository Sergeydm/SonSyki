using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExtendedTaskPanelBehaviour : MonoBehaviour
{
    [Header ("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public bool isTask = true;

    private InterfaceElements UI;
    private GameObject blackScreen;
    private int sceneIndex;

    public void OpenTaskExtendedDescription() => StartCoroutine(OpenTaskExtendedDescription_COR());

    public void CloseTaskExtendedDescription() => StartCoroutine(CloseTaskExtendedDescription_COR());

    public void GoToNextLevel() => StartCoroutine(GoToNextLevel_COR());

    public void OpenTaskExtendedDescription_Special()
    {
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        UI.ExtendedTaskDescriptionScrollbar.value = 1;
        UI.ExtendedTaskPanel.GetComponent<Animator>().Play("MoveUp_TaskPanel_Extended");  
    }

    private IEnumerator OpenTaskExtendedDescription_COR()
    {
        UI.ExtendedTaskDescriptionScrollbar.value = 1;
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        UI.TaskPanel.GetComponent<Animator>().Play("MoveLeft_TaskPanel");
        UI.Pad.GetComponent<Animator>().Play("MoveRight_Pad");
        UI.ExtendedTaskPanel.GetComponent<Animator>().Play("MoveUp_TaskPanel_Extended");
        yield break;
    }

    private IEnumerator CloseTaskExtendedDescription_COR()
    {  
        if (isTask)
        {
            UI.TaskPanel.GetComponent<Animator>().Play("MoveRight_TaskPanel");
            UI.Pad.GetComponent<Animator>().Play("MoveLeft_Pad");
        }
        UI.ExtendedTaskPanel.GetComponent<Animator>().Play("MoveDown_TaskPanel_Extended");
        yield return new WaitForSeconds(0.7f);
        if (isTask)
            UI.CloseTaskButton.transform.localScale = new Vector3(1, 1, 1);
        else isTask = true;
    }

    private IEnumerator GoToNextLevel_COR()
    {
        UI.ExtendedTaskPanel.GetComponent<Animator>().Play("MoveDown_TaskPanel_Extended");
        yield return new WaitForSeconds(0.7f);
        GameObject.Find("BlackScreen_Container").transform.localScale = new Vector3(1, 1, 1);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (currentSceneIndex)
        {
            case 1:
                SceneManager.LoadScene(2);
                break;
            case 2:
                SceneManager.LoadScene(3);
                break;
            case 3:
                SceneManager.LoadScene(4);
                break;
            case 4:
                SceneManager.LoadScene(5);
                break;
            case 6:
                SceneManager.LoadScene(1);
                break;
        }
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch(sceneIndex)
        {
            case 1:
                Canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_1();
                break;
            case 2:
                Canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_2();
                break;
            case 3:
                Canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_3();
                break;
            case 4:
                Canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_4();
                break;
            case 5:
                Canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_5();
                break;
            case 6:
                Canvas.GetComponent<GameData>().currentTaskNumber = 1;
                UI.ActivateTaskButton.GetComponent<ActivateTaskButtonBehaviour>().ActivateTask();
                break;
        }
        OpenTaskExtendedDescription_Special();
        isTask = sceneIndex == 6 ? true : false;
    }
}
