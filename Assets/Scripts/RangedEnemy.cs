
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float distanceToMaintain = 5;
    float counter = 0;
    void Update(){
        if(health <= 0){
            Destroy(gameObject);
        }
        animator.SetBool("isAttacking", false);
        if(seen){
            float distance = player.transform.position.x - transform.position.x;
            if(distance > 0){
                transform.localScale = new Vector3(-1,1,1);
            }
            if(distance < 0){
                transform.localScale = new Vector3(1,1,1);
            }
            if(Mathf.Abs(distance) - distanceToMaintain < 2){
                if(counter > 1){
                    animator.SetBool("isAttacking", true);
                    rb.velocityX = Mathf.Lerp(rb.velocityX, 0, Time.deltaTime);
                    Attack((player.transform.position - transform.position).normalized);
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
