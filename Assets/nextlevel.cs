using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextlevel : MonoBehaviour
{
    public int goodLevel = 0;
    public int badLevel = 0;
    public bool lastCheck = false;
    public void nextGoodLevel(){
        // sign will call this at chapter 2 night, to see which ending we get
        if(lastCheck && EndingCheck.deadLevels == 1){
            // baaaad ending
            DestroyImmediate(GameObject.FindGameObjectWithTag("deathmanager"));
            SceneManager.LoadScene(badLevel);
        } else {
            DestroyImmediate(GameObject.FindGameObjectWithTag("deathmanager"));
            SceneManager.LoadScene(goodLevel);
        }
        
    }
    public void nextBadLevel(){
        EndingCheck.deadLevels += 1;
        DestroyImmediate(GameObject.FindGameObjectWithTag("deathmanager"));
        SceneManager.LoadScene(badLevel);
        
    }
}
