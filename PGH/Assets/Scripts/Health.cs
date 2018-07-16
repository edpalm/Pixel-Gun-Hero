using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
	private GameManager gameManager;

	private Animator animator;
	public bool isAlive = true;
	public int currentHealth;
	public int maxHealth = 1;
	// Use this for initialization
	void Start () 
	{
		currentHealth = maxHealth;
		animator = gameObject.GetComponent<Animator>();
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isAlive)
		{
			animator.SetBool("isDead", true);
			gameManager.EndGame();
		}
		
	}

	public void TakeDamage(int damage)
	{
		currentHealth = currentHealth - damage;
		animator.SetTrigger("TookDamage");
		if (currentHealth < 1)
		{
			isAlive = false;
		}
	}
}
