using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : EnemyController
 {
	private bool chargeDone;
	public GameObject scanner;
	public GameObject chargeScanner;
	
	public float chargeSpeed;
	
	private bool isCharging;
	
	[HideInInspector]
	public bool playerInRange;

	private int attacksPerformedAfterCharge = 0;
	
	// Number of attacks before resetting
	public int maxAttacks;

	private bool resetCharge;
	public float chargeResetTime;
	private float timer = 0f;

 	public GameObject attack;
	public Transform attackSpawner;

	protected override void Update ()
	{
		if (resetCharge)
		{
			timer += Time.deltaTime;
			if(timer > chargeResetTime)
			{
				playerInRange = false;
				isCharging = false;
				attacksPerformedAfterCharge = 0;
				canAttackPlayer = false;
				scanner.SetActive(true);
				chargeScanner.SetActive(false);
				resetCharge = false;
				timer = 0f;
				chargeDone = false;
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

	protected override void FixedUpdate ()
	{
		if (isAttacking && !isCharging)
		{
			isCharging = true;
			StartCoroutine("Charge");
		}
		else if (chargeDone && attacksPerformedAfterCharge >= maxAttacks)
		{
			resetCharge = true;
		}
		else if (isCharging && chargeDone )
		{
			Attack();
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

	IEnumerator Charge ()
	{
		scanner.SetActive(false); // disable player detecion.
		chargeScanner.SetActive(true); // enable player in range detection.
		Vector2 playerPosition  = GameObject.FindGameObjectWithTag("Player").transform.position;
		Vector2 chargeEndPosition = new Vector3(playerPosition.x, gameObject.transform.position.y);
		while (Mathf.Abs(playerPosition.x - gameObject.transform.position.x) > 0.5 && !playerInRange)
		{
			// If not close to player, keep charging.
		  gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, chargeEndPosition, chargeSpeed * Time.deltaTime);			
			yield return new WaitForEndOfFrame();
		}
		chargeDone = true;
	} 

	protected override void Animate ()
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

