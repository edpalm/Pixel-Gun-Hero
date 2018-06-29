using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundchecker : MonoBehaviour {
	private PlayerAttributes playerAttributes;

	// Use this for initialization
	void Start () {
		playerAttributes = gameObject.GetComponentInParent<PlayerAttributes>();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		playerAttributes.grounded = true;
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		playerAttributes.grounded = true;
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		playerAttributes.grounded = false;
	}
	
	// Update is called once per frame
}
