using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogObj dialogObj;
    DialogShower dialogShower;
    public bool isFinal = false;
    public bool sign = false;
    bool triggered = false;
    public bool finalBossfight = false;
    public GameObject finalBoss;
    public string choiceText;
    void Start()
    {
        dialogShower = GameObject.FindGameObjectWithTag("dialogshower").GetComponent<DialogShower>();
    }

    public void interact(){
        if(sign && dialogObj == null){
            // go to the next level (bad and good should be the same if this is being triggered, its a linear section)
            GetComponent<nextlevel>().nextGoodLevel();
            return;
        }
        dialogShower.dialogObj = dialogObj;
        dialogShower.child.SetActive(true);
        dialogShower.Next();
        dialogShower.choiceTXT.text = choiceText;
        if(isFinal){
            dialogShower.shouldDoChoice = true;
        }
        triggered = true;
    }
    void Update(){
        if(triggered && !dialogShower.child.activeSelf && sign){
            //dialog has ended, i call this good level but really is just any level
            dialogShower.GetComponent<nextlevel>().nextGoodLevel();
        }
        if(triggered && !dialogShower.child.activeSelf && finalBossfight){
            //oh shit
            finalBoss.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
