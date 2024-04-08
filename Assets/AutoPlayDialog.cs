using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoPlayDialog : MonoBehaviour
{
    public DialogObj dialogObj;
    DialogShower dialogShower;
    public bool isFinal = false;
    PlayerInput playerInput;
    void Start()
    {
        dialogShower = GameObject.FindGameObjectWithTag("dialogshower").GetComponent<DialogShower>();
        interact();
        playerInput = GetComponent<PlayerInput>();
        
    }
    void Update(){
        if(!dialogShower.child.activeSelf){
            this.enabled = false;
        } else if(playerInput.actions["jump"].WasPerformedThisFrame()){
            dialogShower.Next();
        }
    }
    public void interact(){
        dialogShower.dialogObj = dialogObj;
        dialogShower.child.SetActive(true);
        dialogShower.Next();
    }
}
