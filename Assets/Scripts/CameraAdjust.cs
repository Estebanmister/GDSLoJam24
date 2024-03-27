using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    public float playerHeight = 1.1f;
    void Update()
    {
        transform.localPosition = new Vector3(0,0,-10);
        float size = Camera.main.ViewportToWorldPoint(new Vector2(0,0)).y;
        
        if(size < 0){
            transform.localPosition += new Vector3(0, -size-playerHeight, 0);
        }
    }
}
