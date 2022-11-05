using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 70f;
    public HealthBar healthbar;

    public void TakeDamage(float amount){
        health -= amount;
        healthbar.SetHealth(health);
        if(health <= 0f){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject);
    }
}
