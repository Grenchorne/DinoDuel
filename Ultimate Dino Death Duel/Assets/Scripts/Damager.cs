using UnityEngine;
using System.Collections.Generic;

namespace DinoDuel
{
	public class Damager : MonoBehaviour
	{
		public float damageOnHit;
		private float damageMod = 0.0005f;
		private Joint2D joint2D;
		private Rigidbody2D rigidBody2D;

		void Start()
		{
			joint2D = GetComponent<Joint2D>();
			rigidBody2D = GetComponent<Rigidbody2D>();
		}

		private void OnTriggerStay2D(Collider2D collider)
		{
			float angularV = rigidBody2D.angularVelocity;
			if (angularV > 1)
			{
				Dino enemy = collider.GetComponentInParent<Dino>();
				if(enemy)
					enemy.Health -= angularV * damageMod * damageOnHit;
			}
		}
	}
}
