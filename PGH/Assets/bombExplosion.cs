using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombExplosion : MonoBehaviour 
{
	public int damage;
	Health playerHealth;
	public float lifeTime;
	// Use this for initialization
	void Start () {
	playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
	Destroy(gameObject, lifeTime)	;
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			playerHealth.TakeDamage(damage);
		}
	}
}
