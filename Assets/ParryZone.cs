using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryZone : MonoBehaviour
{
    public Animator animator;
    public string parryType;
    Collider2D collider2D;
    void Start(){
        collider2D = GetComponent<Collider2D>();
    }
    public void Parry(){
        animator.SetTrigger(parryType);
        collider2D.enabled = false;
    }
}
