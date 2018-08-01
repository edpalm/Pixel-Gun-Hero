using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
	private Animator animator;
	public bool isAlive = true;
	public float currentHealth;
	public float maxHealth = 1;
	// Use this for initialization
	void Start () 
	{
		currentHealth = maxHealth;
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isAlive && gameObject != null)
		{
			animator.SetTrigger("Dead");
				Destroy(gameObject, 0.5f);
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
