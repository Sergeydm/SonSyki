using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private GameObject player;
    private GameObject camera;
    private GameObject mainMenu;
    private GameObject settings;
    private GameObject urls;
    private GameObject levels;
    private GameObject blackScreen;

    public void GoTo_Settings() => StartCoroutine(GoTo_Settings_COR());

    public void GoTo_Partners() => StartCoroutine(GoTo_Partners_COR());

    public void GoTo_Play() => StartCoroutine(GoTo_Play_COR());

    public void ReturnToMainMenuFrom_Settings() => StartCoroutine(ReturnToMainMenuFrom_Settings_COR());

    public void ReturnToMainMenuFrom_Partners() => StartCoroutine(ReturnToMainMenuFrom_Partners_COR());

    public void ReturnToMainMenuFrom_Play() => StartCoroutine(ReturnToMainMenuFrom_Play_COR());

    public void GoToURL_Financies() => Application.OpenURL("http://13.59.215.174/FiveRaccoons/");
    public void GoToURL_VK_Group() => Application.OpenURL("https://vk.com/iritrtf_urfu");

    public void Start_Level_Training() => StartCoroutine(Start_Level_COR(6));

    public void Start_Level_1() => StartCoroutine(Start_Level_COR(1));

    public void Start_Level_2() => StartCoroutine(Start_Level_COR(2));

    public void Start_Level_3() => StartCoroutine(Start_Level_COR(3));

    public void Start_Level_4() => StartCoroutine(Start_Level_COR(4));

    public void Start_Level_5() => StartCoroutine(Start_Level_COR(5));

    public void Exit() => Application.Quit();

    private IEnumerator GoTo_Settings_COR()
    {
        mainMenu.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        camera.GetComponent<Animator>().Play("MoveCamera_1");
        yield return new WaitForSeconds(2f);
        settings.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator GoTo_Partners_COR()
    {
        mainMenu.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        camera.GetComponent<Animator>().Play("MoveCamera_2");
        yield return new WaitForSeconds(2f);
        urls.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator GoTo_Play_COR()
    {
        mainMenu.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        camera.GetComponent<Animator>().Play("MoveCamera_3");
        yield return new WaitForSeconds(2f);
        levels.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator ReturnToMainMenuFrom_Settings_COR()
    {
        settings.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        camera.GetComponent<Animator>().Play("MoveBackCamera_1");
        yield return new WaitForSeconds(2f);
        mainMenu.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator ReturnToMainMenuFrom_Partners_COR()
    {
        urls.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        camera.GetComponent<Animator>().Play("MoveBackCamera_2");
        yield return new WaitForSeconds(2f);
        mainMenu.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator ReturnToMainMenuFrom_Play_COR()
    {
        levels.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        camera.GetComponent<Animator>().Play("MoveBackCamera_3");
        yield return new WaitForSeconds(2f);
        mainMenu.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator Start_Level_COR(int levelNumber)
    {
        levels.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        camera.GetComponent<Animator>().Play("MoveBackCamera_3");
        yield return new WaitForSeconds(2f);
        player.GetComponent<Animator>().Play("Walk_MainMenu");
        yield return new WaitForSeconds(5f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(levelNumber);

    }

    private void Start()
    {
        player = GameObject.Find("Robot");
        camera = GameObject.Find("Camera");
        mainMenu = GameObject.Find("MainMenu");
        settings = GameObject.Find("Settings");
        urls = GameObject.Find("URLs");
        levels = GameObject.Find("Levels");
        blackScreen = GameObject.Find("BlackScreen");
        GameObject.Find("FireLight").GetComponent<Animator>().Play("Fire");
    }
}
