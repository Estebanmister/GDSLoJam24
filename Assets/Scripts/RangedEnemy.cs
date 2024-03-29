
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float distanceToMaintain = 5;
    public float shootSpeed = 1;
    float counter = 0;
    Vector3 atck_direction;
    void Awake(){
        counter = shootSpeed;
    }
    public void Attack(){
        GameObject newProjectile = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile cmp = newProjectile.GetComponent<Projectile>();
        cmp.velocity = atck_direction;
        cmp.transform.right = atck_direction;
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
            if(distance > 0){
                transform.localScale = new Vector3(-defScale.x,defScale.y,defScale.z);
            }
            if(distance < 0){
                transform.localScale = new Vector3(defScale.x,defScale.y,defScale.z);
            }
            if(Mathf.Abs(distance) - distanceToMaintain < 2){
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
