using UnityEngine;
using System.Collections.Generic;

namespace DinoDuel
{
	public class Weapon : MonoBehaviour
	{
		private Dino dino;
		public float damageOnHit;
		private float damageMod = 0.0005f;
		private Rigidbody2D rigidBody2D;
		
		public bool willPush = false;
		public float pushForce = 10;
		private Rigidbody2D enemyRigidBody;
		private int directionMod;

		public bool applyingDamage { get; protected set; }

		void Start()
		{
			rigidBody2D = GetComponent<Rigidbody2D>();
			dino = GetComponentInParent<Dino>();
			switch(dino.player)
			{
				case Dino.Player.Player1:
					enemyRigidBody = GameObject.Find("Red_Body").GetComponent<Rigidbody2D>();
					directionMod = 1;
					break;
				case Dino.Player.Player2:
					enemyRigidBody = GameObject.Find("Blue_Body").GetComponent<Rigidbody2D>();
					directionMod = -1;
					break;
			}
		}

		private void OnTriggerStay2D(Collider2D collider)
		{
			float angularV = Mathf.Abs(rigidBody2D.angularVelocity);
			applyingDamage = angularV > 1;

			if(applyingDamage)
			{
				if(!collider.GetComponentInParent<Dino>())	return;
				dino.damageToApply += angularV * damageMod * damageOnHit;
			}
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if(!willPush)	return;
			float angularV = Mathf.Abs(rigidBody2D.angularVelocity);
			if(!collider.GetComponentInParent<Dino>())	return;
			enemyRigidBody.AddForce(new Vector2(angularV * pushForce * directionMod, 100));
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			applyingDamage = false;
		}

	}
}
