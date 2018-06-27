using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHorizontal : MonoBehaviour {
	private Rigidbody2D rigidBody2d;
	private Animator animator;
	private float moveX;

// Player movement speed for walking and sprinting.
public float initialSpeed = 7f;
public float runMultiplier = 1.5f;
public float playerSpeed = 7f;

// Player direction
private bool isFacingRight = true;

// Use this for initialization
void Start ()
{
	rigidBody2d = gameObject.GetComponent<Rigidbody2D>();	
	animator = gameObject.GetComponent<Animator>(); // maybe remove gameObject.
}

// Update is called once per frame
void Update () 
{
	moveX = Input.GetAxis("Horizontal");
}

void FixedUpdate () 
{
	PlayerMovement(moveX);
}

void PlayerMovement(float moveX)
{	
	// Sprint
	if (Input.GetButton("Sprint"))
	{
		playerSpeed = initialSpeed * runMultiplier;
	}
	else 
	{
		playerSpeed = initialSpeed;
	}
	// Player direction.
	if (moveX > 0.0f && isFacingRight == false)
	{
		InvertPlayerDirection();
	}
	else if (moveX < 0.0f && isFacingRight == true)
	{
		InvertPlayerDirection();
	}
	rigidBody2d.velocity = new Vector2 (moveX * playerSpeed, rigidBody2d.velocity.y);	
	animator.SetFloat("Speed", Mathf.Abs(moveX));
	}

	void InvertPlayerDirection()
	{
		isFacingRight = !isFacingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale; 
	}
}
