using UnityEngine;

public class EnemyTriggerZone : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other){
        transform.parent.GetComponent<Enemy>().PlayerDetected(other.gameObject);
    }
    void OnTriggerExit2D(Collider2D other){
        transform.parent.GetComponent<Enemy>().PlayerLeft();
    }
}