using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour 
{
private GameManager gameManager;

// Use this for initialization
void Start ()
{
	gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
}
void OnTriggerEnter2D(Collider2D other)
	{	
		if(other.tag == "Player")
		{
			gameManager.EndGame();
		}
		if (other.tag != "Player" && other.gameObject != null && other.tag != "Scanner")
		{
			Destroy(other.gameObject);
		}
	}
}
