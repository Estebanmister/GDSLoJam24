using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paralax : MonoBehaviour
{
    public Image background;
    public PlayerControl playerControl;
    public float speed;
    void Start()
    {
        
    }
    void Update()
    {
        background.rectTransform.position = new Vector3(playerControl.transform.position.x * speed, background.rectTransform.position.y, background.rectTransform.position.z);
        float pos = Camera.main.ScreenToViewportPoint(background.rectTransform.position).x;
    }
}
