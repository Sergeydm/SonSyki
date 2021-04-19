using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCompletingActions : MonoBehaviour
{
    public List<bool> isTasksCompleted = new List<bool>();
    private GameObject canvas;

    public void MakeActions(int levelNumber, int taskNumber)
    {
        if (!isTasksCompleted[taskNumber - 1])
        {
            if (levelNumber == 6)
                Invoke("MakeActions_Level_Training", 0f);
            else Invoke("MakeActions_Level_" + levelNumber + "_Task_" + taskNumber, 0f);
            isTasksCompleted[taskNumber - 1] = true;
        }
    }

    private void MakeActions_Level_Training()
    {
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
        canvas.GetComponent<GameData>().currentTaskNumber++;
        GameObject.Find("ActivateTaskButton").GetComponent<ActivateTaskButtonBehaviour>().ActivateTask();
    }

    #region Actions_Level_1
    private void MakeActions_Level_1_Task_1() => StartCoroutine(Level_1_Task_1_COR());

    private void MakeActions_Level_1_Task_2() => StartCoroutine(Level_1_Task_2_COR());

    private void MakeActions_Level_1_Task_3() => StartCoroutine(Level_1_Task_3_COR());

    private void MakeActions_Level_1_Task_4() => StartCoroutine(Level_1_Task_4_COR());

    private IEnumerator Level_1_Task_1_COR()
    {
        for (var i = 1; i <= 6; i++)
        {
            GameObject.Find("Flower_" + i).GetComponent<Animator>().Play("ToUp");
            yield return new WaitForSeconds(1.9f);
        }
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private IEnumerator Level_1_Task_2_COR()
    {
        var mushroom = GameObject.Find("Mushroom");
        mushroom.GetComponent<Animator>().Play("PickUp");
        yield return new WaitForSeconds(1.95f);
        mushroom.SetActive(false);
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private IEnumerator Level_1_Task_3_COR()
    {
        for (var i = 7; i <= 10; i++)
        {
            GameObject.Find("Flower_" + i).GetComponent<Animator>().Play("Move_Flower_" + i); 
            yield return new WaitForSeconds(2.2f);
        }
        GameObject.Find("Flower_" + 11).GetComponent<Animator>().Play("Move_Flower_" + 11);
        yield return new WaitForSeconds(5f);
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private IEnumerator Level_1_Task_4_COR()
    {
        for (var i = 1; i <= 8; i++)
        {
            GameObject.Find("Rock_" + i).GetComponent<Animator>().Play("Rock_ToUp");
            yield return new WaitForSeconds(1.9f);
        }
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }
    #endregion

    private void MakeActions_Level_2_Task_1() => StartCoroutine(Level_2_Task_1_COR());

    private void MakeActions_Level_2_Task_2() => StartCoroutine(Level_2_Task_2_COR());

    private void MakeActions_Level_2_Task_3() => StartCoroutine(Level_2_Task_3_COR());

    private void MakeActions_Level_2_Task_4() => StartCoroutine(Level_2_Task_4_COR());

    private void MakeActions_Level_2_Task_5() => StartCoroutine(Level_2_Task_5_COR());

    private void MakeActions_Level_2_Task_6() => StartCoroutine(Level_2_Task_6_COR());

    private void MakeActions_Level_2_Task_7() => StartCoroutine(Level_2_Task_7_COR());

    private void MakeActions_Level_2_Task_8() => StartCoroutine(Level_2_Task_8_COR());

    private IEnumerator Level_2_Task_1_COR()
    {
        GameObject.Find("GreenLight_1").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private IEnumerator Level_2_Task_2_COR()
    {
        GameObject.Find("ScriptingMemoryTree_1").GetComponent<Animator>().Play("ScaleBigTreeUp");
        yield return new WaitForSeconds(2f);
        GameObject.Find("ScriptingMemoryTree_2").GetComponent<Animator>().Play("ScaleSmallTreeUp");
        yield return new WaitForSeconds(2f);
        GameObject.Find("ScriptingMemoryTree_3").GetComponent<Animator>().Play("ScaleBigTreeUp");
        yield return new WaitForSeconds(2f);
        GameObject.Find("ScriptingMemoryTree_4").GetComponent<Animator>().Play("ScaleSmallTreeUp");
        yield return new WaitForSeconds(2f);
        for (var i = 1; i <= 3; i++)
        {
            GameObject.Find("MemoryTreeLog_" + i).GetComponent<Animator>().Play("PickLogUp");
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        for (var i = 1; i <= 3; i++)
            GameObject.Find("MemoryTreeLog_" + i).SetActive(false);
        GameObject.Find("ScriptingMemoryTree_1").GetComponent<Animator>().Play("ScaleBigTreeDown");
        GameObject.Find("ScriptingMemoryTree_2").GetComponent<Animator>().Play("ScaleSmallTreeDown");
        GameObject.Find("ScriptingMemoryTree_3").GetComponent<Animator>().Play("ScaleBigTreeDown");
        GameObject.Find("ScriptingMemoryTree_4").GetComponent<Animator>().Play("ScaleSmallTreeDown");
        yield return new WaitForSeconds(2f);
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private IEnumerator Level_2_Task_3_COR()
    {
        GameObject.Find("GreenLight_2").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private IEnumerator Level_2_Task_4_COR()
    {  
        yield return new WaitForSeconds(0f);
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private IEnumerator Level_2_Task_5_COR()
    {
        GameObject.Find("ScriptingTree").GetComponent<Animator>().Play("ToUp_BrokenTree");
        yield return new WaitForSeconds(2f);
        GameObject.Find("RedLight_1").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        GameObject.Find("ScriptingWagon").GetComponent<Animator>().Play("ToUp_Wagon");
        yield return new WaitForSeconds(2f);
        GameObject.Find("RedLight_2").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        GameObject.Find("GreenLight_3").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private IEnumerator Level_2_Task_6_COR()
    {
        for (var i = 1; i <= 5; i++)
        {
            GameObject.Find("BridgeFence_" + i).GetComponent<Animator>().Play("Move_BridgeFence_" + i);
            yield return new WaitForSeconds(2.7f);
        }
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private IEnumerator Level_2_Task_7_COR()
    {
        yield return new WaitForSeconds(4f);
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private IEnumerator Level_2_Task_8_COR()
    {
        GameObject.Find("RedLight_3").GetComponent<Animator>().Play("LightTurnOn");
        GameObject.Find("RedLight_4").GetComponent<Animator>().Play("LightTurnOn");
        GameObject.Find("RedLight_5").GetComponent<Animator>().Play("LightTurnOn");
        GameObject.Find("GreenLight_4").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        for (var i = 0; i < 9; i++)
        {
            isTasksCompleted.Add(false);
        }
    }
}
