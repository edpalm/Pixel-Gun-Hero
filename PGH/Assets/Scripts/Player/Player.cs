using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

// Player movement Speed
public float initialSpeed = 7f;
public float runMultiplier;
public float playerSpeed = 7f;

// Player direction
private bool isFacingRight = false;

// Player jumping.
public int playerJumpPower = 1250;
public bool grounded;
public bool canDoubleJump;

// Player horizontal movement.
private float moveX;

private Rigidbody2D rigidBody2d;
// private Animator animator;

// Use this for initialization
void Start () 
{
	rigidBody2d = gameObject.GetComponent<Rigidbody2D>();
//	animator = gameObject.GetComponent<Animator>();	
} 
// Update is called once per frame
void Update () 
{

	PlayerMovement();
	
}
void PlayerMovement()
{
	moveX = Input.GetAxis("Horizontal");
	// Sprint
	if (Input.GetButton("Sprint"))
	{
		playerSpeed = initialSpeed * runMultiplier;
	}
	else 
	{
		playerSpeed = initialSpeed;
	}
	// Jumping
	if (Input.GetButtonDown("Jump"))
	{
		Debug.Log(grounded);
		if (grounded)
		{
			Jump();
			canDoubleJump = true;
		}
		else
		{
			if (canDoubleJump)
			{
				canDoubleJump = false;
				rigidBody2d.velocity = new Vector2(rigidBody2d.velocity.x, 0);
				Jump();
			}
		}
		
	}
	// DoubleJump
	// Dashing

	// Animations.
	/*
		Idle
		Walking
		Running
		Jump
		Jump Attack
	
		*/
	// Player direction.
	if (moveX < 0.0f && isFacingRight == false)
	{
		ReversePlayerFacing();
	}
	else if (moveX > 0.0f && isFacingRight == true)
	{
		ReversePlayerFacing();
	}
	// Physics.
	rigidBody2d.velocity = new Vector2 (moveX * playerSpeed, rigidBody2d.velocity.y);
}
void Jump()
{
	rigidBody2d.AddForce(Vector2.up * playerJumpPower);
}

void ReversePlayerFacing()
{
	isFacingRight = !isFacingRight;
	Vector2 localScale = gameObject.transform.localScale;
	localScale.x *= -1;
	transform.localScale = localScale; 
}

}

