using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundchecker : MonoBehaviour {
	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = gameObject.GetComponentInParent<PlayerController>();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		player.grounded = true;
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		player.grounded = true;
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		player.grounded = false;
	}
}
