using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : EnemyController
 {
	public GameObject scanner;
	// public bool isCharging;
	public float chargeSpeed;
	public float dashTime;

	public bool isCharging;
	// public bool playerInReach;

	private float timer = 0f;
	public override void Attack()
	{	
		StartCoroutine("Charge");
	}
	IEnumerator Charge()
	{
		canAttackPlayer = false;
		cantPatrol = true;
		scanner.SetActive(false);
		float time = 0;
		Vector3 chargeEndPosition = new Vector3(playerXPosition, gameObject.transform.position.y, gameObject.transform.position.z);
		while (Mathf.Abs(playerXPosition - gameObject.transform.position.x) > 1)
			{
				Debug.Log(Mathf.Abs(player.transform.position.x - gameObject.transform.position.x));
				gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, chargeEndPosition, chargeSpeed * Time.deltaTime);
				time += Time.deltaTime;				
				yield return new WaitForEndOfFrame();		
			}
		idle = true;
		animator.SetTrigger("Attack");
		yield return new WaitForSeconds(1);
		scanner.SetActive(true);
		idle = false;
		cantPatrol = false;
	}
}

