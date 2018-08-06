using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour 
{

	// Components.
	protected Rigidbody2D enemyRigidBody;
	protected Animator animator;

	// Patrol settings.
	public float enemyStartingPosition;
	public float enemyPatrolTurnPosition;
	public float patrolRange;

	// Movement.
	public float movementSpeed;
	public bool isFacingRight;
	public bool moveRight;
	
	// Attacking.
	public bool canAttackPlayer;
	public bool isAttacking;
	public float attackRate;
	private float nextAttack;

	// Set to true in inspector to create stationary enemies.
	// Stops all movement and directional behaviour.
	public bool idle;

	public bool cantPatrol;

	// Player ref.
	protected PlayerController player;

	[HideInInspector]
	public float playerXPosition;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		// Set facing depending on initial facing.
		if (gameObject.transform.localScale.x < 0)
		{
			isFacingRight = false;
		}
		else if (gameObject.transform.localScale.x > 0)
		{
			isFacingRight = true;
		}
		enemyRigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		enemyStartingPosition = transform.position.x;
		enemyPatrolTurnPosition = enemyStartingPosition + patrolRange;
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerXPosition = GameObject.FindGameObjectWithTag("Player").transform.position.x;
		if (canAttackPlayer)
		{
			isAttacking = true;
		}
		else 
		{
			isAttacking = false;
		}
		if (!idle && !canAttackPlayer)
		{
			DetermineDirection();
		}
		if (animator.gameObject.activeSelf)
		{
			Animate();
		}
	}
	
	void FixedUpdate ()
	{
		if (isAttacking)
		{
			if (Time.time > nextAttack)
			{
				Attack();
				nextAttack = Time.time + attackRate;
			}
		}
		else if (!isAttacking && !idle && !cantPatrol)
		{
			Patrol();
		}
		else if (idle)
		{
			enemyRigidBody.velocity = Vector2.zero;
		}
	}
	
	void DetermineDirection ()
	{
		if (enemyRigidBody.position.x <= enemyStartingPosition)
		{
			moveRight = true;
		}
		else if (enemyRigidBody.position.x >= enemyPatrolTurnPosition)
		{
			moveRight = false;
		}
	}

	void Patrol ()
	{
		if (moveRight)
		{
			enemyRigidBody.velocity = Vector2.right * movementSpeed;
			if (!isFacingRight)
			{
				InvertEnemyDirection();
			}
		}
		else if (!moveRight)
		{
			enemyRigidBody.velocity = Vector2.left * movementSpeed;
			if (isFacingRight)
			{
				InvertEnemyDirection();
			}
		}
	}

	void InvertEnemyDirection()
	{
		isFacingRight = !isFacingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	void Animate ()
	{
		if (!isAttacking && !idle)
		{
			animator.SetBool("Walk", true);
		} 
		else if (isAttacking || idle)
		{
			animator.SetBool("Walk", false);
		}

	}


	public abstract void Attack ();
}
