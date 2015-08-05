using UnityEngine;
using System.Collections.Generic;

namespace DinoDuel
{
	public class Damager : MonoBehaviour
	{
		public float damageOnHit;
		private float damageMod = 0.0005f;
		private Rigidbody2D rigidBody2D;

		private float damageToApply;
		private bool applyingDamage;
		private Dino enemyToDamage;

		void Start()
		{
			rigidBody2D = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			if(!applyingDamage && enemyToDamage && damageToApply > 0)
			{
				applyDamage();
			}
		}

		private void OnTriggerStay2D(Collider2D collider)
		{
			float angularV = rigidBody2D.angularVelocity;
			applyingDamage = angularV > 1;

			if(applyingDamage)
			{
				Dino enemy = collider.GetComponentInParent<Dino>();
				if(!enemy)
					return;

				enemyToDamage = enemy;
				damageToApply += angularV * damageMod * damageOnHit;
			}
		}

		//Move these to Dino.cs
		private void applyDamage()
		{

			Debug.Log(damageToApply);
			if(damageToApply < 1)
				return;

			else if(damageToApply >= 1 && damageToApply < 4)
			{
				Debug.Log("small hit");
			}

			else if(damageToApply >= 4 && damageToApply < 8)
			{
				Debug.Log("med hit");
			}

			else
			{
				Debug.Log("big hit");
			}

			enemyToDamage.Health -= damageToApply;
			Debug.Log(name + " dealt " + damageToApply + " to " + enemyToDamage.name);
			damageToApply = 0;
			applyingDamage = false;
		}
	}
}
