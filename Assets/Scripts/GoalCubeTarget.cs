using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCubeTarget : MonoBehaviour
{
    public float health = 200f;

    public void TakeDamage(float amount){
        health -= amount;

        if(health <= 0f){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject);
        SceneManager.LoadScene("YouWon");
    }
}
