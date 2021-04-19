using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExtendedTaskPanelBehaviour : MonoBehaviour
{
    public bool isTask = true;
    private int sceneIndex;
    private GameObject pad;
    private GameObject taskPanel;
    private GameObject extendedTaskPanel;
    private GameObject canvas;
    private GameObject closeTaskButton;
    private GameObject blackScreen;
    private Scrollbar scrollbar;

    public void OpenTaskExtendedDescription() => StartCoroutine(OpenTaskExtendedDescription_COR());

    public void CloseTaskExtendedDescription() => StartCoroutine(CloseTaskExtendedDescription_COR());

    public void GoToNextLevel() => StartCoroutine(GoToNextLevel_COR());

    public void OpenTaskExtendedDescription_Special()
    {
        closeTaskButton.transform.localScale = new Vector3(0, 0, 0);
        scrollbar.value = 1;
        extendedTaskPanel.GetComponent<Animator>().Play("MoveUp_TaskPanel_Extended");  
    }

    private IEnumerator OpenTaskExtendedDescription_COR()
    {
        scrollbar.value = 1;
        closeTaskButton.transform.localScale = new Vector3(0, 0, 0);
        taskPanel.GetComponent<Animator>().Play("MoveLeft_TaskPanel");
        pad.GetComponent<Animator>().Play("MoveRight_Pad");
        extendedTaskPanel.GetComponent<Animator>().Play("MoveUp_TaskPanel_Extended");
        yield break;
    }

    private IEnumerator CloseTaskExtendedDescription_COR()
    {  
        if (isTask)
        {
            taskPanel.GetComponent<Animator>().Play("MoveRight_TaskPanel");
            pad.GetComponent<Animator>().Play("MoveLeft_Pad");
        }
        extendedTaskPanel.GetComponent<Animator>().Play("MoveDown_TaskPanel_Extended");
        yield return new WaitForSeconds(0.7f);
        if (isTask)
            closeTaskButton.transform.localScale = new Vector3(1, 1, 1);
        else isTask = true;
    }

    private IEnumerator GoToNextLevel_COR()
    {
        extendedTaskPanel.GetComponent<Animator>().Play("MoveDown_TaskPanel_Extended");
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
        pad = GameObject.Find("Pad");
        taskPanel = GameObject.Find("TaskPanel");
        extendedTaskPanel = GameObject.Find("TaskPanel_Extended");
        canvas = GameObject.Find("Canvas");
        closeTaskButton = GameObject.Find("CloseTaskButton");
        blackScreen = GameObject.Find("BlackScreen");
        scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch(sceneIndex)
        {
            case 1:
                canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_1();
                break;
            case 2:
                canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_2();
                break;
            case 3:
                canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_3();
                break;
            case 4:
                canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_4();
                break;
            case 5:
                canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_5();
                break;
            case 6:
                canvas.GetComponent<GameData>().currentTaskNumber = 1;
                GameObject.Find("ActivateTaskButton").GetComponent<ActivateTaskButtonBehaviour>().ActivateTask();
                break;
        }
        OpenTaskExtendedDescription_Special();
        isTask = sceneIndex == 6 ? true : false;
    }
}
