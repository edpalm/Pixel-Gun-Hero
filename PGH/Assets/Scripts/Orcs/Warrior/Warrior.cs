using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : EnemyController
 {
	public GameObject scanner;
	public GameObject chargeScanner;
	// public bool isCharging;
	public float chargeSpeed;
	
	public bool isCharging;
	
	public bool playerInRange;

	private int attacksPerformedAfterCharge = 0;
	public int maxAttacks;

	public bool resetCharge;
	public float chargeResetTime;
	public float timer = 0f;

 	public GameObject attack;
	public Transform attackSpawner;

	protected override void Update()
	{
		if (resetCharge)
		{
			timer += Time.deltaTime;
			if(timer > chargeResetTime)
			{
				Debug.Log("Resetting postcharge");
				playerInRange = false;
				isCharging = false;
				attacksPerformedAfterCharge = 0;
				canAttackPlayer = false;
				scanner.SetActive(true);
				chargeScanner.SetActive(false);
				resetCharge = false;
				timer = 0f;
			}
		}
		else if (canAttackPlayer && !isCharging)
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

	protected override void FixedUpdate()
	{
		if (isAttacking && !isCharging)
		{
			isCharging = true;
			StartCoroutine("Charge");
		}
		else if (isCharging && playerInRange && Time.time > nextAttack && attacksPerformedAfterCharge < maxAttacks)
		{
			Attack();
			nextAttack = Time.time + attackRate;
		}
		else if (isCharging && attacksPerformedAfterCharge >= maxAttacks)
		{
			resetCharge = true;
		}
		else if (!isAttacking && !idle && !isCharging)
		{
			Patrol();
		}
		else if (idle)
		{
			enemyRigidBody.velocity = Vector2.zero;
		}
	}
	
	protected override void Attack ()
	{	
		enemyRigidBody.velocity = Vector2.zero;
		animator.SetTrigger("Attack");
    Instantiate (attack, attackSpawner.position, attackSpawner.rotation);
		attacksPerformedAfterCharge ++;
	}

	IEnumerator Charge()
	{
		Debug.Log("Charging");
		scanner.SetActive(false);
		chargeScanner.SetActive(true);
		Vector3 playerPosition  = GameObject.FindGameObjectWithTag("Player").transform.position;
		Vector3 chargeEndPosition = new Vector3(playerPosition.x, gameObject.transform.position.y, gameObject.transform.position.z);
		while (Mathf.Abs(playerPosition.x - gameObject.transform.position.x) > 1 && !playerInRange)
		{
		  gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, chargeEndPosition, chargeSpeed * Time.deltaTime);			
			yield return new WaitForEndOfFrame();
					
		}
	} 
	protected override void Animate()
	{
		if (!idle && !resetCharge)
		{
			animator.SetBool("Walk", true);
		}
		else if (idle || resetCharge)
		{
			animator.SetBool("Walk", false);
		}
	}
}

