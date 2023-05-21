using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public static Action OpenDialogMenu;
    public static Action<Sprite> SetIcon;
    public static Action<int, string> StartDialog;

    [SerializeField] private Sprite Icon;

    public String FolderName;
    public int DialogNumber = 1;
    public int DialogMaxNumber = 4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && DialogNumber <= DialogMaxNumber)
        {
            OpenDialogMenu?.Invoke();
            SetIcon?.Invoke(Icon);
            StartDialog?.Invoke(DialogNumber, "Dialog/" + FolderName + "/");
            DialogNumber++;
        }
    }
}
