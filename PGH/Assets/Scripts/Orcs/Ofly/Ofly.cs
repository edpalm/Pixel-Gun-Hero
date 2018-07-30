using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ofly : EnemyController 
{

	public override void Attack() 
	{
		Debug.Log (gameObject + "Attacking!");
	}
}
