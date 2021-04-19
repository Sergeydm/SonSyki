using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [Header ("Экраны меню")]
    public GameObject MainMenu;
    public GameObject Levels;
    public GameObject Settings;
    public GameObject Partners;

    [Header ("Для анимаций и переходов")]
    public GameObject MenuCamera;
    public GameObject Robot;
    public GameObject FireLight;
    public GameObject BlackScreen;

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
        MainMenu.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        MenuCamera.GetComponent<Animator>().Play("MoveCamera_1");
        yield return new WaitForSeconds(2f);
        Settings.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator GoTo_Partners_COR()
    {
        MainMenu.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        MenuCamera.GetComponent<Animator>().Play("MoveCamera_2");
        yield return new WaitForSeconds(2f);
        Partners.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator GoTo_Play_COR()
    {
        MainMenu.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        MenuCamera.GetComponent<Animator>().Play("MoveCamera_3");
        yield return new WaitForSeconds(2f);
        Levels.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator ReturnToMainMenuFrom_Settings_COR()
    {
        Settings.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        MenuCamera.GetComponent<Animator>().Play("MoveBackCamera_1");
        yield return new WaitForSeconds(2f);
        MainMenu.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator ReturnToMainMenuFrom_Partners_COR()
    {
        Partners.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        MenuCamera.GetComponent<Animator>().Play("MoveBackCamera_2");
        yield return new WaitForSeconds(2f);
        MainMenu.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator ReturnToMainMenuFrom_Play_COR()
    {
        Levels.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        MenuCamera.GetComponent<Animator>().Play("MoveBackCamera_3");
        yield return new WaitForSeconds(2f);
        MainMenu.GetComponent<Animator>().Play("ScaleInterfaceUp");
    }

    private IEnumerator Start_Level_COR(int levelNumber)
    {
        Levels.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.75f);
        MenuCamera.GetComponent<Animator>().Play("MoveBackCamera_3");
        yield return new WaitForSeconds(2f);
        Robot.GetComponent<Animator>().Play("Walk_MainMenu");
        yield return new WaitForSeconds(5f);
        BlackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(levelNumber);

    }

    private void Start()
    {
        FireLight.GetComponent<Animator>().Play("Fire");
    }
}
