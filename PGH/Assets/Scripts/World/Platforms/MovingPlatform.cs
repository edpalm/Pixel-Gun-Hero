using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour 
{
	// Movement speed of platform.
	public float speed;

	// Platform starting position.
	private Vector3 startingPosition;

	// Destination object starting position.
	private Vector3 destinationPosition;

	// Position of current target destination.
	private Vector3 moveTowards;

	public Transform platform;

	public Transform destination;




	void Start () 
	{
		startingPosition = platform.localPosition;
		destinationPosition = destination.localPosition;
		moveTowards = destinationPosition;
	}
		
	// Update is called once per frame
	void FixedUpdate () 
	{
		platform.localPosition = Vector2.MoveTowards(platform.localPosition, moveTowards, speed * Time.deltaTime);
		// Change direction of traversal upon arrival at destination.
		if (Vector2.Distance(platform.localPosition, moveTowards) == 0)
		{
			if (moveTowards == destinationPosition)
			{
				moveTowards = startingPosition;
			}
			else
			{
				moveTowards = destinationPosition;
			}
		}
	}
}
