using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBullet : MonoBehaviour 
{
	
	public float damage;
	public float travelTime;

	public float bulletSpeed;
	public Vector2 velocity;
	
	// Use this for initialization
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
	void OnTriggerEnter2D(Collider2D other)
	{	
		if (gameObject != null && other.tag != "Scanner" && other.tag != "EnemyProjectile")
		{
			Destroy(gameObject);
		}
		if (other.tag == "Player")
		{
			Debug.Log("Player Hit!");
			other.gameObject.GetComponent<Health>().TakeDamage(damage);
			if (gameObject != null)
			{
				Destroy(gameObject);
			}
		}
	}
}
