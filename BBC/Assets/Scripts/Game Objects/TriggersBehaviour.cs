using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggersBehaviour : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;
    [Header("Триггеры заданий")]
    public GameObject TaskTriggers;
    [Header("Триггеры смены сцен")]
    public GameObject EnterTriggers;
    [Header("Триггеры активации сценарного момента")]
    public GameObject ScenarioTriggers;
    [Header("Диалоговые персонажи с триггерами")]
    public GameObject DialogCharacters;

    private InterfaceElements UI;
    private InterfaceAnimations UIAnimations;

    public void DeleteActionButton() => StartCoroutine(DeleteActionButton_COR());

    private void OnTriggerEnter(Collider other)
    {
        var triggerName = other.gameObject.name;
        if (triggerName.StartsWith("TaskTrigger"))
        {
            var taskNumber = int.Parse(triggerName.Split('_')[1]);
            if (!Canvas.GetComponent<GameData>().HasTasksCompleted[taskNumber - 1])
            {
                ActivateButton("Начать задание", ActionButtonBehaviour.TriggerType.Task);
                Canvas.GetComponent<GameData>().CurrentTaskNumber = taskNumber;
            }
        }
        else if (triggerName.StartsWith("EnterTrigger"))
        {
            var buttonText = other.gameObject.GetComponent<SwitchSceneBehaviour>().buttonText;
            ActivateButton(buttonText, ActionButtonBehaviour.TriggerType.SceneChange);
            Canvas.GetComponent<GameData>().CurrentChangeSceneTriggerNumber = int.Parse(triggerName.Split('_')[1]);
        }
        else if (triggerName.StartsWith("ScenarioTrigger"))
        {
            ActivateButton("Взаимодействовать", ActionButtonBehaviour.TriggerType.ScenarioMoment);
            Canvas.GetComponent<GameData>().CurrentScenarioTriggerNumber = int.Parse(triggerName.Split('_')[1]);
        }
        else if (triggerName.StartsWith("SaveTrigger"))
        {
            ActivateButton("Сохранить игру", ActionButtonBehaviour.TriggerType.Save);
            Canvas.GetComponent<GameData>().CurrentSaveTriggerNumber = int.Parse(triggerName.Split('_')[1]);
        }
        else if (triggerName.StartsWith("DialogTrigger"))
        {
            ActivateButton("Поговорить", ActionButtonBehaviour.TriggerType.Dialog);
        }
        else if (triggerName.StartsWith("Coin"))
            StartCoroutine(PickCoinUp_COR(other));
    }

    private void OnTriggerExit(Collider other)
    {
        var triggerName = other.gameObject.name;
        if (triggerName.StartsWith("TaskTrigger") || triggerName.StartsWith("EnterTrigger") || triggerName.StartsWith("ScenarioTrigger")
            || triggerName.StartsWith("SaveTrigger") || triggerName.StartsWith("DialogTrigger"))
            StartCoroutine(DeleteActionButton_COR());
    }

    private void ActivateButton(string buttonText, ActionButtonBehaviour.TriggerType triggerType)
    {
        Canvas.GetComponent<ActionButtonBehaviour>().triggerType = triggerType;
        UI.ActionButton.gameObject.SetActive(true);
        UI.ActionButton.GetComponentInChildren<Text>().text = buttonText;
        StartCoroutine(UIAnimations.ShowActionButton_COR());
    }

    private IEnumerator DeleteActionButton_COR()
    {
        yield return StartCoroutine(UIAnimations.HideActionButton_COR());
        UI.ActionButton.gameObject.SetActive(false);
    }

    private IEnumerator PickCoinUp_COR(Collider coin)
    {
        coin.GetComponentInChildren<Animator>().Play("PickCoinUp");
        yield return new WaitForSeconds(1f);
        coin.gameObject.SetActive(false);
        Canvas.GetComponent<GameData>().CoinsCount++;
    }

    private void RotateMarks(GameObject triggers)
    {
        if (triggers != null)
        {
            for (var i = 0; i < triggers.transform.childCount; i++)
            {
                var currentChild = triggers.transform.GetChild(i);
                currentChild.GetComponentInChildren<Animator>().Play("RotateExclamationMark");
            }
        }
    }

    private void ActivateNpcMarks()
    {
        if (DialogCharacters != null)
        {
            for (var i = 0; i < DialogCharacters.transform.childCount; i++)
            {
                if (DialogCharacters.transform.GetChild(i).childCount > 1)
                    DialogCharacters.transform.GetChild(i).GetChild(1).GetComponentInChildren<Animator>().Play("RotateExclamationMark");
            }
        }
    }
    
    void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        UIAnimations = Canvas.GetComponent<InterfaceAnimations>();
        UI.ActionButton.gameObject.SetActive(false);
        RotateMarks(TaskTriggers);
        RotateMarks(EnterTriggers);
        RotateMarks(ScenarioTriggers);
        ActivateNpcMarks();
    } 
}
