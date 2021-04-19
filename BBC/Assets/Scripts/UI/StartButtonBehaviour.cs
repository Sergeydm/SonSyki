using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RoslynCSharp;

public class StartButtonBehaviour : MonoBehaviour
{
    public int taskNumber;
    private string robotManagementCode;
    private InputField codeField;
    private InputField resultField;
    private InputField outputField;
    private GameObject robot;
    private GameObject canvas;
    private Button closeTaskButton;

    public void ExecuteCode()
    {
        try
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            switch(sceneIndex)
            {
                case 1:
                    robotManagementCode = GetRobotManagementClass(GetCheckingMethods_Level_1());
                    break;
                case 2:
                    robotManagementCode = GetRobotManagementClass(GetCheckingMethods_Level_2());
                    break;
                case 3:
                    robotManagementCode = GetRobotManagementClass(GetCheckingMethods_Level_3());
                    break;
                case 4:
                    robotManagementCode = GetRobotManagementClass(GetCheckingMethods_Level_4());
                    break;
                case 5:
                    robotManagementCode = GetRobotManagementClass(GetCheckingMethods_Level_5());
                    break;
                case 6:
                    robotManagementCode = GetRobotManagementClass(GetCheckingMethods_Level_Training());
                    break;
            }
            ScriptDomain domain = ScriptDomain.CreateDomain("MyDomain");
            ScriptType type = domain.CompileAndLoadMainSource(robotManagementCode);
            ScriptProxy proxy = type.CreateInstance(robot);
            Tuple<bool, string> result = (Tuple<bool, string>)proxy.Call("isTaskCompleted_" + taskNumber);
            if (result.Item1)
            {
                resultField.text = "<color=green>Задание выполнено!</color>";
                canvas.GetComponent<TaskCompletingActions>().MakeActions(sceneIndex, taskNumber);
                closeTaskButton.transform.localScale = new Vector3(0, 0, 0);
            }
            else resultField.text = "Есть ошибки. Попробуй ещё раз!";
            outputField.text = result.Item2;
        }
        catch
        {
            Debug.Log("Exception!!!");
            resultField.text = "Есть ошибки. Попробуй ещё раз!";
        }
    }

    private void Start()
    {
        codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        resultField = GameObject.Find("ResultField").GetComponent<InputField>();
        outputField = GameObject.Find("OutputField").GetComponent<InputField>();
        closeTaskButton = GameObject.Find("CloseTaskButton").GetComponent<Button>();
        robot = GameObject.Find("robot1");
        canvas = GameObject.Find("Canvas");
    }

    private string GetCheckingMethods_Level_1()
    {
        switch (taskNumber)
        {
            case 1:
                return @"
public Tuple<bool, string> isTaskCompleted_1()
{
    var result = Execute();
    return Tuple.Create(result == 6, ""Выход:  "" + result);
}";
            case 2:
                return @"
public Tuple<bool, string> isTaskCompleted_2()
{
    var result = Execute() * 10000;
    return Tuple.Create(Math.Abs(result - 0.16755) < 1e-4, ""Выход:  "" + result);
}";
            case 3:
                return @"
public Tuple<bool, string> isTaskCompleted_3()
{
    var result = Execute();
    return Tuple.Create(result == 1, ""Выход:  "" + result);
}";
            case 4:
                return @"
public Tuple<bool, string> isTaskCompleted_4()
{
    var result = Execute();
    return Tuple.Create(result == 300, ""Выход:  "" + result);
}";
        }
        return null;
    }

    private string GetCheckingMethods_Level_2()
    {
        switch (taskNumber)
        {
            case 1:
                return @"
public Tuple<bool, string> isTaskCompleted_1()
{
    var result1 = Execute(true);
    var totalResult = result1 == 1;
    var output = totalResult ? ""корректный"" : ""неправильный"";
    return Tuple.Create(totalResult, ""Выход:  "" + output);
}";
            case 2:
                return @"
public Tuple<bool, string> isTaskCompleted_2()
{
    var result1 = Execute(8);
    var result2 = Execute(10);
    var result3 = Execute(5);
    var totalResult = result1 == 1 && result2 == 1 && result3 == 2;
    var output = totalResult ? ""корректный"" : ""неправильный"";
    return Tuple.Create(totalResult, ""Выход:  "" + output);
}";
            case 3:
                return @"
public Tuple<bool, string> isTaskCompleted_3()
{
    var result1 = Execute(true);
    var result2 = Execute(false);
    var totalResult = result1 == 800 && result2 == 1600;
    var output = totalResult ? ""корректный"" : ""неправильный"";
    return Tuple.Create(totalResult, ""Выход:  "" + output);
}";
            case 4:
                return @"
public Tuple<bool, string> isTaskCompleted_4()
{
    var result1 = Execute(0, 7, false, false);
    var result2 = Execute(0, 7, false, true);
    var result3 = Execute(0, 7, true, false);
    var result4 = Execute(0, 5, true, false);
    var result5 = Execute(0, 3, true, true);
    var result6 = Execute(0, 3, true, false);
    var result7 = Execute(0, 3, false, true);
    var result8 = Execute(0, 3, false, false);
    var result9 = Execute(0, 4, true, true);
    var totalResult = result1 == -1 && result2 == -1 && result3 == -1 && result4 == 1 && result5 == -1 && result6 == 1 && result7 == 1 && result8 == 1 && result9 == 1;
    var output = totalResult ? ""корректный"" : ""неправильный"";
    return Tuple.Create(totalResult, ""Выход:  "" + output);
}";
            case 5:
                return @"
public Tuple<bool, string> isTaskCompleted_5()
{
    var result1 = Execute(true, false, false);
    var result2 = Execute(false, true, false);
    var result3 = Execute(false, false, true);
    var totalResult = result1 == 400 && result2 == 800 && result3 == 1200;
    var output = totalResult ? ""корректный"" : ""неправильный"";
    return Tuple.Create(totalResult, ""Выход:  "" + output);
}";
            case 6:
                return @"
public Tuple<bool, string> isTaskCompleted_6()
{
    var result1 = Execute(2);
    var result2 = Execute(3);
    var result3 = Execute(4);
    var result4 = Execute(5);
    var totalResult = !result1 && result2 && result3 && !result4;
    var output = totalResult ? ""корректный"" : ""неправильный"";
    return Tuple.Create(totalResult, ""Выход:  "" + output);
}";
            case 7:
                return @"
public Tuple<bool, string> isTaskCompleted_7()
{
    var result1 = Execute(2);
    var result2 = Execute(6);
    var result3 = Execute(4);
    var result4 = Execute(5);
    var totalResult = result1 == 1 && result2 == 2 && result3 == 3 && result4 == 1;
    var output = totalResult ? ""корректный"" : ""неправильный"";
    return Tuple.Create(totalResult, ""Выход:  "" + output);
}";
            case 8:
                return @"
public Tuple<bool, string> isTaskCompleted_8()
{
    var result1 = Execute(3);
    var result2 = Execute(5);
    var result3 = Execute(7);
    var result4 = Execute(9);
    var totalResult = result1 == 1 && result2 == 2 && result3 == 3 && result4 == 4;
    var output = totalResult ? ""корректный"" : ""неправильный"";
    return Tuple.Create(totalResult, ""Выход:  "" + output);
}";
        }
        return null;
    }

    private string GetCheckingMethods_Level_3()
    {
        return null;
    }

    private string GetCheckingMethods_Level_4()
    {
        return null;
    }

    private string GetCheckingMethods_Level_5()
    {
        return null;
    }

    private string GetCheckingMethods_Level_Training()
    {
        switch (taskNumber)
        {
            case 1:
                return @"
public Tuple<bool, string> isTaskCompleted_1()
{
    var result = Execute();
    return Tuple.Create(result == 4, ""Выход:  "" + result);
}";
            case 2:
                return @"
public Tuple<bool, string> isTaskCompleted_2()
{
    var result = Execute();
    return Tuple.Create(result == 0, ""Выход:  "" + result);
}";
            case 3:
                return @"
public Tuple<bool, string> isTaskCompleted_3()
{
    var result = Execute();
    return Tuple.Create(result == 2, ""Выход:  "" + result);
}";
            case 4:
                return @"
public Tuple<bool, string> isTaskCompleted_4()
{
    var result = Execute();
    return Tuple.Create(Math.Abs(result - 1000000014.94159) < 1e-7, ""Выход:  "" + result);
}";
        }
        return null;
    }

    private string GetRobotManagementClass(string checkingMethods)
    {
        return @"
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RobotManagementClass : MonoBehaviour
{" + 
   codeField.text + 
   checkingMethods + @"
}";
    }
}
