using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseOptions : MonoBehaviour
{
    public void QuitToMenu(){
        Time.timeScale = 1;
        DestroyImmediate(GameObject.FindGameObjectWithTag("deathmanager"));
        SceneManager.LoadScene(0);
    }
}
