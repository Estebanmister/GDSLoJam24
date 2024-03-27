using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10;
    public GameObject projectile;
    float count = 0;
    void Update(){
        count += Time.deltaTime;
        if(count>1){
            GameObject newProjectile = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
            Projectile cmp = newProjectile.GetComponent<Projectile>();
            cmp.velocity = new Vector3(1, 0,0);
            count = 0;
        }
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}