using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttack : MonoBehaviour 
{
	public float lifeTime;
	public float damage;
	// Use this for initialization
void Start ()	
{
	if (gameObject != null)
	{
		Destroy(gameObject, lifeTime);
	}
}
	
// Deal damage to player if hit.
// Destroy attack.	
void OnTriggerEnter2D (Collider2D other)
	{	
		if (other.tag == "Player")
		{
			other.gameObject.GetComponent<Health>().TakeDamage(damage);
			if (gameObject != null)
			{
				Destroy(gameObject);
			}
		}
	}
}
