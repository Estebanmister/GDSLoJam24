using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    bool paused = false;
    public void returnToMenu(){
        SceneManager.LoadScene(0);
    }
    public void pause(InputAction.CallbackContext context){
        if(context.started){
            paused = !paused;
            if(paused){
            Time.timeScale = 0;
            ui.SetActive(true);
            } else {
                Time.timeScale = 1;
                ui.SetActive(false);
            }
        }
    }
}
