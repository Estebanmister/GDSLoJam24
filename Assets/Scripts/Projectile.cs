using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    public float damage = 1;
    public Vector3 velocity;
    public float speed = 20;
    public float lifetime_distance = 0.5f;
    float elapsed_distance = 0;
    public bool friendly = true;
    public bool melee = false;
    public ParticleSystem collisionParticles;
    SpriteRenderer spriteRenderer;
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(collisionParticles.isPlaying){
            return;
        }
        if(!friendly){
            if(other.tag != "Enemy"){
                if(spriteRenderer != null){
                    spriteRenderer.enabled = false;
                }
                
                if(other.tag == "ParryZone"){
                    other.GetComponent<ParryZone>().Parry();
                    //
                    if(!melee){
                        friendly = true;
                        speed = -speed;
                        spriteRenderer.enabled = true;
                        transform.right = -velocity;
                    } else {
                        Destroy(gameObject);
                        friendly = true;
                        speed = 0;
                    }
                    
                }
                if(other.tag == "Player"){
                    collisionParticles.Play();
                    PlayerControl playerControl = other.GetComponent<PlayerControl>();
                    playerControl.health -= damage;
                    playerControl.DamageSound.Play();
                    playerControl.animator.SetTrigger("hurt");
                }
            }
        } else {
            if(other.tag != "Player"){
                if(other.tag == "Enemy"){
                    collisionParticles.Play();
                    other.GetComponent<Enemy>().health -= damage;
                }
            }
        }
        
    }
    bool destroyit = false;
    void Update(){
        if(collisionParticles.isPlaying){
            destroyit = true;
            return;
        } else if (destroyit){
            Destroy(gameObject);
            return;
        }
        if(elapsed_distance> lifetime_distance){
            Destroy(gameObject);
        } else {
            transform.position += velocity * speed * Time.deltaTime;
            elapsed_distance += (velocity * speed * Time.deltaTime).magnitude;
        }
    }
}