using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour 
{
	private EnemyHealth enemyHealth;
	public float damage;

	public float travelTime;

	// Use this for initialization
	void Start () {
		enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
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

	// Update is called once per frame
	 /* void Update () {
	} */

	void OnTriggerEnter2D(Collider2D other)
	{	
		if (gameObject != null)
		{
			Destroy(gameObject);
		}
		if (other.gameObject.tag == "Enemy")
		{
			Debug.Log("Enemy Hit!");
			other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
			
		}
	}
}
