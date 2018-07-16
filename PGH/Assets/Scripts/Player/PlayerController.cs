using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rigidBody2d;
	private Animator animator;
	private float moveX;

	// Player movement speed for walking and running.
	private bool isRunning;
	public float initialSpeed;
	public float runMultiplier;
	public float playerSpeed;

	// Player direction.
	private bool isFacingRight;

	// Jumping.
	public int playerJumpPower;
	public bool grounded;
	private bool canDoubleJump;
	private bool jumped;

	// Dashing.
	private bool isDashing;
	public float dashSpeed;
	public int tapsToDash;
	public float dashResetTime;
	private float buttonPressResetTime;
	private int buttonPressCounter = 0;
	public float dashDuration;
	private float dashTime;
	private bool canDashMidAir;

// Attacking.
	 // Projectile ref and spawnpoint from char.
	public GameObject bullet;
	public Vector2 velocity;
	public Vector2 offset = new Vector2(0.4f, 0);
// Attack speed.
	public float fireRate;
	private float nextFire;
	private bool attack;

	// Use this for initialization
	void Start () {
		canDashMidAir = true;
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
		if (grounded)	
		{
			canDashMidAir = true;
		}
		moveX = Input.GetAxis("Horizontal");
		CheckPlayerFacing();
		Animate();
	}
	void FixedUpdate () 
	{
		PerformActions();
	}

	///<summary>
	/// Check player inputs.
	/// Set action variables.
	/// </summary>
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

	///<summary>
	/// Check double tap of left/right movement hotkeys.
	/// Set dash action.
	///</summary>
	void CheckForDoubleTap()
	{
		if (Input.GetButtonDown("Horizontal"))
		{
			if (buttonPressResetTime > 0 && buttonPressCounter == tapsToDash - 1 && canDashMidAir)
			{
				// If doubletap.
				isDashing = true;
				if(!grounded)
				{
					canDashMidAir = false;
				}
			}
			else 
			{
				buttonPressCounter += 1;
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
	///<summary>
	/// Adjust the way the player is facing according to movement.
	///</summary> 
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
	///<summary>
	/// Change the direction the player is facing.
	///</summary>
	void InvertPlayerDirection()
	{
		isFacingRight = !isFacingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale; 
	}
	///<summary>
	/// Perform player action based on action variables.
	///</summary>
	void PerformActions()
	{	
		if (isDashing)
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
		else if (attack && !isDashing)
		{
			nextFire = Time.time + fireRate;
			GameObject gameObject = (GameObject) Instantiate (bullet, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (velocity.x * transform.localScale.x, velocity.y);
			attack = false;
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

	///<summary>
	/// Animate the player model.
	///</summary>
	void Animate()
	{
		if (!grounded && Input.GetButtonDown("Gun") && Time.time > nextFire )
		{
			animator.SetTrigger("Jumpattack");
		}
		else if (Input.GetButtonDown("Gun") && Time.time > nextFire)
		{
			animator.SetTrigger("Attack");
		}
		else if (isDashing)
		{
			animator.SetTrigger("Dash");
		}
		else if (!grounded)
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
