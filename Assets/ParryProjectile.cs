using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParryProjectile : MonoBehaviour
{
    public Vector3 velocity;
    public float speed = 20;
    public float lifetime_distance = 0.5f;
    float elapsed_distance = 0;
    void OnCollisionEnter2D(Collision2D collision) 
    {
        Collider2D other = collision.otherCollider;
        if(other.tag != "Player"){
            if(other.tag == "EnemyProjectile"){
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }
    void Update(){
        if(elapsed_distance> lifetime_distance){
            Destroy(gameObject);
        } else {
            transform.position += velocity * speed * Time.deltaTime;
            elapsed_distance += (velocity * speed * Time.deltaTime).magnitude;
        }
    }
}