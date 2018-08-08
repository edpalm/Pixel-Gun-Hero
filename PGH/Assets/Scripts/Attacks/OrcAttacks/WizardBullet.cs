using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBullet : MonoBehaviour 
{
	
	public float damage;
	public float travelTime;

	public float bulletSpeed;

	[HideInInspector]
	public Vector2 velocity;
	
	// Use this for initialization
	// Set direction of sprite depending on which way it's going.
	void Start () 
	{
		gameObject.GetComponent<Rigidbody2D>().velocity = velocity * bulletSpeed;
		if (GetComponent<Rigidbody2D>().velocity.x > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}
		else if (GetComponent<Rigidbody2D>().velocity.x < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
		if (gameObject != null)
		{
			Destroy(gameObject, travelTime);
		}
	}

	// Deal damage to player if hit.
	// Destroy bullet on collision.
	void OnTriggerEnter2D (Collider2D other)
	{	
		if (gameObject != null && other.tag != "Scanner" && other.tag != "EnemyProjectile")
		{
			Destroy(gameObject);
		}
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
