using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10;
    public GameObject projectile;
    public GameObject player;
    public bool seen = false;
    public Rigidbody2D rb;
    public float acceleration = 10;
    public float maxVelocity = 10;
    public Animator animator;
    public bool homing = false;
    public Vector3 defScale;
    
    public void Start(){
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        defScale = transform.localScale;
    }
    public void PlayerDetected(GameObject obj){
        seen = true;
        player = obj;
    }
    public void PlayerLeft(){
        if(!homing){
            seen = false;
            player = null;
        }
    }
    
    void Update(){
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}