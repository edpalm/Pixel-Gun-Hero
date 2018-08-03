using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScanner : MonoBehaviour 
{
	private EnemyController enemyController;

	// Use this for initialization
	void Start () 
	{
		enemyController = gameObject.GetComponentInParent<EnemyController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			enemyController.canAttackPlayer = true;
		}
	}
 
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			enemyController.canAttackPlayer = false;
		}	
	}
}
