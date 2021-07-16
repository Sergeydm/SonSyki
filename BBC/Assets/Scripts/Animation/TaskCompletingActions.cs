using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCompletingActions : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;

    private int sceneIndex;
    private GameObject taskTriggers;
    private GameObject enterTriggers;
    private GameObject scenarioTriggers;
    private GameObject player;
    private RobotBehaviour robotBehaviour;
    private GameData gameData;
    private InterfaceElements UI;
    private InterfaceAnimations UIAnimations;

    public void MakeActions(int taskNumber)
    {
        if (!gameData.HasTasksCompleted[taskNumber - 1])
        {
            if (sceneIndex == 0)
                StartCoroutine(MakeActions_Level_Training());
            else StartCoroutine("MakeActions_Level_" + sceneIndex + "_Task_" + taskNumber);
            gameData.HasTasksCompleted[taskNumber - 1] = true;
        }
    }

    private IEnumerator WaitAndHideTaskPanel_COR()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(UIAnimations.HideTaskPanel_COR());
    }

    private void CloseTask() => Canvas.GetComponent<TaskPanelBehaviour>().CloseTask();

    private void ReturnToScene() => Canvas.GetComponent<TaskPanelBehaviour>().ReturnToScene();

    private void Start()
    {
        gameData = Canvas.GetComponent<GameData>();
        UI = Canvas.GetComponent<InterfaceElements>();
        UIAnimations = Canvas.GetComponent<InterfaceAnimations>();
        sceneIndex = gameData.SceneIndex;
        player = gameData.Player;
        robotBehaviour = player.GetComponent<RobotBehaviour>();
        if (sceneIndex != 0)
        {
            taskTriggers = player.GetComponent<TriggersBehaviour>().TaskTriggers;
            enterTriggers = player.GetComponent<TriggersBehaviour>().EnterTriggers;
            scenarioTriggers = player.GetComponent<TriggersBehaviour>().ScenarioTriggers;
        }
        SwitchObjectsToStartState();
    }

    private void SwitchObjectsToStartState()
    {
        switch (sceneIndex)
        {
            case 3:
                taskTriggers.transform.GetChild(1).gameObject.SetActive(false);
                taskTriggers.transform.GetChild(2).gameObject.SetActive(false);
                taskTriggers.transform.GetChild(4).gameObject.SetActive(false);
                taskTriggers.transform.GetChild(7).gameObject.SetActive(false);
                enterTriggers.transform.GetChild(0).gameObject.SetActive(false);
                enterTriggers.transform.GetChild(2).gameObject.SetActive(false);
                scenarioTriggers.transform.GetChild(1).gameObject.SetActive(false);
                var boards = GameObject.Find("ScriptingBoards");
                var stumps = GameObject.Find("ScriptingStumps");
                var newBridge = GameObject.Find("NewBridge");
                var unearthedItems = GameObject.Find("UnearthedItems");
                var gatesKeys = GameObject.Find("GatesKeys");
                for (var i = 0; i < boards.transform.childCount; i++)
                    boards.transform.GetChild(i).gameObject.SetActive(false);
                for (var i = 0; i < stumps.transform.childCount; i++)
                    stumps.transform.GetChild(i).gameObject.SetActive(false);
                for (var i = 0; i < newBridge.transform.childCount; i++)
                    newBridge.transform.GetChild(i).gameObject.SetActive(false);
                for (var i = 0; i < unearthedItems.transform.childCount; i++)
                    unearthedItems.transform.GetChild(i).gameObject.SetActive(false);
                for (var i = 0; i < gatesKeys.transform.childCount; i++)
                    gatesKeys.transform.GetChild(i).gameObject.SetActive(false);
                break;
        }
    }

    #region Пример
    private IEnumerator MakeActions_Level_НомерУровня_Task_НомерЗадания()             // Шаблон для названия методов
    {
        GameObject.Find("Имя объекта").GetComponent<Animator>().Play("Имя анимации"); // Находим объект по имени как в Иерархии и запускаем анимацию (по имени из Animator Controller-а)
        yield return new WaitForSeconds(2f);                                          // Ставим задержку, равную времени анимации в строчке выше, чтобы анимации проигрывали по очереди. Если надо одновременно - не ставим (задержка в секундах, тип float)
        GameObject.Find("Имя объекта").GetComponent<Animator>().Play("Имя анимации");
        yield return new WaitForSeconds(3f);
        CloseTask();                                                                  // После проигрывания всех анимаций завершаем задание методом CloseTask
                                                                                      // В данном примере будет проиграна некая анимация, через 2 секунды - другая анимация, а ещё через 3 секунды задание завершится
    }
    #endregion

    #region Действия для обучающего уровня
    private IEnumerator MakeActions_Level_Training()
    {
        yield return new WaitForSeconds(1f);
        CloseTask();
        yield return new WaitForSeconds(0.7f);
        gameData.CurrentTaskNumber++;
        UI.ActionButton.GetComponent<ActionButtonBehaviour>().ActivateTask();
    }
    #endregion

    #region Действия для 1-го уровня
    private IEnumerator MakeActions_Level_1_Task_1()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        for (var i = 1; i <= 6; i++)
        {
            GameObject.Find("Flower_" + i).GetComponent<Animator>().Play("ToUp");
            yield return new WaitForSeconds(1.9f);
        }
        CloseTask();
    }

    private IEnumerator MakeActions_Level_1_Task_2()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        var mushroom = GameObject.Find("Mushroom");
        mushroom.GetComponent<Animator>().Play("PickUp");
        yield return new WaitForSeconds(1.95f);
        mushroom.SetActive(false);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_1_Task_3()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        for (var i = 7; i <= 10; i++)
        {
            GameObject.Find("Flower_" + i).GetComponent<Animator>().Play("Move_Flower_" + i);
            yield return new WaitForSeconds(2.2f);
        }
        GameObject.Find("Flower_" + 11).GetComponent<Animator>().Play("Move_Flower_" + 11);
        yield return new WaitForSeconds(5f);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_1_Task_4()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        for (var i = 1; i <= 8; i++)
        {
            GameObject.Find("Rock_" + i).GetComponent<Animator>().Play("Rock_ToUp");
            yield return new WaitForSeconds(1.9f);
        }
        CloseTask();
    }
    #endregion

    #region Действия для 2-го уровня
    private IEnumerator MakeActions_Level_2_Task_1()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        GameObject.Find("GreenLight_1").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_2()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
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
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_3()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        GameObject.Find("GreenLight_2").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_4()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        for (var i = 1; i <= 9; i++)
        {
            var mushroom = GameObject.Find("ScriptingMushroom_" + i).transform.GetChild(0);
            if (i % 2 == 1)
            {
                mushroom.GetComponent<Animator>().Play("MoveMushroom");
                yield return new WaitForSeconds(2f);
            }
            else
            {
                mushroom.GetComponent<Animator>().Play("PutDown_Mushroom_" + i);
                yield return new WaitForSeconds(3f);
            }
        }
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_5()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
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
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_6()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        for (var i = 1; i <= 5; i++)
        {
            GameObject.Find("BridgeFence_" + i).GetComponent<Animator>().Play("Move_BridgeFence_" + i);
            yield return new WaitForSeconds(2.7f);
        }
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_7()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        GameObject.Find("GreenLight_5").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_8()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        GameObject.Find("RedLight_3").GetComponent<Animator>().Play("LightTurnOn");
        GameObject.Find("RedLight_4").GetComponent<Animator>().Play("LightTurnOn");
        GameObject.Find("RedLight_5").GetComponent<Animator>().Play("LightTurnOn");
        GameObject.Find("GreenLight_4").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        CloseTask();
    }
    #endregion

    #region Действия для 3-го уровня
    private IEnumerator MakeActions_Level_3_Task_1()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        var boxes = GameObject.Find("Boxes");
        for (var i = 0; i < boxes.transform.childCount; i++)
        {
            var box = boxes.transform.GetChild(i);
            box.GetChild(0).GetComponent<Animator>().Play("BreakBox");
            yield return new WaitForSeconds(1.833f);
            box.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        ReturnToScene();
        yield return new WaitForSeconds(2f);
        taskTriggers.transform.GetChild(1).gameObject.SetActive(true);
        taskTriggers.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().Play("RotateExclamationMark");
    }

    private IEnumerator MakeActions_Level_3_Task_2()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        var boxes = GameObject.Find("Boxes");
        for (var i = 0; i < 2; i++)
        {
            var box = boxes.transform.GetChild(i).GetChild(0);
            for (var j = 1; j < 3; j++)
            {
                box.GetChild(j).GetChild(0).gameObject.GetComponent<Animator>().Play("PickUpItem");
                yield return new WaitForSeconds(1f);
            }
        }
        GameObject.Find("Hatchet").GetComponent<Animator>().Play("PickUpAxe");
        yield return new WaitForSeconds(2f);
        GameObject.Find("Hatchet").SetActive(false);
        ReturnToScene();
        yield return new WaitForSeconds(2f);
        taskTriggers.transform.GetChild(2).gameObject.SetActive(true);
        taskTriggers.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().Play("RotateExclamationMark");
    }

    private IEnumerator MakeActions_Level_3_Task_3()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        var axe = GameObject.Find("Box_3_Parent").transform.GetChild(0).GetChild(1).gameObject;
        axe.SetActive(true);
        axe.GetComponent<Animator>().Play("UseAxe_Vertical");
        yield return new WaitForSeconds(4f);
        var blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
        UI.BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.5f);
        axe.SetActive(false);
        var logs = GameObject.Find("ScriptingLogs");
        for (var i = 0; i < logs.transform.childCount; i++)
            logs.transform.GetChild(i).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(1.4f);
        UI.BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        ReturnToScene();
    }

    private IEnumerator MakeActions_Level_3_Task_4()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        var axe = GameObject.Find("Box_3_Parent").transform.GetChild(0).GetChild(1).gameObject;
        axe.SetActive(true);
        axe.GetComponent<Animator>().Play("UseAxe_Horizontal");
        yield return new WaitForSeconds(4f);
        var blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
        UI.BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.5f);
        axe.SetActive(false);
        var boards = GameObject.Find("ScriptingBoards");
        var stumps = GameObject.Find("ScriptingStumps");
        var trees = GameObject.Find("ScriptingTrees");
        for (var i = 0; i < boards.transform.childCount; i++)
            boards.transform.GetChild(i).gameObject.SetActive(true);
        for (var i = 0; i < stumps.transform.childCount; i++)
            stumps.transform.GetChild(i).gameObject.SetActive(true);
        for (var i = 0; i < trees.transform.childCount; i++)
            trees.transform.GetChild(i).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(1.4f);
        UI.BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        ReturnToScene();
        yield return new WaitForSeconds(2f);
        taskTriggers.transform.GetChild(4).gameObject.SetActive(true);
        taskTriggers.transform.GetChild(4).GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().Play("RotateExclamationMark");
    }

    private IEnumerator MakeActions_Level_3_Task_5()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        var boards = GameObject.Find("ScriptingBoards");
        for (var i = 0; i < 3; i++)
        {
            boards.transform.GetChild(i).gameObject.GetComponent<Animator>().Play("UseBoard_" + (i + 1));
            yield return new WaitForSeconds(1.5f);
        }
        boards.transform.GetChild(3).gameObject.GetComponent<Animator>().Play("UseBoard_4");
        var blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
        UI.BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("BrokenBridge").SetActive(false);
        GameObject.Find("ScriptingBoards").SetActive(false);
        var newBridge = GameObject.Find("NewBridge");
        for (var i = 0; i < newBridge.transform.childCount; i++)
            newBridge.transform.GetChild(i).gameObject.SetActive(true);
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(1.4f);
        UI.BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Lever").GetComponent<Animator>().Play("ActivateLever");
        yield return new WaitForSeconds(1f);
        newBridge.GetComponent<Animator>().Play("GetBridgeDown");
        yield return new WaitForSeconds(1.5f);
        ReturnToScene();
    }

    private IEnumerator MakeActions_Level_3_Task_6()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        gameData.CurrentSceneCamera.GetComponent<Animator>().Play("CheckAllPlaces");
        yield return new WaitForSeconds(10.5f);
        GameObject.Find("Key_MiniScene_1").GetComponent<Animator>().Play("PickUpKey_1");
        yield return new WaitForSeconds(2f);
        GameObject.Find("Key_MiniScene_1").SetActive(false);
        yield return new WaitForSeconds(2.5f);
        gameData.TaskItemsCount++;
        robotBehaviour.currentMoveSpeed = robotBehaviour.moveSpeed;
        robotBehaviour.currentRotateSpeed = robotBehaviour.rotateSpeed;
        TurnOnScenarioTrigger2_Level_3();
    }

    private IEnumerator MakeActions_Level_3_Task_7()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        Canvas.GetComponent<GameData>().CurrentSceneCamera.GetComponent<Animator>().Play("CheckAllChests");
        yield return new WaitForSeconds(2f);

        var chest1 = GameObject.Find("Chest_1");
        chest1.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("OpenChest");
        yield return new WaitForSeconds(1.5f);
        for (var i = 2; i < chest1.transform.childCount; i++)
            chest1.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Animator>().Play("CheckItem");
        yield return new WaitForSeconds(3f);
        chest1.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("CloseChest");
        yield return new WaitForSeconds(4f);

        var chest2 = GameObject.Find("Chest_2");
        chest2.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("OpenChest");
        yield return new WaitForSeconds(1.5f);
        for (var i = 2; i < chest2.transform.childCount; i++)
            chest2.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Animator>().Play("CheckItem");
        yield return new WaitForSeconds(3f);
        chest2.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("CloseChest");
        yield return new WaitForSeconds(4f);

        var chest3 = GameObject.Find("Chest_3");
        chest3.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("OpenChest");
        yield return new WaitForSeconds(1.5f);
        for (var i = 2; i < chest3.transform.childCount - 1; i++)
            chest3.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Animator>().Play("CheckItem");
        var key = chest3.transform.GetChild(chest3.transform.childCount - 1).GetChild(0).gameObject;
        key.GetComponent<Animator>().Play("PickUpKey_2");
        yield return new WaitForSeconds(4f);
        key.SetActive(false);
        chest3.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("CloseChest");
        yield return new WaitForSeconds(4f);

        Canvas.GetComponent<GameData>().TaskItemsCount++;
        robotBehaviour.currentMoveSpeed = robotBehaviour.moveSpeed;
        robotBehaviour.currentRotateSpeed = robotBehaviour.rotateSpeed;
        TurnOnScenarioTrigger2_Level_3();
    }

    private IEnumerator MakeActions_Level_3_Task_8()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        GameObject.Find("Robot_Digger").GetComponent<Animator>().Play("ActivateDigger");
        yield return new WaitForSeconds(3.8f);
        var blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
        UI.BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(3f);
        var buriedItems = GameObject.Find("BuriedItems");
        var unearthedItems = GameObject.Find("UnearthedItems");
        for (var i = 0; i < buriedItems.transform.childCount; i++)
        {
            buriedItems.transform.GetChild(i).gameObject.SetActive(false);
            unearthedItems.transform.GetChild(i).gameObject.SetActive(true);
        }
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(1.4f);
        UI.BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        var key = unearthedItems.transform.GetChild(0).gameObject;
        key.GetComponent<Animator>().Play("PickUpKey_3");
        yield return new WaitForSeconds(2f);
        key.SetActive(false);
        ReturnToScene();
        gameData.TaskItemsCount++;
        TurnOnScenarioTrigger2_Level_3();
    }

    private void TurnOnScenarioTrigger2_Level_3()
    {
        if (gameData.TaskItemsCount == 3)
        {
            var scenarioTrigger1 = scenarioTriggers.transform.GetChild(0).gameObject;
            var scenarioTrigger2 = scenarioTriggers.transform.GetChild(1).gameObject;
            scenarioTrigger1.SetActive(false);
            scenarioTrigger2.SetActive(true);
            scenarioTrigger2.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("RotateExclamationMark");
        }
    }
    #endregion

    #region Действия для 4-го уровня
    private IEnumerator MakeActions_Level_4_Task_1()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        ReturnToScene();
        yield return new WaitForSeconds(2f);
        var npc = player.GetComponent<VIDEDemoPlayer>().inTrigger.transform.parent;
        npc.GetChild(1).gameObject.SetActive(false);
        npc.GetChild(2).gameObject.SetActive(true);
        npc.GetChild(2).GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        Canvas.GetComponent<VIDEUIManager1>().Interact(player.GetComponent<VIDEDemoPlayer>().inTrigger);
        gameData.Player.GetComponent<TriggersBehaviour>().DeleteActionButton();
        npc.GetChild(2).gameObject.SetActive(false);
    }
    #endregion
}
