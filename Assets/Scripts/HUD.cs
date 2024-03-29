using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    PlayerControl playerControl;
    TMP_Text text;
    void Start(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        text.text = playerControl.health.ToString();
    }
}
