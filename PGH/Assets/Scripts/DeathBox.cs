using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour 
{
private GameManager gameManager;

// Use this for initialization
void Start ()
{
	Debug.Log("deathbox");
	gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
}
void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.gameObject.tag);
		Debug.Log("DEAZZUBOXXU");
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("playertag");
			Debug.Log(gameManager);
			gameManager.EndGame();
		}
	}
}
