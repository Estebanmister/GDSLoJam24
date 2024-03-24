using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControl : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody2D rb;
    public float health = 10;
    public float maxStamina = 10;
    public float stamina = 10;
    // Parrying has to happen within 0.2 seconds
    public float parryPeriod = 0.2f;
    
    public float basic_attack_cooldown = 0.2f;
    public float heavy_attack_cooldown = 0.3f;
    public float playerHeight = 1.0f;
    public float acceleration = 20;
    public float max_speed = 10;
    public float jumping_initial_vel = 1;
    Animator animator;
    public GameObject melee_projectile_prefab;
    float last_axis_change = 1;
    bool isGrounded = false;
    public LayerMask layer;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void CheckGrounding(){
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector3.down, playerHeight, layer);
        
        isGrounded = hit2D.collider != null;
    }
    float attackCooldownCounter = 0;
    public void Attack_Basic(InputAction.CallbackContext context){
        if(context.started){
            if(attackCooldownCounter <= 0){
                attackCooldownCounter = basic_attack_cooldown;
                GameObject newProjectile = GameObject.Instantiate(melee_projectile_prefab, transform.position, Quaternion.identity);
                Projectile cmp = newProjectile.GetComponent<Projectile>();
                cmp.velocity = new Vector3(Mathf.Round(last_axis_change), Mathf.Round(Mathf.Round(playerInput.actions["y_axis"].ReadValue<float>())),0);
            }
        }
    }

    void FixedUpdate()
    {
        CheckGrounding();
        float x_axis = playerInput.actions["x_axis"].ReadValue<float>();
        float y_axis = playerInput.actions["y_axis"].ReadValue<float>();
        animator.SetFloat("sword_pos", y_axis);
        bool jump = playerInput.actions["jump"].IsPressed();
        bool basic_attack = playerInput.actions["basic_attack"].IsPressed();
        if(rb.velocityX < max_speed){
            // todo scale this with velocity
            rb.AddForce(Vector3.right * x_axis * acceleration);
        }
        if(x_axis < 0){
            last_axis_change = -1;
            transform.localScale = new Vector3(-1,1,1);
        } else if (x_axis > 0){
            last_axis_change = 1;
            transform.localScale = new Vector3(1,1,1);
        }
        if(jump && isGrounded){
            // todo, only jump when touching the ground, add cooldown
            rb.velocityY += jumping_initial_vel;
        }
        if(attackCooldownCounter >= 0){
            attackCooldownCounter -= Time.fixedDeltaTime;
        }
        
    }
}
