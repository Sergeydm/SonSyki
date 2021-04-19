using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceElements : MonoBehaviour
{
    [Header ("Кнопки")]
    [Tooltip ("Кнопка активации задания")]
    public Button ActivateTaskButton;

    [Header ("Панель задания")]
    [Tooltip("Панель задания")]
    public GameObject TaskPanel;
    [Tooltip("Название задания")]
    public Text TaskTitle;
    [Tooltip("Описание задания")]
    public Text TaskDescription;
    [Tooltip("Кнопка для получения полной доп. информации о задании")]
    public Button TaskInfoButton;
    [Tooltip("Кнопка для закрытия задания")]
    public Button CloseTaskButton;

    [Header("Панель задания (расширенная)")]
    [Tooltip("Панель задания (расширенная)")]
    public GameObject ExtendedTaskPanel;
    [Tooltip("Название задания (расширенное)")]
    public Text ExtendedTaskTitle;
    [Tooltip("Описание задания (расширенное)")]
    public Text ExtendedTaskDescription;
    [Tooltip("Скроллбар для прокрутки описания")]
    public Scrollbar ExtendedTaskDescriptionScrollbar;
    [Tooltip("Кнопка перехода на следующий уровень")] 
    public Button NextLevelButton;
    [Tooltip("Кнопка для закрытия расширенного описания задания")]
    public Button CloseExtendedTaskButton;

    [Header("Планшет")]
    [Tooltip("Планшет")]
    public GameObject Pad;
    [Tooltip("Поле для ввода кода")]
    public InputField CodeField;
    [Tooltip("Поле для вывода результата выполнения задания")]
    public Text ResultField;
    [Tooltip("Поле для вывода выхода программы (корректный или нет)")]
    public Text OutputField;
    [Tooltip("Кнопка запуска программы")]
    public Button StartButton;
    [Tooltip("Кнопка сброса кода к начальному состоянию")]
    public Button ResetButton;
    [Tooltip("Кнопка выключения планшета")]
    public Button PowerButton;

    [Header("Панель выхода в меню")]
    [Tooltip("Панель выхода в меню")]
    public GameObject ExitToMenuPanel;

    [Header("Чёрный экран (контейнер)")]
    [Tooltip("Чёрный экран (контейнер)")]
    public GameObject BlackScreen;

}
