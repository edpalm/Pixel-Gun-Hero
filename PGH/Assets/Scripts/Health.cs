using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
	private GameManager gameManager;
	private Rigidbody2D playerBody;

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
		playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isAlive)
		{
			animator.SetBool("isDead", true);
			playerBody.velocity = new Vector2(0, 0);
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
