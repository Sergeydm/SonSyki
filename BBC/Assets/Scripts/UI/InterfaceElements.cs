using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceElements : MonoBehaviour
{
    [Header ("������")]
    [Tooltip ("������ ��������� �������")]
    public Button ActivateTaskButton;

    [Header ("������ �������")]
    [Tooltip("������ �������")]
    public GameObject TaskPanel;
    [Tooltip("�������� �������")]
    public Text TaskTitle;
    [Tooltip("�������� �������")]
    public Text TaskDescription;
    [Tooltip("������ ��� ��������� ������ ���. ���������� � �������")]
    public Button TaskInfoButton;
    [Tooltip("������ ��� �������� �������")]
    public Button CloseTaskButton;

    [Header("������ ������� (�����������)")]
    [Tooltip("������ ������� (�����������)")]
    public GameObject ExtendedTaskPanel;
    [Tooltip("�������� ������� (�����������)")]
    public Text ExtendedTaskTitle;
    [Tooltip("�������� ������� (�����������)")]
    public Text ExtendedTaskDescription;
    [Tooltip("��������� ��� ��������� ��������")]
    public Scrollbar ExtendedTaskDescriptionScrollbar;
    [Tooltip("������ �������� �� ��������� �������")] 
    public Button NextLevelButton;
    [Tooltip("������ ��� �������� ������������ �������� �������")]
    public Button CloseExtendedTaskButton;

    [Header("�������")]
    [Tooltip("�������")]
    public GameObject Pad;
    [Tooltip("���� ��� ����� ����")]
    public InputField CodeField;
    [Tooltip("���� ��� ������ ���������� ���������� �������")]
    public Text ResultField;
    [Tooltip("���� ��� ������ ������ ��������� (���������� ��� ���)")]
    public Text OutputField;
    [Tooltip("������ ������� ���������")]
    public Button StartButton;
    [Tooltip("������ ������ ���� � ���������� ���������")]
    public Button ResetButton;
    [Tooltip("������ ���������� ��������")]
    public Button PowerButton;

    [Header("������ ������ � ����")]
    [Tooltip("������ ������ � ����")]
    public GameObject ExitToMenuPanel;

    [Header("׸���� ����� (���������)")]
    [Tooltip("׸���� ����� (���������)")]
    public GameObject BlackScreen;
}
