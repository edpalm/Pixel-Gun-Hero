using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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
	public bool grounded;
	public bool canDoubleJump;

	public bool jumped;

	// Dashing.
	private bool isDashing;
	public float dashSpeed;
	public int tapsToDash;
	public float dashResetTime;
	private float buttonPressResetTime;
	private int buttonPressCounter = 0;
	public float dashDuration;

	private float dashTime;

// Attacking.
	public GameObject bullet;
	public Vector2 velocity;
	public Vector2 offset = new Vector2(0.4f, 0);
	public float fireRate;
	private float nextFire;

	public bool attack;

	// Use this for initialization
	void Start () {
		attack = false;
		jumped = false;
		isRunning = false;
		isFacingRight = true;
		isDashing = false;
		canDoubleJump = true;
		rigidBody2d = gameObject.GetComponent<Rigidbody2D>();	
		animator = gameObject.GetComponent<Animator>();
		dashTime = dashDuration;
	}
	
	// Update is called once per frame
	void Update () {
		CheckPlayerInput();	
		moveX = Input.GetAxis("Horizontal");
		CheckPlayerFacing();
		PlayAnimations();
	}
	void FixedUpdate () 
	{
		// Perform player movement.
		PerformActions();
	}

	void CheckPlayerInput ()
	{
		if (Input.GetButtonDown("Gun") && Time.time > nextFire)
		{
			attack = true;
		}
		if (Input.GetButton("Sprint"))
		{
			isRunning = true;
		}
		else
		{
			isRunning = false;
		}
		if (Input.GetButtonDown("Jump"))
		{
			jumped = true;
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
			}
			else 
			{
				buttonPressCounter += 1;
				Debug.Log(buttonPressCounter);
				buttonPressResetTime = dashResetTime;
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

	void PerformActions()
	{	
		if (attack)
		{
			nextFire = Time.time + fireRate;
			GameObject gameObject = (GameObject) Instantiate (bullet, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (velocity.x * transform.localScale.x, velocity.y);
			attack = false;
		}
		else if (isDashing)
		{
			if (dashTime > 0)
			{	
				dashTime -= Time.deltaTime;
				if (isFacingRight)
				{
					rigidBody2d.velocity = Vector2.right * dashSpeed;	
				}
				else if (!isFacingRight)
				{
					rigidBody2d.velocity = Vector2.left * dashSpeed;
				}
			}
			else
			{
				isDashing = false;
				dashTime = dashDuration;
			}
		}
		else if (jumped)
		{
			if (grounded)
			{
				rigidBody2d.AddForce(Vector2.up * playerJumpPower);
				canDoubleJump = true;
			}
			else
			{
				if (canDoubleJump)
				{
					rigidBody2d.AddForce(Vector2.up * playerJumpPower);
					canDoubleJump = false;
				}
			}
			jumped = false;
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
		if (!grounded && attack)
		{
			animator.SetTrigger("Jumpattack");
		}
		else if (attack)
		{
			animator.SetTrigger("Attack");
		}
		if (isDashing)
		{
			animator.SetTrigger("Dash");
		}

		if (!grounded)
		{
			animator.SetBool("Jump", true);
			animator.SetBool("Landed", false);
		}
		else if (grounded)
		{
			animator.SetBool("Jump", false);
			animator.SetBool("Landed", true);
		}
	}
}
