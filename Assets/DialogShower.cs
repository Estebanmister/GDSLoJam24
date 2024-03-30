using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogShower : MonoBehaviour
{
    public Image convo;
    public TMP_Text name;
    public TMP_Text dialog;
    public bool startconvo = false;
    public GameObject child;
    public DialogObj dialogObj;
    int i = 0;

    public void Next(){
        if(i >= dialogObj.textLines.Length){
            child.SetActive(false);
            i = 0;
            Time.timeScale = 1;
            return;
        }
        Time.timeScale = 0;
        name.text = dialogObj.names[i];
        dialog.text = dialogObj.textLines[i];
        convo.sprite = dialogObj.portraits[i];
        i += 1;
    }
}
