using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeScanner : MonoBehaviour 
{
private Warrior warrior;

	// Use this for initialization
	void Start () 
	{
		warrior = gameObject.GetComponentInParent<Warrior>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			warrior.playerInRange = true;
		}
	}
 
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			warrior.playerInRange = false;
		}	
	}
}
