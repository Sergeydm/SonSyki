using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTriggersBehaviour : MonoBehaviour
{
    private GameObject activateTaskButton;
    private GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        var triggerName = other.gameObject.name;
        if (triggerName.StartsWith("TaskTrigger"))
        {
            var taskNumber = int.Parse(triggerName[triggerName.Length - 1].ToString());
            var isTaskCompleted = canvas.GetComponent<TaskCompletingActions>().isTasksCompleted[taskNumber - 1];
            if (!isTaskCompleted)
            {
                activateTaskButton.SetActive(true);
                activateTaskButton.GetComponent<Animator>().Play("ScaleInterfaceUp");
                canvas.GetComponent<GameData>().currentTaskNumber = taskNumber;
            }
        }
    }

    private void OnTriggerExit(Collider other) => StartCoroutine(DeleteButton_COR());

    private IEnumerator DeleteButton_COR()
    {
        activateTaskButton.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.7f);
        activateTaskButton.SetActive(false);
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        activateTaskButton = GameObject.Find("ActivateTaskButton");
        activateTaskButton.SetActive(false);
    } 
}
