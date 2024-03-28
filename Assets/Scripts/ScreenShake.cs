using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeFor = 0;
    void Shake(){
        if(shakeFor > 0){
            shakeFor -= Time.deltaTime;
            Camera.main.transform.localPosition += new Vector3(Random.Range(-0.1f,0.1f),Random.Range(-0.1f,0.1f));
        }
    }
    void LateUpdate(){
        Shake();
    }
}
