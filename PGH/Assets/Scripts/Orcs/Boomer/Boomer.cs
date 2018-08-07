using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer : EnemyController 
{
	public GameObject boomerang;
	public Transform boomerangSpawner;

	// Use this for initialization
	protected override void Attack()
	{
	//	animator.SetTrigger("Attack");
		enemyRigidBody.velocity = Vector2.zero;
		animator.SetTrigger("Attack");
		Debug.Log("boomer attacking!");	
		if (isFacingRight)
		{
			GameObject boomerangInstance = (GameObject)Instantiate (boomerang, boomerangSpawner.position, boomerangSpawner.rotation);	
			boomerangInstance.GetComponent<Boomerang>().velocity = Vector2.right;
			
		}
		else
		{
			GameObject boomerangInstance = (GameObject)Instantiate (boomerang, boomerangSpawner.position, boomerangSpawner.rotation);	
			boomerangInstance.GetComponent<Boomerang>().velocity = Vector2.left;
		}
	}
}
