using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RespawnButton : MonoBehaviour
{
    DeathManager deathManager;
    void Start()
    {
        deathManager = GameObject.FindGameObjectWithTag("deathmanager").GetComponent<DeathManager>();
        GetComponent<Button>().onClick.AddListener(deathManager.Respawn);
        
    }
    void OnEnable(){
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
