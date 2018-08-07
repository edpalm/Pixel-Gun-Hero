using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour 
{

// Time before platform starts to fall.
public float timeBeforeFalling;

void OnTriggerEnter2D(Collider2D other)
{
	if(other.gameObject.tag == "Player")
		{
			StartCoroutine("Fall");
		}
}

IEnumerator Fall()
{
	yield return new WaitForSeconds(timeBeforeFalling);
	gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
}

}
