using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    DeathManager deathManager;
    ParticleSystem particleSystem;
    void Start()
    {
        deathManager = GameObject.FindGameObjectWithTag("deathmanager").GetComponent<DeathManager>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void activate(){
        deathManager.lastSavePoint = transform.position;
        particleSystem.Play();
    }
}
