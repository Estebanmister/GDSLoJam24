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
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag != "Player"){
            if(other.tag == "Enemy"){
                other.GetComponent<Enemy>().health -= damage;
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