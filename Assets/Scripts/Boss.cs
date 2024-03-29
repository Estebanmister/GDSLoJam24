
using System.Diagnostics;
using UnityEngine;

public class Boss : Enemy
{
    public float distanceToMaintain = 5;
    float counter = 0;
    public void Attack(Vector3 atck_direction){
        GameObject newProjectile = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile cmp = newProjectile.GetComponent<Projectile>();
        cmp.velocity = atck_direction;
        cmp.transform.right = atck_direction;
    }
    void Update(){
        if(health <= 0){
            Destroy(gameObject);
        }
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
                if(counter > 1){
                    animator.SetBool("isAttacking", true);
                    rb.velocityX = Mathf.Lerp(rb.velocityX, 0, Time.deltaTime);
                    Attack((player.transform.position - transform.position).normalized);
                    counter = 0;
                }
                counter += Time.deltaTime;
            }
            //animator.SetBool("isWalking", Mathf.Abs(rb.velocityX) > 0.2f);
            if(Vector3.Distance(transform.position,player.transform.position) > distanceToMaintain){
                if(rb.velocity.magnitude < maxVelocity){
                    rb.AddForce((player.transform.position-transform.position).normalized * acceleration);
                }
            } else {
                if(rb.velocity.magnitude < maxVelocity){
                    rb.AddForce((-(player.transform.position-transform.position).normalized) * acceleration);
                }
            }
            
        }
    }
}
