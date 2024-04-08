using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SavePoint : MonoBehaviour
{
    DeathManager deathManager;
    ParticleSystem particleSystem;
    public bool showDialog = false;
    public DialogObj dialogObj;
    DialogShower dialogShower;
    PlayerControl pr;
    bool started = false;
    void Start()
    {
        deathManager = GameObject.FindGameObjectWithTag("deathmanager").GetComponent<DeathManager>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        dialogShower = GameObject.FindGameObjectWithTag("dialogshower").GetComponent<DialogShower>();
        pr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    public void activate(){
        deathManager.lastSavePoint = transform.position;
        particleSystem.Play();
        pr.health = pr.maxHealth;
        if(showDialog){
            dialogShower.dialogObj = dialogObj;
            dialogShower.child.SetActive(true);
            dialogShower.Next();
            started = true;
        }
    }
    void Update(){
        if(started && !dialogShower.child.activeSelf){
            showDialog = false;
        }
    }
}
