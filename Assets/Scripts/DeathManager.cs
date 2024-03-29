using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    Animator deathScreen;
    public Vector3 lastSavePoint;
    GameObject player;
    PlayerControl playerControl;
    bool loaded = false;
    bool markedfordestruction = false;
    void Awake(){
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        GameObject[] otherDeathManagers = GameObject.FindGameObjectsWithTag("deathmanager");
        deathScreen = GameObject.FindGameObjectWithTag("deathscreen").GetComponent<Animator>();
        foreach(GameObject otherManager in otherDeathManagers){
            if(otherManager.GetComponent<DeathManager>().loaded){
                Destroy(gameObject);
                markedfordestruction = true;
            }
        }
        if(!markedfordestruction){
            loaded = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void NextLevel(){
        DestroyImmediate(gameObject);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(loaded){
            player = GameObject.FindGameObjectWithTag("Player");
            playerControl = player.GetComponent<PlayerControl>();
            player.transform.position = lastSavePoint;
            playerControl.health = playerControl.maxHealth;
        }
    }
    public void Respawn(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Update(){
        if(playerControl.health <= 0){
            deathScreen.SetTrigger("dead");
        }
        
    }
}
