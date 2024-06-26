using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerControl : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody2D rb;
    public bool flipSide = false;
    public float flipSideStrengthModifier = 2;
    public float health = 10;
    public float maxHealth = 10;
    public float maxStamina = 10;
    public float stamina = 10;
    public float staminaRecover = 0.2f;
    public float healthRecover = 0.1f;
    // Parrying has to happen within 0.2 seconds
    public float parryPeriod = 0.2f;
    //public float parryCooldown = 0.1f;
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
    public Transform parryPivot;

    public AudioSource ParrySound;
    public AudioSource[] AttackSounds;
    public AudioSource DashSound;
    public AudioSource DamageSound;
    public AudioSource jumpingSound;
    public AudioSource interactSound;
    GameObject pauseMenu;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        pauseMenu = GameObject.FindGameObjectWithTag("pause").transform.GetChild(0).gameObject;
        
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
                DashSound.Play();
            }
        }
    }
    void Interact(Collider2D col){
        NPC dialogueTrigger = col.GetComponent<NPC>();
        SavePoint savePoint = col.GetComponent<SavePoint>();
        interactSound.Play();
        if(savePoint != null){
            savePoint.activate();
        } else {
            dialogueTrigger.interact();
        }
    }
    public void heal(InputAction.CallbackContext context){
        
    }
    float attackCooldownCounter = 0;
    public void spawnAttack(float strength = 1){
        GameObject newProjectile = GameObject.Instantiate(melee_projectile_prefab, transform.position, Quaternion.identity);
        Projectile cmp = newProjectile.GetComponent<Projectile>();
        if(flipSide && health < maxHealth/2){
            cmp.damage *= strength * flipSideStrengthModifier;
        } else {
            cmp.damage *= strength;
        }
        AttackSounds[lastPhase].Play();
        if(lastPhase >= 2){
            cmp.damage *= 2;
            lastPhase = -1;
        }
       
        lastPhase += 1;
        cmp.velocity = new Vector3(Mathf.Round(last_axis_change), Mathf.Round(Mathf.Round(playerInput.actions["y_axis"].ReadValue<float>())),0);
    }
    int lastPhase = 0;
    public void Attack_Basic(InputAction.CallbackContext context){
        if(context.started){
            if(attackCooldownCounter <= 0){
                animator.SetBool("isAttacking", true);
                attackCooldownCounter = basic_attack_cooldown;
                lastPhase = 0;
                //spawnAttack();
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
                jumpingSound.Play();
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
    bool paused = false;
    public void pauseGame(InputAction.CallbackContext context){
        if(context.started){
            if(!paused){
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                paused = true;
                EventSystem.current.SetSelectedGameObject(pauseMenu.GetComponentInChildren<Button>().gameObject);
            } else {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                paused = false;
            }
        }
    }
    float parryCounter = 0;
    float jumpStrength = 0;
    bool jump_initiated = false;
    float dashCounter = 0;
    void FixedUpdate()
    {
        if(health <= 0){
            // dead
            animator.SetBool("dead", true);
            return;
        }
        CheckGrounding();
        float x_axis = playerInput.actions["x_axis"].ReadValue<float>();
        float y_axis = playerInput.actions["y_axis"].ReadValue<float>();
        //animator.SetFloat("sword_pos", y_axis);
        bool jumpPressed = playerInput.actions["jump"].IsPressed();
        bool parry = playerInput.actions["parry"].IsPressed();
        animator.SetBool("isDashing", isDashing);
        

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
            stamina += Time.fixedDeltaTime * staminaRecover;
            if(flipSide){
                stamina += Time.fixedDeltaTime * 2;
            }
        }
        
        if(health < maxHealth){
            health += Time.fixedDeltaTime * healthRecover;
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
            // switch animations to left
            animator.SetBool("directionRight", false);
            parryPivot.transform.localScale = new Vector3(-1,1,1);
            //transform.localScale = new Vector3(-1,1,1);
        } else if (x_axis > 0){
            animator.SetBool("isRunning", true);
            last_axis_change = 1;
            // switch animations to right
            animator.SetBool("directionRight", true);
            parryPivot.transform.localScale = new Vector3(1,1,1);
            //transform.localScale = new Vector3(1,1,1);
        } else {
            animator.SetBool("isRunning", false);
        }
        if(parryCounter > parryPeriod){
            ParryCenter.enabled = false;
            ParryUp.enabled = false;
            animator.SetBool("parry", false);
        }
        if(parry && parryCounter == 0 && !animator.GetBool("isAttacking")){
            parryCounter += Time.fixedDeltaTime;
            ParryCenter.enabled = true;
            animator.SetBool("parry", true);
        }else if(parry && parryCounter < parryPeriod){
            parryCounter += Time.fixedDeltaTime;
        } else if (!parry){
            parryCounter = 0;
            ParryCenter.enabled = false;
            ParryUp.enabled = false;
            animator.SetBool("parry", false);
        }
        //animator.SetFloat("animationSpeed", map(Mathf.Abs(rb.velocityX), 0.2f, max_speed, 0.2f, 2));
        
        if(attackCooldownCounter >= 0){
            attackCooldownCounter -= Time.fixedDeltaTime;
        }
        
    }
}
