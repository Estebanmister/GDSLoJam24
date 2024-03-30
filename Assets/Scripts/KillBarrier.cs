using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBarrier : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.otherCollider.tag == "Player"){
            collision.otherCollider.GetComponent<PlayerControl>().health = 0;
        } else {
            Destroy(collision.otherCollider.gameObject);
        }
    }
}
