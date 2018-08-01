using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ofly : EnemyController 
{
	public Transform bombDropper;
	public GameObject bomb;
	public override void Attack() 
	{
		Debug.Log ("Dropping bomb!");
		animator.SetTrigger("Attack");
		Instantiate (bomb, bombDropper.position, bombDropper.rotation);
	}
}
