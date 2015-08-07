using UnityEngine;
using System.Collections;

namespace DinoDuel
{
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class Meteor : MonoBehaviour
	{
		public Dino target;
		public ParticleSystem explosion;

		private Rigidbody2D rigidBody2D;
		private BoxCollider2D boxCollider2D;
		bool collidedWithDino = false;


		void Start ()
		{
			rigidBody2D = GetComponent<Rigidbody2D>();
			boxCollider2D = GetComponent<BoxCollider2D>();
			boxCollider2D.isTrigger = true;
			if(explosion)
				explosion.enableEmission = false;

			rigidBody2D.velocity = new Vector2(0, -100);
		}

		void FixedUpdate()
		{
			if(target)
				transform.position = new Vector2(target.transform.position.x, transform.position.y);
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			Dino dino = collider.GetComponentInParent<Dino>();
			if(dino && dino == target)
				collidedWithDino = true;

			else if(collider.name.Contains("Platform") && collidedWithDino)
			{
				Debug.Log("platform");
				target.die(Dino.DeathType.Meteor);
				explode();
			}
		}

		private void explode()
		{
			ParticleSystem expl = GameObject.Instantiate<ParticleSystem>(explosion);
			expl.transform.position = transform.position;
			expl.Emit(10);
		}
	}
}
