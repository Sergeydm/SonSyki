using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    [Serializable]
    public class TaskText
    {
        public int ID;
        public string Title;
        public string Description;
        public string ExtendedDescription;
        public string StartCode;
    }

    [Serializable]
    public class Test
    {
        public int TaskNumber;
        public string ExtraCode;
        public string TestCode;
    }

    [Serializable]
    public class Message
    {
        public int LevelNumber;
        public string Title;
        public string Description;
    }

    [Serializable]
    public class ScenarioMessage
    {
        public string Title;
        public string Description;
    }

    [Serializable]
    public class HandbookLetter
    {
        public string Title;
        public string Description;
    }

    [Serializable]
    public class TipMessage
    {
        public string Tip;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array, bool prettyPrint = false)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    [Header("Игрок")]
    public GameObject Player;
    public int CoinsCount;
    public int TipsCount;
    [Header("Текущая включенная камера на сцене")]
    public Camera CurrentSceneCamera;
    [Header("Номер текущего задания")]
    public int CurrentTaskNumber;
    [Header("Номер текущего триггера смены сцены")]
    public int CurrentChangeSceneTriggerNumber;
    [Header("Номер текущего сценарного триггера")]
    public int CurrentScenarioTriggerNumber;
    [Header("Номер текущего триггера сохранения")]
    public int CurrentSaveTriggerNumber;
    [Header("Индекс сцены")]
    public int SceneIndex;
    [HideInInspector]
    public TaskText[] TaskTexts;
    [HideInInspector]
    public Test[] Tests;
    [HideInInspector]
    public ScenarioMessage[] ScenarioMessages;
    [HideInInspector]
    public Message[] StartMessages;
    [HideInInspector]
    public Message[] FinishMessages;
    [HideInInspector]
    public List<HandbookLetter[]> HandbookLetters;
    [HideInInspector]
    public List<TipMessage[]> Tips;
    [HideInInspector]
    [Tooltip("Кол-во предметов, необходимых для прохождения задания")]
    public int TaskItemsCount;
    [HideInInspector]
    public List<bool> HasTasksCompleted;
    [HideInInspector]
    public bool IsTaskStarted;

    private void Awake()
    {
        TaskItemsCount = 0;
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (SceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            SceneIndex = 0;
        IsTaskStarted = SceneIndex == 0;
        GetDataFromFiles();
        HasTasksCompleted = new List<bool>();
        for (var i = 0; i <= TaskTexts.Length; i++)
            HasTasksCompleted.Add(false);
        if (PlayerPrefs.HasKey("CoinsCount"))
        {
            if (PlayerPrefs.HasKey("IsTransitToNextLevel"))
            {
                gameObject.GetComponent<SaveLoad>().Load_NextLevel();
                PlayerPrefs.DeleteAll();
            }
            else
            {
                if (PlayerPrefs.GetInt("SceneIndex") == SceneIndex)
                    gameObject.GetComponent<SaveLoad>().Load();
            }
        }
    }

    private void GetDataFromFiles()
    {
        var tasksFile = Resources.Load<TextAsset>("Tasks/Tasks Level " + SceneIndex);
        var testsFile = Resources.Load<TextAsset>("Tests/Tests Level " + SceneIndex);
        var scenarioMessagesFile = Resources.Load<TextAsset>("Scenario Messages Level " + SceneIndex);
        var startMessagesFile = Resources.Load<TextAsset>("Start Messages");
        var finishMessagesFile = Resources.Load<TextAsset>("Finish Messages");
        TaskTexts = JsonHelper.FromJson<TaskText>(tasksFile.text);
        Tests = JsonHelper.FromJson<Test>(testsFile.text);
        StartMessages = JsonHelper.FromJson<Message>(startMessagesFile.text);
        FinishMessages = JsonHelper.FromJson<Message>(finishMessagesFile.text);
        HandbookLetters = new List<HandbookLetter[]>();
        for (var i = 0; i <= 3; i++)
        {
            var handbookLettersFile = Resources.Load<TextAsset>("Handbook Letters/Handbook Letters Level " + SceneIndex);
            HandbookLetters.Add(JsonHelper.FromJson<HandbookLetter>(handbookLettersFile.text));
        }
        if (SceneIndex > 0)
        {
            Tips = new List<TipMessage[]>();
            for (var i = 1; i <= TaskTexts.Length; i++)
            {
                var tipsFile = Resources.Load<TextAsset>("Tips/Tips Level " + SceneIndex + " Task " + i);
                Tips.Add(JsonHelper.FromJson<TipMessage>(tipsFile.text));
            }
        }
        if (SceneIndex == 3)
            ScenarioMessages = JsonHelper.FromJson<ScenarioMessage>(scenarioMessagesFile.text);
    }
}
