using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {


	private Animator animator;
	public GameObject bullet;
	public Vector2 velocity;
	public Vector2 offset = new Vector2(0.4f, 0);

	public float fireRate;
	private float nextFire;
	// Use this for initialization
	void Start () {
	animator = gameObject.GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
		CheckForInput();
	}

	void CheckForInput ()
	{
		if (Input.GetButtonDown("Gun") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			GameObject gameObject = (GameObject) Instantiate (bullet, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (velocity.x * transform.localScale.x, velocity.y);
			animator.SetTrigger("Attack");
			// fix animation according to firerate.		
		}
	}

	void AttackAnimations ()
	{

	}
}
