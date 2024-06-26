
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float distanceToMaintain = 5;
    public float shootSpeed = 1;
    float counter = 0;
    public bool flee = false;
    Vector3 atck_direction;
    AudioSource attck;
    SpriteRenderer spriteRenderer;
    void Awake(){
        counter = shootSpeed;
        attck = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Attack(){
        GameObject newProjectile = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile cmp = newProjectile.GetComponent<Projectile>();
        cmp.velocity = atck_direction;
        cmp.transform.right = atck_direction;
        attck.Play();
    }
    void Update(){
        if(health <= 0){
            //Destroy(gameObject);
            animator.SetBool("dead", true);
            return;
        }
        //if(barrageCounter > 0){
        //    barrageTimer += Time.deltaTime;
        //}
        //if(barrageCounter > 0 && barrageTimer > barrageTime){
        //    Attack((player.transform.position - transform.position).normalized);
        ///    barrageCounter -= 1;
        //    barrageTimer = 0;
        //}
        animator.SetBool("isAttacking", false);
        if(seen){
            float distance = player.transform.position.x - transform.position.x;
            //Debug.Log(distance);
            if(distance > 0){
                //spriteRenderer.flipX = tr
                animator.SetBool("directionRight", true);
                transform.localScale = new Vector3(-defScale.x,defScale.y,defScale.z);
            } else if(distance < 0){
                transform.localScale = new Vector3(defScale.x,defScale.y,defScale.z);
                animator.SetBool("directionRight", false);
            }
            if(Mathf.Abs(distance) - distanceToMaintain < 2 && !flee){
                if(counter > shootSpeed){
                    animator.SetBool("isAttacking", true);
                    rb.velocityX = Mathf.Lerp(rb.velocityX, 0, Time.deltaTime);
                    //Attack((player.transform.position - transform.position).normalized);
                    atck_direction = (player.transform.position - transform.position).normalized;
                    //barrageCounter = barrage;
                    //barrageTimer = 0;
                    counter = 0;
                }
                counter += Time.deltaTime;
            }
            animator.SetBool("isWalking", Mathf.Abs(rb.velocityX) > 0.2f);
            if(Mathf.Abs(rb.velocityX) < maxVelocity){
                if(flee){
                    if(distance <= 0){
                        rb.AddForce(Vector3.right * acceleration * Time.deltaTime);
                    } else if(distance > 0){
                        rb.AddForce(Vector3.left * acceleration * Time.deltaTime);
                    }
                    return;
                }
                if(Mathf.Abs(distance) < distanceToMaintain){
                    if(distance <= 0){
                        rb.AddForce(Vector3.right * acceleration * Time.deltaTime);
                    } else if(distance > 0){
                        rb.AddForce(Vector3.left * acceleration * Time.deltaTime);
                    }
                }
                if(Mathf.Abs(distance) > distanceToMaintain){
                    if(distance <= 0){
                        rb.AddForce(Vector3.left * acceleration * Time.deltaTime);
                    } else if(distance > 0){
                        rb.AddForce(Vector3.right * acceleration * Time.deltaTime);
                    }
                }
            }
            
        }
    }
}
