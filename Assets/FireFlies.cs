using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlies : MonoBehaviour
{
    public float updateRate = 2;
    Vector3 newpos = Vector3.zero;
    public float radius = 5;
    public float speed = 0.001f;
    void Start()
    {
        
    }
    float count = 0;
    void Update()
    {
        if(count > updateRate){
            newpos = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,1.0f),0)*radius;
            count = 0;
        }
        count += Time.deltaTime;
        transform.localPosition = Vector3.Lerp(transform.localPosition, newpos, speed);
    }
}
