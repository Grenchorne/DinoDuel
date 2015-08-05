using UnityEngine;
using System.Collections.Generic;

namespace DinoDuel
{
	public class Damager : MonoBehaviour
	{
		private Dino dino;
		public float damageOnHit;
		private float damageMod = 0.0005f;
		private Rigidbody2D rigidBody2D;

		public bool applyingDamage { get; protected set; }

		void Start()
		{
			rigidBody2D = GetComponent<Rigidbody2D>();
			dino = GetComponentInParent<Dino>();
		}

		private void OnTriggerStay2D(Collider2D collider)
		{
			float angularV = rigidBody2D.angularVelocity;
			applyingDamage = angularV > 1;

			if(applyingDamage)
			{
				if(!collider.GetComponentInParent<Dino>())	return;
				dino.damageToApply += angularV * damageMod * damageOnHit;
			}
		}
	}
}
