using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour 
{

	// Components.
	protected Rigidbody2D enemyRigidBody;
	protected Animator animator;

	// Patrol settings.
	protected float enemyStartingPosition;
	protected float enemyPatrolTurnPosition;
	public float patrolRange;

	// Movement.
	public float movementSpeed;
	protected bool isFacingRight;
	protected bool moveRight;
	
	// Attacking.
	[HideInInspector]
	public bool canAttackPlayer;
	protected bool isAttacking;
	public float attackRate;
	protected float nextAttack;

	// Set to true in inspector to create stationary enemies.
	// Stops all movement and directional behaviour.
	public bool idle;
	

	[HideInInspector]
	// Use this for initialization
	protected virtual void Start () 
	{
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
	protected virtual void Update () 
	{
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
	
	protected virtual void FixedUpdate ()
	{
		if (isAttacking)
		{
			if (Time.time > nextAttack)
			{
				Attack();
				nextAttack = Time.time + attackRate;
			}
		}
		else if (!isAttacking && !idle)
		{
			Patrol();
		}
		else if (idle)
		{
			enemyRigidBody.velocity = Vector2.zero;
		}
	}
	
	// Check which direction to move.
	protected void DetermineDirection ()
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

	protected void Patrol ()
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

	protected void InvertEnemyDirection ()
	{
		isFacingRight = !isFacingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	protected virtual void Animate ()
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


	protected abstract void Attack ();
}
