
using System.Diagnostics;
using UnityEngine;

public class Boss : Enemy
{
    public float distanceToMaintain = 5;
    public bool ignoreDistance = true;
    float counter = 0;
    Vector3 atck_direction;
    public void Attack(){
        atck_direction = (player.transform.position - transform.position).normalized;
        GameObject newProjectile = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile cmp = newProjectile.GetComponent<Projectile>();
        cmp.velocity = atck_direction;
        cmp.transform.right = atck_direction;

        atck_direction = Vector3.Cross(atck_direction, Vector3.forward);
        newProjectile = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
        cmp = newProjectile.GetComponent<Projectile>();
        cmp.velocity = atck_direction;
        cmp.transform.right = atck_direction;

        atck_direction = Vector3.Cross((player.transform.position - transform.position).normalized, Vector3.back);
        newProjectile = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
        cmp = newProjectile.GetComponent<Projectile>();
        cmp.velocity = atck_direction;
        cmp.transform.right = atck_direction;

        atck_direction = -(player.transform.position - transform.position).normalized;
        newProjectile = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
        cmp = newProjectile.GetComponent<Projectile>();
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
            if(Mathf.Abs(distance) - distanceToMaintain < 2 || ignoreDistance){
                if(counter > 1){
                    animator.SetBool("isAttacking", true);
                    rb.velocityX = Mathf.Lerp(rb.velocityX, 0, Time.deltaTime);
                    //Attack((player.transform.position - transform.position).normalized);
                    atck_direction = (player.transform.position - transform.position).normalized;
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
