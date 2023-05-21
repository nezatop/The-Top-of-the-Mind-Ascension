using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public static Action EndDialog;

    [SerializeField] private TextMeshProUGUI QuestionText;
    [SerializeField] private Image Icon;

    private List<string> DialogTextList = new List<string>();
    private int LocalDialogNumber = 0;

    private void OnEnable()
    {
        NPC.StartDialog += LoadDialog;
        NPC.SetIcon += SetIcon;
    }
    private void OnDisable()
    {
        NPC.StartDialog -= LoadDialog;
        NPC.SetIcon -= SetIcon;
    }

    private void LoadDialog(int DialogNumber, string path)
    {
        //string[] lines = File.ReadAllLines(path + "D (" + DialogNumber + ").txt");
        string[] lines = Resources.Load<TextAsset>(path + "D (" + DialogNumber + ")").text.Split('\n');

        foreach (string line in lines)
        {
            DialogTextList.Add(line);
        }

        QuestionText.text = DialogTextList[LocalDialogNumber];
    }

    private void SetIcon(Sprite I)
    {
        Icon.sprite = I;
    }

    public void ClickNext()
    {
        if (LocalDialogNumber + 1 < DialogTextList.Count)
            NextDialog();
        else
        {
            QuestionText.text = "";
            LocalDialogNumber = 0;
            DialogTextList.Clear();
            EndDialog?.Invoke();
        }

    }

    private void NextDialog()
    {
        LocalDialogNumber++;
        QuestionText.text = DialogTextList[LocalDialogNumber];
    }
}
