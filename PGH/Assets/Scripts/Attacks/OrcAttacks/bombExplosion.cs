using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombExplosion : MonoBehaviour 
{
	public int damage;
	
	public float lifeTime;
	// Use this for initialization
	void Start () 
	{
	Destroy(gameObject, lifeTime)	;
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<Health>().TakeDamage(damage);
		}
	}
}
