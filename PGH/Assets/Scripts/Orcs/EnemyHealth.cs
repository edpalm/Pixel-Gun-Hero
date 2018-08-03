using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
	public GameObject deathVFX;
	public bool isAlive = true;
	public float currentHealth;
	public float maxHealth;
	// Use this for initialization
	void Start () 
	{
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isAlive && gameObject != null)
		{
				Instantiate (deathVFX, gameObject.transform.position, gameObject.transform.rotation);
				Destroy(gameObject, 0f);
				
		}
		
	}

	public void TakeDamage(float damage)
	{
		currentHealth = currentHealth - damage;
		if (currentHealth < 1)
		{
			isAlive = false;
		}
	}
}
