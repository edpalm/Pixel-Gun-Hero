using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

	public float travelTime;

	// Use this for initialization
	void Start () {
		if (GetComponent<Rigidbody2D>().velocity.x > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}
		else if (GetComponent<Rigidbody2D>().velocity.x < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}

		Destroy(gameObject, travelTime);
	}

	// Update is called once per frame
	 /* void Update () {
	} */

	void OnTriggerStay2D(Collider2D collider)
	{
		Destroy(gameObject);
	}
}
