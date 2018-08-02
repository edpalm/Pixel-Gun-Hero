using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour 
{
	public int damage;
	Health playerHealth;
	public GameObject explosion;
	public float explosionTime;
	public Vector2 explosionOffset = new Vector2(0.4f, 0);
	// Use this for initialization
	void Start () {
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
	}
	
	// Update is called once per frame
/* 	void Update () {
		
	} */
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Enemy" && other.tag != "Scanner")
		{
			if (other.tag == "Player")
			{
				playerHealth.TakeDamage(damage);
			}
			Destroy(gameObject);
		  Instantiate (explosion, (Vector2)transform.position + explosionOffset * transform.localScale.x, transform.rotation);
		}
		
		// Instantiate(explosion, transform.position, Quaternion.identity);
	// Instantiate (explosion, (Vector2)transform.position + explosionOffset * transform.localScale.x, Quaternion.identity);
	}
}
