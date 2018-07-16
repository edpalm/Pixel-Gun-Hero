using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	// Use this for initialization
	private Health playerHealth;
	public int damage = 1;

	void Start()
	{
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			playerHealth.TakeDamage(damage);
		}
	}

}
