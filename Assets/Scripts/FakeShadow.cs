using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeShadow : MonoBehaviour
{
    Transform player;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    float map(float val, float oldmin, float oldmax, float newmin, float newmax){
        return (val - oldmin) * (newmax - newmin) / (oldmax - oldmin) + newmin;

    }

    void Update()
    {
        transform.position = new Vector3(player.position.x,transform.position.y,0);
        Color color = spriteRenderer.color;
        color.a = map(player.position.y,0,5,0.7f,0);
        if(color.a < 0){
            color.a = 0;
        }
        spriteRenderer.color = color;
    }
}
