using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCompletingActions : MonoBehaviour
{
    [Header("Èםעונפויס")]
    public GameObject Canvas;

    private int sceneIndex;
    private GameData gameData;

    public void MakeActions()
    {
        switch (sceneIndex)
        {
            case 4:
                StartCoroutine(MakeActions_Level_4_COR());
                break;
        }
    }

    private IEnumerator MakeActions_Level_4_COR()
    {
        var triggerName = gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger.name;
        var triggerNumber = int.Parse(triggerName.Split('_')[1]);
        switch (triggerNumber)
        {
            case 1:
                gameData.CurrentTaskNumber = 1;
                Canvas.GetComponent<ActionButtonBehaviour>().ActivateTask(false);
                yield break;
            case 2:
                yield break;
        }
        
    }

    private void Start()
    {
        gameData = Canvas.GetComponent<GameData>();
        sceneIndex = gameData.SceneIndex;
    }
}
