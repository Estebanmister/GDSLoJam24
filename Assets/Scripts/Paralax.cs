using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paralax : MonoBehaviour
{
    public Image background;
    public PlayerControl playerControl;
    public float speed;
    Vector3 initialpos;
    void Start()
    {
        initialpos = playerControl.transform.position;
    }
    void Update()
    {
        float delta = playerControl.transform.position.x - initialpos.x;
        background.rectTransform.position = new Vector3(delta * speed, background.rectTransform.position.y, background.rectTransform.position.z);
        
    }
}
