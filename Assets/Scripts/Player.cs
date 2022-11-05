using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth;
	public AudioSource audioSource;
    public AudioClip sfx2;
    // public Collider object;

	public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		audioSource.clip = sfx2;
		audioSource.Play();
		healthBar.SetHealth(currentHealth);
		if (currentHealth <= 0){
			Dead();
		}
	}

	public void Dead(){
		SceneManager.LoadScene("YouDied");
	}
}
