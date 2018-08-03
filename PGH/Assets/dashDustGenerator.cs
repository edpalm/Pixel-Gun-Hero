using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDustGenerator : MonoBehaviour 
{
	PlayerController player;
	public GameObject dashVfx;

	public float vfxDuration;

	// Use this for initialization
	void Start () 
	{
		player = gameObject.GetComponentInParent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player.isDashing)	
		{
			GameObject dust = Instantiate (dashVfx, gameObject.transform.position, gameObject.transform.rotation);
			if (!player.isFacingRight)
			{
				dust.GetComponent<SpriteRenderer>().flipX = false;
			}
			Destroy(dust, vfxDuration);
		}
	}
}
