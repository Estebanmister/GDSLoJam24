using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryZone : MonoBehaviour
{
    public Animator animator;
    public string parryType;
    Collider2D collider2D;
    public ParticleSystem parryParticles;
    ScreenShake screenShake;
    void Start(){
        collider2D = GetComponent<Collider2D>();
        screenShake = transform.parent.parent.GetComponent<ScreenShake>();
    }
    public void Parry(){
        animator.SetTrigger(parryType);
        collider2D.enabled = false;
        parryParticles.Play();
        screenShake.shakeFor = 0.2f;
    }
}
