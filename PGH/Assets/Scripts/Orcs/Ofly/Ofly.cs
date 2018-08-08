using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ofly : EnemyController 
{
	public Transform bombDropper;
	public GameObject bomb;
	protected override void Attack () 
	{
		animator.SetTrigger("Attack");
		Instantiate (bomb, bombDropper.position, bombDropper.rotation);
	}
}
