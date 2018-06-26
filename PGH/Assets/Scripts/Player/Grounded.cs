using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour {

	private Player player;

	// Use this for initialization
	void Start () {
		Debug.Log("Groundcheck initiated");
		player = gameObject.GetComponentInParent<Player>();
	}
	
	void onTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Enter");
		player.grounded = true;
	}

	void onTriggerStay2D(Collider2D col)
	{
		Debug.Log("Stay");
		player.grounded = true;
	}
	void onTriggerExit2D(Collider2D col)
	{
		Debug.Log("Exit");
		player.grounded = false;
	}
}
