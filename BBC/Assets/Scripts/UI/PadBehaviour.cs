using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadBehaviour : MonoBehaviour
{
    public enum PadMode
    {
        Normal,
        Development,
        Handbook_MainThemes,
        Handbook_SubThemes,
        Handbook_ProgrammingInfo
    }

    [Header("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public string StartCode;
    [HideInInspector]
    public PadMode Mode;
    [HideInInspector]
    public int firstThemeToLockNumber;
    [HideInInspector]
    public List<int> availableTipsCounts;

    private InterfaceElements UI;
    private RobotBehaviour robotBehaviour;
    private GameData gameData;
    private int themeNumber;
    private int taskNumber;
    private bool isPadCalled;

    public void ResetCode() => UI.CodeField.text = StartCode;

    public void SwitchToDevMode()
    {
        UI.Pad.GetComponentInParent<Animator>().Play("SwitchToDevMode");
        Mode = PadMode.Development;
    }
    
    public void SwitchToHandbookMode() => StartCoroutine(SwitchToHandbookMode_COR());

    public void ReturnToMenuFromDevMode() => StartCoroutine(ReturnToMenuFromDevMode_COR());

    public void ReturnToMenuFromHandbookMode() => StartCoroutine(ReturnToMenuFromHandbookMode_COR());

    #region Выбор темы из главного меню справочника

    public void ShowSubThemes_Theme_1() => StartCoroutine(ShowSubThemes_COR(1));

    public void ShowSubThemes_Theme_2() => StartCoroutine(ShowSubThemes_COR(2));

    public void ShowSubThemes_Theme_3() => StartCoroutine(ShowSubThemes_COR(3));
    
    #endregion

    #region Выбор раздела в каждой из тем, представленных в справочнике
    
    public void ShowProgrammingInfo_SubTheme_1() => StartCoroutine(ShowProgrammingInfo_COR(1));

    public void ShowProgrammingInfo_SubTheme_2() => StartCoroutine(ShowProgrammingInfo_COR(2));

    public void ShowProgrammingInfo_SubTheme_3() => StartCoroutine(ShowProgrammingInfo_COR(3));

    #endregion

    public void ShowHelpPanel()
    {
        if (availableTipsCounts[taskNumber - 1] == gameData.Tips[taskNumber - 1].Length)
            UI.Tip.text = "";
        else
        {
            var tipNumber = gameData.Tips[taskNumber - 1].Length - availableTipsCounts[taskNumber - 1] - 1;
            UI.Tip.text = gameData.Tips[taskNumber - 1][tipNumber].Tip;
        }
        UI.HelpPanel.GetComponent<Animator>().Play("ScaleUp"); 
    }

    public void CloseHelpPanel() => UI.HelpPanel.GetComponent<Animator>().Play("ScaleDown");

    public void ReturnToPreviousPage() => StartCoroutine(ReturnToPreviousPage_COR());

    public void UnlockProgrammingInfo(int chapterNumber)
    {
        var buttonToUnlock = UI.SubThemeButtons.transform.GetChild(firstThemeToLockNumber - 2).GetChild(chapterNumber - 1).gameObject.GetComponent<Button>();
        buttonToUnlock.interactable = true;
        buttonToUnlock.GetComponentInChildren<Text>().text = gameData.HandbookLetters[firstThemeToLockNumber - 1][chapterNumber - 1].Title;
    }

    public void ShowTip()
    {
        gameData.TipsCount--;
        if (UI.TipFiller.IsActive())
            UI.TipFiller.gameObject.SetActive(false);
        var tipNumber = gameData.Tips[taskNumber - 1].Length - availableTipsCounts[taskNumber - 1];
        UI.Tip.text = gameData.Tips[taskNumber - 1][tipNumber].Tip;
        availableTipsCounts[taskNumber - 1]--;
    }

    public void BuyTip()
    {
        gameData.TipsCount++;
        gameData.CoinsCount -= 3;
    }

    public void BuyManyTips()
    {
        gameData.TipsCount += 3;
        gameData.CoinsCount -= 8;
    }

    private IEnumerator ShowSubThemes_COR(int themeNumber)
    {
        UI.ThemeButtons.GetComponent<Animator>().Play("ScaleDown");
        yield return new WaitForSeconds(0.45f);
        UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleUp");
        UI.PreviousHandbookPageButton.transform.parent.gameObject.SetActive(true);
        this.themeNumber = themeNumber;
        Mode = PadMode.Handbook_SubThemes;
    }

    private IEnumerator ShowProgrammingInfo_COR(int subThemeNumber)
    {
        var handbookLetter = gameData.HandbookLetters[themeNumber - 1][subThemeNumber - 1];
        UI.ProgrammingInfo.text = handbookLetter.Description;
        UI.ProgrammingInfoTitle.text = handbookLetter.Title;
        UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleDown");
        yield return new WaitForSeconds(0.45f);
        UI.ProgrammingInfo.GetComponentInParent<Animator>().Play("ScaleUp");
        UI.ProgrammingInfoTitle.GetComponent<Animator>().Play("ScaleUp");
        Mode = PadMode.Handbook_ProgrammingInfo;
    }

    private IEnumerator SwitchToHandbookMode_COR()
    {
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        UI.PreviousHandbookPageButton.transform.parent.gameObject.SetActive(false);
        UI.Pad.transform.parent.parent.gameObject.GetComponent<Animator>().Play("SwitchToHandbookMode");
        if (gameData.IsTaskStarted)
        {
            UI.TaskPanel.GetComponent<Animator>().Play("MoveLeft_TaskPanel");
            yield return new WaitForSeconds(0.7f);
            yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().EraseTaskPanelBackground_COR());
        }
        Mode = PadMode.Handbook_MainThemes;
    }

    private IEnumerator ReturnToMenuFromDevMode_COR()
    {
        UI.Pad.GetComponentInParent<Animator>().Play("ReturnToMenuFromDevMode");
        yield return new WaitForSeconds(1f);
        UI.HelpPanel.GetComponent<Animator>().Play("ScaleDown_Quick");
        Mode = PadMode.Normal;
    }

    private IEnumerator ReturnToMenuFromHandbookMode_COR()
    {
        UI.Pad.GetComponentInParent<Animator>().Play("ReturnToMenuFromHandbookMode");
        yield return new WaitForSeconds(0.83f);
        UI.ThemeButtons.GetComponent<Animator>().Play("ScaleUp");
        for (var i = 0; i < UI.SubThemeButtons.transform.childCount; i++)
            UI.SubThemeButtons.transform.GetChild(i).gameObject.GetComponent<Animator>().Play("ScaleDown");
        UI.ProgrammingInfo.GetComponentInParent<Animator>().Play("ScaleDown");
        UI.ProgrammingInfoTitle.GetComponent<Animator>().Play("ScaleDown");
        if (gameData.IsTaskStarted)
        {
            yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().DrawTaskPanelBackground_COR());
            UI.TaskPanel.GetComponent<Animator>().Play("MoveRight_TaskPanel");
            yield return new WaitForSeconds(0.7f);
            UI.CloseTaskButton.transform.localScale = new Vector3(1, 1, 1);
        }
        Mode = PadMode.Normal;
    }

    private IEnumerator ReturnToPreviousPage_COR()
    {
        switch (Mode)
        {
            case PadMode.Handbook_SubThemes:
                UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleDown");
                yield return new WaitForSeconds(0.45f);
                UI.ThemeButtons.GetComponent<Animator>().Play("ScaleUp");
                UI.PreviousHandbookPageButton.transform.parent.gameObject.SetActive(false);
                Mode = PadMode.Handbook_MainThemes;
                break;
            case PadMode.Handbook_ProgrammingInfo:
                UI.ProgrammingInfo.GetComponentInParent<Animator>().Play("ScaleDown");
                UI.ProgrammingInfoTitle.GetComponent<Animator>().Play("ScaleDown");
                yield return new WaitForSeconds(0.45f);
                UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleUp");
                Mode = PadMode.Handbook_SubThemes;
                break;
        }
    }  

    private void LockThemes()
    {       
        switch (gameData.SceneIndex)
        {
            case 0:
                firstThemeToLockNumber = 1;
                break;
            case 1:
                firstThemeToLockNumber = 1;
                break;
            case 2:
                firstThemeToLockNumber = 2;
                break;
            case 3:
                firstThemeToLockNumber = 3;
                break;
            case 4:
                firstThemeToLockNumber = 3; // потом поменять на 4
                break;
            default:
                firstThemeToLockNumber = int.MaxValue;
                break;
        }
        for (var i = firstThemeToLockNumber; i < UI.ThemeButtons.transform.childCount; i++)
        {
            var themeButton = UI.ThemeButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
            themeButton.interactable = false;
            themeButton.GetComponentInChildren<Text>().text = "???";
        }
        var newProgrammingInfoButtons = UI.SubThemeButtons.transform.GetChild(firstThemeToLockNumber - 1).gameObject;
        for (var i = 0; i < newProgrammingInfoButtons.transform.childCount; i++)
        {
            var programmingInfoButton = newProgrammingInfoButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
            programmingInfoButton.interactable = false;
            programmingInfoButton.GetComponentInChildren<Text>().text = "???";
        }
        for (var i = 0; i < firstThemeToLockNumber - 1; i++)
        {
            for (var j = 0; j < UI.SubThemeButtons.transform.GetChild(i).childCount; j++)
                UI.SubThemeButtons.transform.GetChild(i).GetChild(j).GetComponentInChildren<Text>().text = gameData.HandbookLetters[i][j].Title;
        }
    }

    private void CallPad()
    {
        if (!isPadCalled)
        {
            robotBehaviour.FreezePlayer();
            UI.Pad.GetComponentInParent<Animator>().Play("MoveLeft_Pad");
        }
        else
        {
            robotBehaviour.UnfreezePlayer();
            UI.Pad.GetComponentInParent<Animator>().Play("MoveRight_Pad");
        }
        isPadCalled = !isPadCalled;
        
    }

    private void Update()
    {
        taskNumber = gameData.CurrentTaskNumber;
        UI.CoinsCounter.text = UI.CoinsMenuCounter.text = gameData.CoinsCount.ToString();
        UI.TipsCounter.text = UI.TipsMenuCounter.text = gameData.TipsCount.ToString();
        UI.BuyTipButton.interactable = gameData.CoinsCount >= 3;
        UI.BuyManyTipsButton.interactable = gameData.CoinsCount >= 8;
        UI.ShowTipButton.interactable = gameData.TipsCount > 0 && availableTipsCounts[taskNumber - 1] > 0;
        if (gameData.SceneIndex > 0 && taskNumber > 0 && taskNumber < availableTipsCounts.Count)
            UI.ShowTipButton.GetComponentInChildren<Text>().text = "Получить подсказку (Осталось: " + availableTipsCounts[taskNumber - 1] + ")";
        if (Input.GetKeyDown(KeyCode.P))
            CallPad();
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        robotBehaviour = gameData.Player.GetComponent<RobotBehaviour>();
        Mode = PadMode.Normal;
        isPadCalled = false;
        UI.IDEButton.interactable = gameData.SceneIndex == 0;
        LockThemes();
        availableTipsCounts = new List<int>();
        if (gameData.SceneIndex > 0)
        {
            for (var i = 0; i < gameData.TaskTexts.Length; i++)
                availableTipsCounts.Add(gameData.Tips[i].Length);
        }
        if (PlayerPrefs.HasKey("PositionX"))
        {
            for (var i = 0; i < availableTipsCounts.Count; i++)
                availableTipsCounts[i] = PlayerPrefs.GetInt("Available Tips Count (Task " + (i + 1) + ")");
        }
        if (gameData.SceneIndex != 0 && gameData.SceneIndex != 4)
            Canvas.GetComponent<SaveLoad>().Save();
    }
}
