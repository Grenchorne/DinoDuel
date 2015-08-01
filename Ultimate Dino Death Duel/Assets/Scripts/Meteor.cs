using UnityEngine;
using System.Collections;

namespace DinoDuel
{
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class Meteor : MonoBehaviour
	{
		private Rigidbody2D rigidBody2D;
		private BoxCollider2D boxCollider2D;

		void Start ()
		{
			rigidBody2D = GetComponent<Rigidbody2D>();
			boxCollider2D = GetComponent<BoxCollider2D>();

			rigidBody2D.mass = 2000;
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			Dino dino = collider.GetComponent<Dino>();
			if(dino)
			{
				dino.die(Dino.DeathType.Meteor);
			}
		}
	}
}
