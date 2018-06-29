using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour 
{
	private PlayerAttributes playerAttributes;

	public int playerJumpPower = 1250;

	private Rigidbody2D rigidBody2d;
	private Animator animator;

// Use this for initialization
	void Start () 
	{
		rigidBody2d = gameObject.GetComponent<Rigidbody2D>();
		playerAttributes = gameObject.GetComponentInParent<PlayerAttributes>();
		animator = gameObject.GetComponent<Animator>();
	} 
	// Update is called once per frame
	void Update () 
	{
		CheckForInput();
		AnimateAir();
}

	void CheckForInput()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (playerAttributes.grounded)
			{
				Jump();
				playerAttributes.canDoubleJump = true;
			}
			else
			{
				if (playerAttributes.canDoubleJump)
				{
					Jump();
					playerAttributes.canDoubleJump = false;
				}
			}
		}	
	}
	void Jump()
	{
		rigidBody2d.AddForce(Vector2.up * playerJumpPower);
	}

	void AnimateAir()
	{
		if (!playerAttributes.grounded)
		{
			animator.SetBool("Jump", true);
			animator.SetBool("Landed", false);
		}
		else if (!playerAttributes.grounded && !playerAttributes.canDoubleJump)
		{
			animator.SetBool("Doublejump", true);
		}
		else if (playerAttributes.grounded)
		{
			animator.SetBool("Jump", false);
			animator.SetBool("Landed", true);
		}
	}

}
