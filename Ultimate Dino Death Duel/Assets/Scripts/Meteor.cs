using UnityEngine;
using System.Collections;

namespace DinoDuel
{
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class Meteor : MonoBehaviour
	{
		public Dino target;
		Transform targetHead;
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

			switch(target.player)
			{
				case Dino.Player.Player1:
					targetHead = GameObject.Find("Blue_Head").transform;
					break;
				case Dino.Player.Player2:
					targetHead = GameObject.Find("Red_Head").transform;
					break;
			}
			rigidBody2D.velocity = new Vector2(0, -100);
		}

		void FixedUpdate()
		{
			if(targetHead)
				transform.position = new Vector2(targetHead.transform.position.x, transform.position.y);
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			Dino dino = collider.GetComponentInParent<Dino>();
			if(dino && dino == target)
				collidedWithDino = true;

			else if(collider.name.Contains("Platform") && collidedWithDino)
			{
				target.die(Dino.DeathType.Meteor);
				gameObject.AddComponent<PartDestroyer>();
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