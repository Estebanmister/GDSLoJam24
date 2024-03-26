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
    public float jumping_variation = 5;
    public GameObject melee_projectile_prefab;
    public GameObject parry_projectile_prefab;
    float last_axis_change = 1;
    bool isGrounded = false;
    public LayerMask layer;
    public Animator animator;
    public Collider2D ParryUp;
    public Collider2D ParryCenter;
    public Collider2D ParryDown;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }
    void CheckGrounding(){
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector3.down, playerHeight, layer);
        
        isGrounded = hit2D.collider != null;
        
    }
    public void SpecialAttack(InputAction.CallbackContext context){

    }
    void Interact(Collider2D col){
        
    }
    public void heal(InputAction.CallbackContext context){
        
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
    public void jump(InputAction.CallbackContext context){
        if(context.started){
            Collider2D[] cols = Physics2D.OverlapPointAll(
                                Vector3.Scale(new Vector3(1,1,1), transform.position), 
                                LayerMask.GetMask("Interactable"));
            Collider2D toInteract = null;
            foreach(Collider2D col in cols){
                if(col != null){
                    toInteract = col;
                }
            }
            if(toInteract != null){
                Interact(toInteract);
                return;
            }
            if(isGrounded){
                //animator.SetBool("jump", true);
                jump_initiated = true;
                //rb.velocityY += jumping_initial_vel/2;
                //jumping_cooldown = 0;
            }
            if(isGrounded){
                //jumpCounter += Time.fixedDeltaTime;
            }
        }
    }
    float map(float val, float oldmin, float oldmax, float newmin, float newmax){
        return (val - oldmin) * (newmax - newmin) / (oldmax - oldmin) + newmin;

    }
    float parryCounter = 0;
    float jumpStrength = 0;
    bool jump_initiated = false;
    void FixedUpdate()
    {
        CheckGrounding();
        float x_axis = playerInput.actions["x_axis"].ReadValue<float>();
        float y_axis = playerInput.actions["y_axis"].ReadValue<float>();
        //animator.SetFloat("sword_pos", y_axis);
        bool jumpPressed = playerInput.actions["jump"].IsPressed();
        bool parry = playerInput.actions["parry"].IsPressed();

        if(jumpPressed && jump_initiated && jumpStrength <= 0.15f){
            jumpStrength += Time.fixedDeltaTime;
        } else if (jump_initiated){
            jump_initiated = false;
            animator.SetBool("jump", true);
            rb.velocityY += map(jumpStrength,0,0.15f,jumping_initial_vel-jumping_variation, jumping_initial_vel);
            jumpStrength = 0;
        }


        if(Mathf.Abs(rb.velocityX) < max_speed){
            // todo scale this with velocity
            rb.AddForce(Vector3.right * x_axis * acceleration);
        }
        if(Mathf.Abs(rb.velocityY) < 0.05f && isGrounded){
            animator.SetBool("jump", false);
        }
        if(x_axis < 0){
            animator.SetBool("isRunning", true);
            last_axis_change = -1;
            transform.localScale = new Vector3(-1,1,1);
        } else if (x_axis > 0){
            animator.SetBool("isRunning", true);
            last_axis_change = 1;
            transform.localScale = new Vector3(1,1,1);
        } else {
            animator.SetBool("isRunning", false);
        }
        if(parryCounter > parryPeriod){
            ParryCenter.enabled = false;
            ParryUp.enabled = false;
            ParryDown.enabled = false;
        }
        if(parry && parryCounter == 0){
            parryCounter += Time.fixedDeltaTime;
            if(y_axis < 0.5f && y_axis > -0.5f){
                ParryCenter.enabled = true;
            } else if (y_axis > 0.5f){
                ParryUp.enabled = true;
            } else if(y_axis < -0.5f){
                ParryDown.enabled = true;
            }
        }else if(parry && parryCounter < parryPeriod){
            parryCounter += Time.fixedDeltaTime;
        } else if (!parry){
            parryCounter = 0;
            ParryCenter.enabled = false;
            ParryUp.enabled = false;
            ParryDown.enabled = false;
        }
        //animator.SetFloat("animationSpeed", map(Mathf.Abs(rb.velocityX), 0.2f, max_speed, 0.2f, 2));
        
        if(attackCooldownCounter >= 0){
            attackCooldownCounter -= Time.fixedDeltaTime;
        }
        
    }
}
