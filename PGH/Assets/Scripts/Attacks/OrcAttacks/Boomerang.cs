using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour 
{
	public float damage;
	public float range;
	public float boomerangSpeed;
	private bool returning;

	private float thrownDistance;

	private float startingPosition;

	private float currentPosition;
	
	[HideInInspector]
	public Vector2 velocity;

	public float travelTime;

	private bool changeDirection;

	// Use this for initialization
	// Set starting position to determine where boomerang should despawn upon returning back.
	// Check direction of thrown boomerang. Set sprite X.
	void Start () 
	{
		startingPosition = gameObject.GetComponent<Rigidbody2D>().transform.position.x;
		returning = false;
		gameObject.GetComponent<Rigidbody2D>().velocity = velocity * boomerangSpeed;
		if (GetComponent<Rigidbody2D>().velocity.x > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}
		else if (GetComponent<Rigidbody2D>().velocity.x < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentPosition = gameObject.GetComponent<Rigidbody2D>().transform.position.x;
		travelTime = Time.deltaTime * boomerangSpeed;
		if (!returning)
		{
			//keep moving forward.
			thrownDistance += travelTime;
			if (thrownDistance >= range)
			{
				returning = true;
				changeDirection = true;
			}
		}
		else if (returning && changeDirection)
		{
			if (GetComponent<Rigidbody2D>().velocity.x > 0)
			{
				gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * boomerangSpeed;
			}
			else
			{
				gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * boomerangSpeed;
			}
			changeDirection = false;
		}
		if (returning && currentPosition == startingPosition)
		{
			Destroy(gameObject, 0);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{	
		if (gameObject != null && other.tag != "Scanner" && other.tag != "EnemyProjectile" && other.gameObject != gameObject && other.tag != "PlayerBullet")
		{
			Destroy(gameObject);
		}
		if (other.tag == "Player")
		{
			other.gameObject.GetComponent<Health>().TakeDamage(damage);
		}
	}
}
