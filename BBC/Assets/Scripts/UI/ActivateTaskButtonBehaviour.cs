using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTaskButtonBehaviour : MonoBehaviour
{
    [Header ("Игрок")]
    public GameObject Player;
    [Header ("Интерфейс")]
    public GameObject Canvas;

    private RobotBehaviour robotBehaviour;

    public void ActivateTask()
    {
        var currentTaskNumber = Canvas.GetComponent<GameData>().currentTaskNumber;
        Canvas.GetComponent<TaskPanelBehaviour>().taskNumber = currentTaskNumber;
        robotBehaviour.currentMoveSpeed = robotBehaviour.freezeSpeed;
        robotBehaviour.currentRotateSpeed = robotBehaviour.freezeSpeed;
        StartCoroutine(TurnOnTaskCamera_COR(currentTaskNumber));
    }

    private IEnumerator TurnOnTaskCamera_COR(int currentTaskNumber)
    {
        gameObject.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        if (currentTaskNumber <= Canvas.GetComponent<TaskPanelBehaviour>().tasksCount)
        {
            var currentCamera = Canvas.GetComponent<GameData>().currentSceneCamera;
            var currentCameraName = currentCamera.gameObject.name;
            if (currentCameraName.StartsWith("SceneCamera"))
            {
                var currentCameraNumber = int.Parse(currentCameraName[currentCameraName.Length - 1].ToString());
                if (char.IsDigit(currentCameraName[currentCameraName.Length - 2]))
                    currentCameraNumber += 10 * int.Parse(currentCameraName[currentCameraName.Length - 2].ToString());
                currentCamera.GetComponent<Animator>().Play("MoveToTask_" + currentTaskNumber + "_SceneCamera_" + currentCameraNumber);
                yield return new WaitForSeconds(2f);
            }
        }
        Canvas.GetComponent<TaskPanelBehaviour>().ChangeTask();
    }

    private void Awake()
    {
        robotBehaviour = Player.GetComponent<RobotBehaviour>();
    }
}
