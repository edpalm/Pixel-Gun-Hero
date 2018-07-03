using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHorizontal : MonoBehaviour {
	private Rigidbody2D rigidBody2d;
	private Animator animator;
	private float moveX;

	// Player movement speed for walking and running.
	private bool isRunning;
	public float initialSpeed = 7f;
	public float runMultiplier = 1.5f;
	public float playerSpeed = 7f;

	// Player direction.
	private bool isFacingRight;

	// Jumping.
	public int playerJumpPower;

	// Dashing.
	private bool isDashing;
	public float dashSpeed;
	public int tapsToDash;
	public float resetTime;
	private float buttonPressResetTime;
	private int buttonPressCounter = 0;

	public float dashDuration;

	private float dashTime;


	// Use this for initialization
	void Start ()
	{
		isRunning = false;
		isFacingRight = true;
		isDashing = false;
		rigidBody2d = gameObject.GetComponent<Rigidbody2D>();	
		animator = gameObject.GetComponent<Animator>();
		dashTime = dashDuration;
	}

	// Update is called once per frame
	void Update () 
	{
		CheckPlayerInput();	
		moveX = Input.GetAxis("Horizontal");
		CheckPlayerFacing();
		PlayAnimations();
	}

	void FixedUpdate () 
	{
		// Perform player movement.
		PerformMovement();
	}

	void CheckPlayerInput ()
	{
		if (Input.GetButton("Sprint"))
		{
			isRunning = true;
		}
		else
		{
			isRunning = false;
		}
		CheckForDoubleTap();
	}

	void CheckForDoubleTap()
	{
		if (Input.GetButtonDown("Horizontal"))
		{
			if (buttonPressResetTime > 0 && buttonPressCounter == tapsToDash - 1)
			{
				// If doubletap.
				isDashing = true;
				if (isFacingRight)
				{
					Debug.Log("Dash Right");
					rigidBody2d.velocity = Vector2.right * dashSpeed;	
				}
				else if (!isFacingRight)
				{
					Debug.Log("Dash Left");
					rigidBody2d.velocity = Vector2.left * dashSpeed;
				}
			}
			else 
			{
				buttonPressCounter += 1;
				Debug.Log(buttonPressCounter);
				buttonPressResetTime = resetTime;
			}
		}
		// Reset dash button press timer.
		if (buttonPressResetTime > 0)
		{
			buttonPressResetTime -= 1 * Time.deltaTime;
		}
		else
		{
			buttonPressCounter = 0;
		}
	}
	void CheckPlayerFacing ()
	{
		// Player direction.
		if (moveX > 0.0f && isFacingRight == false)
		{
			InvertPlayerDirection();
		}
		else if (moveX < 0.0f && isFacingRight == true)
		{
			InvertPlayerDirection();
		}
	}

	void InvertPlayerDirection()
	{
		isFacingRight = !isFacingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale; 
	}

	void PerformMovement()
	{	
		if (isDashing)
		{
			if (dashTime > 0)
			{	
				dashTime -= Time.deltaTime;
			}
			else
			{
				isDashing = false;
				dashTime = dashDuration;
			}
		}
		else
		{
		// Sprint
			if (isRunning)
			{
				playerSpeed = initialSpeed * runMultiplier;
			}
			else 
			{
				playerSpeed = initialSpeed;
			}
			rigidBody2d.velocity = new Vector2 (moveX * playerSpeed, rigidBody2d.velocity.y);	
			animator.SetFloat("Speed", Mathf.Abs(moveX));
		}
	}

	void PlayAnimations()
	{
		if (isDashing) 
		{
			animator.SetBool("Dashing", true);
		}
		else 
		{
			animator.SetBool("Dashing", false);
		}
		
	}
}
