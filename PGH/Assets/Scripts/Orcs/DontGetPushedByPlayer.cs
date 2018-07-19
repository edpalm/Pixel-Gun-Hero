/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGetPushedByPlayer : MonoBehaviour 
{
	private Rigidbody2D rigidBody2d;
	// Use this for initialization
	void Start () 
	{
		rigidBody2d = gameObject.GetComponent<Rigidbody2D>();		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (rigidBody2d.isKinematic == true)
		{
			rigidBody2d.isKinematic = false;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Orc")
		{			
			rigidBody2d.bodyType = RigidbodyType2D.Kinematic;
			Debug.Log(rigidBody2d.bodyType);
			rigidBody2d.velocity = Vector2.zero;
			// hit by player variable?
		}
	}
} */
