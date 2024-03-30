using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogObj dialogObj;
    DialogShower dialogShower;
    void Start()
    {
        dialogShower = GameObject.FindGameObjectWithTag("dialogshower").GetComponent<DialogShower>();
    }

    public void interact(){
        dialogShower.dialogObj = dialogObj;
        dialogShower.child.SetActive(true);
        dialogShower.Next();
    }
}
