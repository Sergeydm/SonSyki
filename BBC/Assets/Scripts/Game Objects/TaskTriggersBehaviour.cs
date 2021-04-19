using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTriggersBehaviour : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;

    private InterfaceElements UI;

    private void OnTriggerEnter(Collider other)
    {
        var triggerName = other.gameObject.name;
        if (triggerName.StartsWith("TaskTrigger"))
        {
            var taskNumber = int.Parse(triggerName[triggerName.Length - 1].ToString());
            var isTaskCompleted = Canvas.GetComponent<TaskCompletingActions>().isTasksCompleted[taskNumber - 1];
            if (!isTaskCompleted)
            {
                UI.ActivateTaskButton.gameObject.SetActive(true);
                UI.ActivateTaskButton.GetComponent<Animator>().Play("ScaleInterfaceUp");
                Canvas.GetComponent<GameData>().currentTaskNumber = taskNumber;
            }
        }
    }

    private void OnTriggerExit(Collider other) => StartCoroutine(DeleteButton_COR());

    private IEnumerator DeleteButton_COR()
    {
        UI.ActivateTaskButton.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.7f);
        UI.ActivateTaskButton.gameObject.SetActive(false);
    }

    void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        UI.ActivateTaskButton.gameObject.SetActive(false);
    } 
}
