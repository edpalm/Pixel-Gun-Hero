using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour 
{

private GameObject player;

// X & Y clamp settings.
// Starting values.
public float xMinDefault;
public float xMaxDefault;
public float yMinDefault;
public float yMaxDefault;

// Current Values.
public float xMin;
public float xMax;
public float yMin;
public float yMax;
	// Use this for initialization
	// Set Default camera values.
	void Start () 
	{
		xMin = xMinDefault;
		xMax = xMaxDefault;
		yMin = yMinDefault;
		yMax = yMaxDefault;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Lateupdate to track objects that moved inside Update.
	void LateUpdate () 
	{
		float x= Mathf.Clamp(player.transform.position.x, xMin, xMax);
		float y= Mathf.Clamp(player.transform.position.y, yMin, yMax);
		gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
	}
}
