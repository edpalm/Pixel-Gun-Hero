using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour 
{
	public float damage;

	public float travelTime;

	public Vector2 velocity;

	public float bulletSpeed;

	public GameObject impactVfx;

	public float impactVfxDuration;

	// Use this for initialization
	void Start () 
	{
		gameObject.GetComponent<Rigidbody2D>().velocity = velocity * bulletSpeed;
		if (GetComponent<Rigidbody2D>().velocity.x > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
			impactVfx.GetComponent<SpriteRenderer>().flipX = false;
		}
		else if (GetComponent<Rigidbody2D>().velocity.x < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
			impactVfx.GetComponent<SpriteRenderer>().flipX = true;
		}

		if (gameObject != null)
		{
			Destroy(gameObject, travelTime);
		}
	}

	// Update is called once per frame
	 /* void Update () {
	} */

	void OnTriggerEnter2D(Collider2D other)
	{	
		if (gameObject != null && other.tag != "Scanner" && other.tag != "Player")
		{
			Destroy(gameObject);
			GameObject impact = (GameObject)Instantiate(impactVfx, gameObject.transform.position, gameObject.transform.rotation);	
			Destroy(impact, impactVfxDuration);
		}
		if (other.gameObject.tag == "Enemy")
		{
			Debug.Log("Enemy Hit!");
			other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
			
		}
	}
}
