using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenuPanelButtonsBehaviour : MonoBehaviour
{
    public void ActivatePanel()
    {
        var panel = GameObject.Find("Panel_ExitToMenu");
        panel.transform.position = panel.GetComponent<ExitToMenuPanelBehaviour>().TurnOnPosition;
        GameObject.Find("Snowman").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReturnToLevel()
    {
        var panel = GameObject.Find("Panel_ExitToMenu");
        panel.transform.position = panel.GetComponent<ExitToMenuPanelBehaviour>().TurnOffPosition;
        GameObject.Find("Snowman").GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
    }
}
