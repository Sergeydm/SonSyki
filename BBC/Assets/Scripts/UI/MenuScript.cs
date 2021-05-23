﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [Header ("Для анимаций и переходов")]
    public GameObject MenuCamera;
    public GameObject Robot;
    public GameObject FireLight;
    public GameObject BlackScreen;

    public void GoTo_Settings() => StartCoroutine(GoTo_Settings_COR());

    public void GoTo_Partners() => StartCoroutine(GoTo_Partners_COR());

    public void GoTo_Levels() => StartCoroutine(GoTo_Levels_COR());

    public void ReturnToMainMenuFrom_Settings() => StartCoroutine(ReturnToMainMenu_Settings_COR());

    public void ReturnToMainMenuFrom_Partners() => StartCoroutine(ReturnToMainMenu_Partners_COR());

    public void ReturnToMainMenuFrom_Levels() => StartCoroutine(ReturnToMainMenu_Levels_COR());

    public void GoToURL_Financies() => Application.OpenURL("http://13.59.215.174/FiveRaccoons/");
    public void GoToURL_VK_Group() => Application.OpenURL("https://vk.com/iritrtf_urfu");

    public void Start_Level_Training() => StartCoroutine(Start_Level_COR(SceneManager.sceneCountInBuildSettings - 1));

    public void Start_Level_1() => StartCoroutine(Start_Level_COR(1));

    public void Start_Level_2() => StartCoroutine(Start_Level_COR(2));

    public void Start_Level_3() => StartCoroutine(Start_Level_COR(3));

    public void Start_Level_4() => StartCoroutine(Start_Level_COR(4));

    public void Start_Level_5() => StartCoroutine(Start_Level_COR(5));

    public void Exit() => Application.Quit();

    private IEnumerator GoTo_Settings_COR()
    {
        yield return StartCoroutine(ChangeMenuChapter_COR(1));
        for (var i = 1; i <= 2; i++)
        {
            GameObject.Find("SettingsBackground_Part_" + i).GetComponent<Animator>().Play("DrawChapter");
            yield return new WaitForSeconds(0.15f);
        }
        GameObject.Find("SettingsBackground_BackToMenu").GetComponent<Animator>().Play("DrawChapter");
        GameObject.Find("SettingsButtons").GetComponent<Animator>().Play("MoveSettingsDown");
        GameObject.Find("BackToMainMenuButton_Settings").GetComponent<Animator>().Play("MoveBackToMenuButtonUp");
    }

    private IEnumerator GoTo_Partners_COR()
    {
        yield return StartCoroutine(ChangeMenuChapter_COR(2));
        for (var i = 1; i <= 2; i++)
        {
            GameObject.Find("PartnersBackground_Part_" + i).GetComponent<Animator>().Play("DrawChapter");
            yield return new WaitForSeconds(0.15f);
        }
        GameObject.Find("PartnersBackground_BackToMenu").GetComponent<Animator>().Play("DrawChapter");
        GameObject.Find("PartnersPanel").GetComponent<Animator>().Play("MovePartnersDown");
        GameObject.Find("BackToMainMenuButton_Partners").GetComponent<Animator>().Play("MoveBackToMenuButtonUp");
    }

    private IEnumerator GoTo_Levels_COR()
    {
        yield return StartCoroutine(ChangeMenuChapter_COR(3));
        for (var i = 1; i <= 7; i++)
        {
            GameObject.Find("LevelsBackground_Part_" + i).GetComponent<Animator>().Play("DrawChapter");
            yield return new WaitForSeconds(0.15f);
        }
        GameObject.Find("LevelsBackground_BackToMenu").GetComponent<Animator>().Play("DrawChapter");
        GameObject.Find("LevelsScrollArea").GetComponent<Animator>().Play("MoveLevelsDown");
        GameObject.Find("BackToMainMenuButton_Levels").GetComponent<Animator>().Play("MoveBackToMenuButtonUp");
    }

    private IEnumerator ReturnToMainMenu_Settings_COR()
    {
        GameObject.Find("SettingsBackground_BackToMenu").GetComponent<Animator>().Play("EraseChapter");
        GameObject.Find("SettingsButtons").GetComponent<Animator>().Play("MoveSettingsUp");
        GameObject.Find("BackToMainMenuButton_Settings").GetComponent<Animator>().Play("MoveBackToMenuButtonDown");
        yield return new WaitForSeconds(0.5f);
        for (var i = 2; i >= 1; i--)
        {
            GameObject.Find("SettingsBackground_Part_" + i).GetComponent<Animator>().Play("EraseChapter");
            yield return new WaitForSeconds(0.15f);
        }
        yield return StartCoroutine(ReturnToMainMenu_COR(1));
    }

    private IEnumerator ReturnToMainMenu_Partners_COR()
    {
        GameObject.Find("PartnersBackground_BackToMenu").GetComponent<Animator>().Play("EraseChapter");
        GameObject.Find("PartnersPanel").GetComponent<Animator>().Play("MovePartnersUp");
        GameObject.Find("BackToMainMenuButton_Partners").GetComponent<Animator>().Play("MoveBackToMenuButtonDown");
        yield return new WaitForSeconds(0.5f);
        for (var i = 2; i >= 1; i--)
        {
            GameObject.Find("PartnersBackground_Part_" + i).GetComponent<Animator>().Play("EraseChapter");
            yield return new WaitForSeconds(0.15f);
        }
        yield return StartCoroutine(ReturnToMainMenu_COR(2));
    }

    private IEnumerator ReturnToMainMenu_Levels_COR()
    {
        GameObject.Find("LevelsBackground_BackToMenu").GetComponent<Animator>().Play("EraseChapter");
        GameObject.Find("LevelsScrollArea").GetComponent<Animator>().Play("MoveLevelsUp");
        GameObject.Find("BackToMainMenuButton_Levels").GetComponent<Animator>().Play("MoveBackToMenuButtonDown");
        yield return new WaitForSeconds(0.5f);
        for (var i = 7; i >= 1; i--)
        {
            GameObject.Find("LevelsBackground_Part_" + i).GetComponent<Animator>().Play("EraseChapter");
            yield return new WaitForSeconds(0.15f);
        }
        yield return StartCoroutine(ReturnToMainMenu_COR(3));
    }

    private IEnumerator ReturnToMainMenu_COR(int cameraNumber)
    {
        MenuCamera.GetComponent<Animator>().Play("MoveBackCamera_" + cameraNumber);
        yield return new WaitForSeconds(2f);
        for (var i = 1; i <= 5; i++)
        {
            GameObject.Find("MainMenuBackground_Part_" + i).GetComponent<Animator>().Play("DrawMainMenu");
            yield return new WaitForSeconds(0.15f);
        }
        GameObject.Find("Content").GetComponent<Animator>().Play("MoveMainMenuDown");
    }

    private IEnumerator ChangeMenuChapter_COR(int cameraNumber)
    {
        GameObject.Find("Content").GetComponent<Animator>().Play("MoveMainMenuUp");
        yield return new WaitForSeconds(0.5f);
        for (var i = 5; i >= 1; i--)
        {
            GameObject.Find("MainMenuBackground_Part_" + i).GetComponent<Animator>().Play("EraseMainMenu");
            yield return new WaitForSeconds(0.15f);
        }
        MenuCamera.GetComponent<Animator>().Play("MoveCamera_" + cameraNumber);
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator Start_Level_COR(int levelNumber)
    {
        GameObject.Find("LevelsBackground_BackToMenu").GetComponent<Animator>().Play("EraseChapter");
        GameObject.Find("LevelsScrollArea").GetComponent<Animator>().Play("MoveLevelsUp");
        GameObject.Find("BackToMainMenuButton_Levels").GetComponent<Animator>().Play("MoveBackToMenuButtonDown");
        yield return new WaitForSeconds(0.5f);
        for (var i = 7; i >= 1; i--)
        {
            GameObject.Find("LevelsBackground_Part_" + i).GetComponent<Animator>().Play("EraseChapter");
            yield return new WaitForSeconds(0.15f);
        }
        MenuCamera.GetComponent<Animator>().Play("MoveBackCamera_3");
        yield return new WaitForSeconds(2f);
        Robot.GetComponent<Animator>().Play("Walk_MainMenu");
        yield return new WaitForSeconds(5f);
        BlackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(levelNumber);
        yield break;
    }

    private void Start()
    {
        FireLight.GetComponent<Animator>().Play("Fire");
    }
}
