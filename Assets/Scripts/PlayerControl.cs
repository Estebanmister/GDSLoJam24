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
    public float dashSpeed = 20;
    // how long to lock controls during a dash
    public float dashPeriod = 0.3f;
    public float basic_attack_cooldown = 0.2f;
    public float heavy_attack_cooldown = 0.3f;
    public float playerHeight = 1.0f;
    public float acceleration = 20;
    public float max_speed = 10;
    public float jumping_initial_vel = 1;
    public float jumping_variation = 5;
    public GameObject melee_projectile_prefab;
    public float meeleCriticalModifier = 2;
    public GameObject parry_projectile_prefab;
    float last_axis_change = 1;
    bool isGrounded = false;
    bool isDashing = false;
    public LayerMask layer;
    public Animator animator;
    public Collider2D ParryUp;
    public Collider2D ParryCenter;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }
    void CheckGrounding(){
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector3.down, playerHeight, layer);
        
        isGrounded = hit2D.collider != null;
        
    }
    public void dash(InputAction.CallbackContext context){
        if(context.started){
            if(stamina > 0){
                stamina -= 1;
                rb.velocityX += dashSpeed*last_axis_change;
                isDashing = true;
            }
        }
    }
    void Interact(Collider2D col){

    }
    public void heal(InputAction.CallbackContext context){
        
    }
    float attackCooldownCounter = 0;
    void spawnAttack(float strength = 1){
        GameObject newProjectile = GameObject.Instantiate(melee_projectile_prefab, transform.position, Quaternion.identity);
        Projectile cmp = newProjectile.GetComponent<Projectile>();
        cmp.damage *= strength;
        cmp.velocity = new Vector3(Mathf.Round(last_axis_change), Mathf.Round(Mathf.Round(playerInput.actions["y_axis"].ReadValue<float>())),0);
    }
    int lastPhase = 0;
    public void Attack_Basic(InputAction.CallbackContext context){
        if(context.started){
            if(attackCooldownCounter <= 0){
                animator.SetBool("isAttacking", true);
                attackCooldownCounter = basic_attack_cooldown;
                lastPhase = 0;
                spawnAttack();
            }
        }
        if(context.canceled){
            animator.SetBool("isAttacking", false);
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
    float dashCounter = 0;
    void FixedUpdate()
    {
        CheckGrounding();
        float x_axis = playerInput.actions["x_axis"].ReadValue<float>();
        float y_axis = playerInput.actions["y_axis"].ReadValue<float>();
        //animator.SetFloat("sword_pos", y_axis);
        bool jumpPressed = playerInput.actions["jump"].IsPressed();
        bool parry = playerInput.actions["parry"].IsPressed();
        animator.SetBool("isDashing", isDashing);
        // honestly i could use playerInput.actions["attack"].ReadValue<float>() instead of animator.GetBool("isAttacking")
        // but like, im already keeping track of it so idk
        if(animator.GetBool("isAttacking") && animator.GetInteger("attackPhase") != lastPhase){
            // if the attack button is being held
            // and we moved onto another phase of the attack
            // then spawn a new attack projectile
            lastPhase = animator.GetInteger("attackPhase");
            if(lastPhase == 2){
                // last phase of the attack
                spawnAttack(strength: meeleCriticalModifier);
            } else {
                spawnAttack();
            }
            
        }

        if(isDashing){
            dashCounter += Time.deltaTime;
            if(dashCounter > dashPeriod){
                isDashing = false;
                dashCounter = 0;
            } else {
                return;
            }
        }
        if(stamina < maxStamina){
            stamina += Time.fixedDeltaTime;
        }
        if(jumpPressed && jump_initiated && jumpStrength <= 0.15f){
            jumpStrength += Time.fixedDeltaTime;
        } else if (jump_initiated){
            jump_initiated = false;
            animator.SetBool("jump", true);
            rb.velocityY += map(jumpStrength,0,0.15f,jumping_initial_vel-jumping_variation, jumping_initial_vel);
            jumpStrength = 0;
        }
        // todo use y_axis to change character's head position to reflect aim
        // waiting for wendi to do her work :^)

        if(Mathf.Abs(rb.velocityX) < max_speed){
            rb.AddForce(Vector3.right * x_axis * acceleration);
        }
        if(Mathf.Abs(rb.velocityY) < 0.05f && isGrounded){
            animator.SetBool("jump", false);
        }
        if(x_axis < 0){
            animator.SetBool("isRunning", true);
            last_axis_change = -1;
            // todo switch animations to left
            transform.localScale = new Vector3(-1,1,1);
        } else if (x_axis > 0){
            animator.SetBool("isRunning", true);
            last_axis_change = 1;
            // todo switch animations to right
            transform.localScale = new Vector3(1,1,1);
        } else {
            animator.SetBool("isRunning", false);
        }
        if(parryCounter > parryPeriod){
            ParryCenter.enabled = false;
            ParryUp.enabled = false;
        }
        if(parry && parryCounter == 0 && !animator.GetBool("isAttacking")){
            parryCounter += Time.fixedDeltaTime;
            if(y_axis < 0.5f && y_axis > -0.5f){
                ParryCenter.enabled = true;
            } else if (y_axis > 0.5f){
                ParryUp.enabled = true;
            }
        }else if(parry && parryCounter < parryPeriod){
            parryCounter += Time.fixedDeltaTime;
        } else if (!parry){
            parryCounter = 0;
            ParryCenter.enabled = false;
            ParryUp.enabled = false;
        }
        //animator.SetFloat("animationSpeed", map(Mathf.Abs(rb.velocityX), 0.2f, max_speed, 0.2f, 2));
        
        if(attackCooldownCounter >= 0){
            attackCooldownCounter -= Time.fixedDeltaTime;
        }
        
    }
}
