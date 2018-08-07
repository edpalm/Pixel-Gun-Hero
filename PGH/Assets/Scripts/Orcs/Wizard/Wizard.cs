using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : EnemyController 
{
	public bool facingLeft;
	// Projectile reference and spawnpoint from character.
	public GameObject bullet;
	public Transform bulletSpawner;
	
	protected override void Attack()
	{	
		enemyRigidBody.velocity = Vector2.zero;
		animator.SetTrigger("Attack");
		Debug.Log("Wizard attacking!");	
		if (isFacingRight)
		{
			GameObject wizardBullet = (GameObject)Instantiate (bullet, bulletSpawner.position, bulletSpawner.rotation);	
			wizardBullet.GetComponent<WizardBullet>().velocity = Vector2.right;
		}
		else
		{
			GameObject wizardBullet = (GameObject)Instantiate (bullet, bulletSpawner.position, bulletSpawner.rotation);	
			wizardBullet.GetComponent<WizardBullet>().velocity = Vector2.left;
		}
	}
}
