using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour 
{


	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
	private CameraSystem cameraPosition;
	// Use this for initialization
	void Start () 
	{
		cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSystem>();
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			cameraPosition.yMin = yMin;
			cameraPosition.yMax = yMax;
			cameraPosition.xMin = xMin;
			cameraPosition.xMax = xMax;
		}
	}
}
