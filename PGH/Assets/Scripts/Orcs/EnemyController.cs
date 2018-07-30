using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour 
{

	// Components.
	Rigidbody2D enemyRigidBody;
	Animator animator;

	// Patrol settings.
	public float enemyStartingPosition;
	public float enemyPatrolTurnPosition;
	public int patrolRange;

	// Movement.
	public float movementSpeed;
	public bool isFacingRight;
	public bool moveRight;
	
	// Attacking.
	public bool canAttackPlayer;
	public bool isAttacking;
	
	// Use this for initialization
	void Start () 
	{
		enemyRigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		enemyStartingPosition = transform.position.x;
		enemyPatrolTurnPosition = enemyStartingPosition + patrolRange;
		isFacingRight = true; // rework?
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canAttackPlayer)
		{
			Attack();
		}
		DetermineDirection();
		// Look for player.
		// If player found. set variable to true.
		// If True Attack player.
		// If False, back to patrol.
	}
	
	void FixedUpdate ()
	{
		if (!canAttackPlayer)
		{
			Patrol();
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
			// enemyRigidBody.AddForce(Vector2.right * movementSpeed * Time.deltaTime);
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
		Debug.Log("Invert");
		isFacingRight = !isFacingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}


	public abstract void Attack ();
}
