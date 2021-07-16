using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonBehaviour : MonoBehaviour
{
    public enum TriggerType
    {
        Task,
        SceneChange,
        ScenarioMoment,
        Save,
        Dialog
    }

    [Header ("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public TriggerType triggerType;

    private RobotBehaviour robotBehaviour;
    private GameObject taskTriggers;
    private GameObject enterTriggers;
    private GameObject scenarioTriggers;
    private InterfaceElements UI;
    private GameData gameData;
    private int sceneIndex;

    public void MakeAction()
    {
        switch (triggerType)
        {
            case TriggerType.Task:
                ActivateTask();
                break;
            case TriggerType.SceneChange:
                ChangeScene();
                break;
            case TriggerType.ScenarioMoment:
                ActivateScenarioMoment();
                break;
            case TriggerType.Save:
                StartCoroutine(SaveGame_COR());
                break;
            case TriggerType.Dialog:
                ActivateDialog();
                break;
        }
    }

    public void ActivateTask(bool hasActivateButton = true)
    {
        var currentTaskNumber = gameData.CurrentTaskNumber;
        Canvas.GetComponent<TaskPanelBehaviour>().taskNumber = currentTaskNumber;
        gameData.IsTaskStarted = true;
        UI.IDEButton.interactable = true;
        robotBehaviour.FreezePlayer();
        if (gameData.SceneIndex != 0)
            taskTriggers.transform.GetChild(currentTaskNumber - 1).gameObject.SetActive(false);
        StartCoroutine(TurnOnTaskCamera_COR(currentTaskNumber, hasActivateButton));
    }

    public void ChangeScene()
    {
        var triggerNumber = gameData.CurrentChangeSceneTriggerNumber;
        robotBehaviour.FreezePlayer();
        if (gameData.SceneIndex != 0)
            enterTriggers.transform.GetChild(triggerNumber - 1).gameObject.SetActive(false);
        StartCoroutine(ChangeScene_COR(triggerNumber));
    }

    public void ActivateScenarioMoment()
    {
        var triggerNumber = gameData.CurrentScenarioTriggerNumber;
        switch (sceneIndex)
        {
            case 3:
                StartCoroutine(ActivateScenarioMoment_Level_3_COR(triggerNumber));
                break;
        }
    }

    public void ActivateDialog() => StartCoroutine(ActivateDialog_COR());

    private IEnumerator ActivateDialog_COR()
    {
        var dialogTrigger = gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger;
        dialogTrigger.transform.GetChild(0).gameObject.SetActive(false);
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideActionButton_COR());      
        Canvas.GetComponent<VIDEUIManager1>().Interact(dialogTrigger);
    }

    private IEnumerator SaveGame_COR()
    {
        UI.ActionButton.interactable = false;
        Canvas.GetComponent<SaveLoad>().Save();
        var saveTrigger = GameObject.Find("SaveTrigger_" + gameData.CurrentSaveTriggerNumber);
        saveTrigger.GetComponentInChildren<Animator>().Play("Saving");
        var buttonText = UI.ActionButton.GetComponentInChildren<Text>();
        for (var i = 1; i <= 3; i++)
        {
            buttonText.text = "Сохранение" + new string('.', i);
            yield return new WaitForSeconds(0.667f);
        }
        buttonText.text = "Игра сохранена.";
        yield return new WaitForSeconds(1.5f);
        buttonText.text = "Сохранить игру";
        UI.ActionButton.interactable = true;
    }

    private IEnumerator TurnOnTaskCamera_COR(int currentTaskNumber, bool hasActivateButton = true)
    {
        if (hasActivateButton)
            yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideActionButton_COR());
        if (currentTaskNumber <= Canvas.GetComponent<TaskPanelBehaviour>().tasksCount)
        {
            var currentCamera = gameData.CurrentSceneCamera;
            var currentCameraName = currentCamera.gameObject.name;
            if (currentCameraName.StartsWith("SceneCamera"))
            {
                var currentCameraNumber = int.Parse(currentCameraName.Split('_')[1]);
                currentCamera.GetComponent<Animator>().Play("MoveToTask_" + currentTaskNumber + "_SceneCamera_" + currentCameraNumber);
                yield return new WaitForSeconds(2f);
            }
        }
        Canvas.GetComponent<TaskPanelBehaviour>().ChangeTask();
    } 

    private IEnumerator ChangeScene_COR(int triggerNumber)
    {
        var blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
        UI.BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(2.5f);
        var currentTrigger = enterTriggers.transform.GetChild(triggerNumber - 1).gameObject;
        var previousCamera = currentTrigger.GetComponent<SwitchSceneBehaviour>().PreviousCamera;
        var nextCamera = currentTrigger.GetComponent<SwitchSceneBehaviour>().NextCamera;
        previousCamera.enabled = false;
        nextCamera.enabled = true;
        gameData.CurrentSceneCamera = nextCamera;
        var destinationTriggerNumber = currentTrigger.GetComponent<SwitchSceneBehaviour>().destinationTriggerNumber;
        gameData.Player.transform.position = enterTriggers.transform.GetChild(destinationTriggerNumber - 1).position;
        currentTrigger.SetActive(true);
        currentTrigger.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("RotateExclamationMark");
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(2f);
        UI.BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        robotBehaviour.UnfreezePlayer();
    }    

    private IEnumerator ActivateScenarioMoment_Level_3_COR(int triggerNumber)
    {
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideActionButton_COR());
        scenarioTriggers.transform.GetChild(triggerNumber - 1).gameObject.SetActive(false);
        switch (triggerNumber)
        {
            case 1:
                var message = gameData.ScenarioMessages[0];
                UI.ExtendedTaskTitle.text = message.Title;
                UI.ExtendedTaskDescription.text = message.Description;
                Canvas.GetComponent<ExtendedTaskPanelBehaviour>().isTaskMessage = false;
                Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
                ActivateTrigger(taskTriggers, 7);
                ActivateTrigger(enterTriggers, 0);
                ActivateTrigger(enterTriggers, 2);
                break;
            case 2:
                gameData.CurrentSceneCamera.GetComponent<Animator>().Play("MoveToTask_9_SceneCamera_14");
                yield return new WaitForSeconds(2f);
                var gatesKeys = GameObject.Find("GatesKeys");
                for (var i = 0; i < gatesKeys.transform.childCount; i++)
                {
                    var key = gatesKeys.transform.GetChild(i).gameObject;
                    key.SetActive(true);
                    key.GetComponentInChildren<Animator>().Play("TurnKey");
                }
                yield return new WaitForSeconds(3f);
                for (var i = 0; i < gatesKeys.transform.childCount; i++)
                {
                    var key = gatesKeys.transform.GetChild(i).gameObject;
                    key.SetActive(false);
                }
                var blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
                UI.BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
                yield return new WaitForSeconds(1.5f);
                GameObject.Find("LeftGate").GetComponent<Animator>().Play("OpenLeftGate");
                GameObject.Find("RightGate").GetComponent<Animator>().Play("OpenRightGate");
                yield return new WaitForSeconds(2f);
                blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
                yield return new WaitForSeconds(1.4f);
                UI.BlackScreen.transform.localScale = new Vector3(0, 0, 0);
                gameData.CurrentSceneCamera.GetComponent<Animator>().Play("MoveToScene_TaskCamera_9");
                yield return new WaitForSeconds(2f);
                robotBehaviour.currentMoveSpeed = robotBehaviour.moveSpeed;
                robotBehaviour.currentRotateSpeed = robotBehaviour.rotateSpeed;
                break;
        }
    }

    private void ActivateTrigger(GameObject triggersGroup, int childNumber)
    {
        var childTrigger = triggersGroup.transform.GetChild(childNumber).gameObject;
        childTrigger.SetActive(true);
        childTrigger.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("RotateExclamationMark");
    }

    private void Awake()
    {
        gameData = Canvas.GetComponent<GameData>();
        robotBehaviour = gameData.Player.GetComponent<RobotBehaviour>();
    }

    private void Start()
    {
        sceneIndex = Canvas.GetComponent<GameData>().SceneIndex;
        UI = Canvas.GetComponent<InterfaceElements>();     
        if (sceneIndex != 0)
        {
            scenarioTriggers = gameData.Player.GetComponent<TriggersBehaviour>().ScenarioTriggers;
            taskTriggers = gameData.Player.GetComponent<TriggersBehaviour>().TaskTriggers;
            enterTriggers = gameData.Player.GetComponent<TriggersBehaviour>().EnterTriggers;
        }
    }
}
