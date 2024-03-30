using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCallback : MonoBehaviour
{
    PlayerControl playerControl;
    void Start()
    {
        playerControl = transform.parent.GetComponent<PlayerControl>();
    }

    public void attck(){
        playerControl.spawnAttack();
    }
    public void stop(){
        playerControl.animator.SetBool("isAttacking", false);
    }
}
