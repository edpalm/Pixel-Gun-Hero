using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour 
{
	public int damage;
	public GameObject explosion;
	// Offset of explosion instance.
	private Vector2 explosionOffset = new Vector2(0f, 1.2f);
	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag != "Enemy" && other.tag != "Scanner" && other.tag != "EnemyProjectile")
		{
			if (other.tag == "Player")
			{
				other.GetComponent<Health>().TakeDamage(damage);
			}
			Destroy(gameObject);
		  Instantiate (explosion, (Vector2)transform.position + explosionOffset * transform.localScale.x, transform.rotation);
		}
	}
}
