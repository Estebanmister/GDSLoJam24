using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSeeYou : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (transform.parent.position - player.position).normalized;
        transform.localPosition = -direction;
    }
}
