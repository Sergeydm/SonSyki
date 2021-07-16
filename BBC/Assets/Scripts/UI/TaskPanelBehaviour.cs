using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanelBehaviour : MonoBehaviour
{
    [Header ("Номер текущего задания")]
    public int taskNumber;
    [HideInInspector]
    public int tasksCount;
    [Header ("Интерфейс")]
    public GameObject Canvas;
    
    private int sceneIndex;
    private InterfaceElements UI;
    private GameData gameData;
    private PadBehaviour padBehaviour;
    private RobotBehaviour robotBehaviour;

    public void ChangeTask()
    {
        if (taskNumber <= tasksCount)
        {
            var taskText = gameData.TaskTexts[taskNumber - 1];
            UI.ExtendedTaskTitle.text = taskText.Title;
            UI.ExtendedTaskDescription.text = taskText.ExtendedDescription;
            UI.TaskTitle.text = taskText.Title;
            UI.TaskDescription.text = taskText.Description;
            padBehaviour.StartCode = taskText.StartCode;
            UI.CodeField.text = taskText.StartCode;
            UI.ResultField.text = "";
            UI.OutputField.text = "";
            Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
            UI.StartButton.GetComponent<StartButtonBehaviour>().taskNumber = taskNumber;
        }
        else
        {
            var finishMessage = gameData.FinishMessages[sceneIndex];
            UI.CloseExtendedTaskButton.gameObject.SetActive(false);
            UI.NextLevelButton.gameObject.SetActive(true);
            Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
            UI.ExtendedTaskTitle.text = finishMessage.Title;
            UI.ExtendedTaskDescription.text = finishMessage.Description;
        }
    }

    public void CloseTask() => StartCoroutine(CloseTask_COR());

    public void ReturnToScene() => StartCoroutine(ReturnToScene_COR());

    private IEnumerator CloseTask_COR()
    {
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        gameData.IsTaskStarted = false;
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        UI.IDEButton.interactable = false;
        if (sceneIndex != 0)
            yield return StartCoroutine(ReturnToScene_COR());
        padBehaviour.Mode = PadBehaviour.PadMode.Normal;
    }

    private IEnumerator ReturnToScene_COR()
    {
        gameData.CurrentSceneCamera.GetComponent<Animator>().Play("MoveToScene_TaskCamera_" + taskNumber);
        yield return new WaitForSeconds(2f);
        padBehaviour.Mode = PadBehaviour.PadMode.Normal;
        var isTaskCompleted = gameData.HasTasksCompleted[taskNumber - 1];
        if (!isTaskCompleted)
        {
            var taskMark = gameData.Player.GetComponent<TriggersBehaviour>().TaskTriggers.transform.GetChild(taskNumber - 1);
            taskMark.gameObject.SetActive(true);
            taskMark.GetComponentInChildren<Animator>().Play("RotateExclamationMark");
            StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().ShowActionButton_COR());
        }
        robotBehaviour.currentMoveSpeed = robotBehaviour.moveSpeed;
        robotBehaviour.currentRotateSpeed = robotBehaviour.rotateSpeed;
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        sceneIndex = gameData.SceneIndex;
        padBehaviour = UI.Pad.GetComponent<PadBehaviour>();
        robotBehaviour = gameData.Player.GetComponent<RobotBehaviour>();
        UI.NextLevelButton.gameObject.SetActive(false);
        tasksCount = gameData.TaskTexts.Length;
    }
}
