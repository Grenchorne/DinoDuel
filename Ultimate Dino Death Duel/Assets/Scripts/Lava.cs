using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vexe.Runtime.Types;

namespace DinoDuel
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class Lava : BetterBehaviour
	{
		const float drag = 100;
		public ParticleSystem fireEruption;
		bool fired = false;

		void OnTriggerEnter2D(Collider2D collider)
		{
			Rigidbody2D body = null;
			Dino dino = collider.GetComponentInParent<Dino>();
			if(!dino)
				return;
			switch(dino.player)
			{
				case Dino.Player.Player1:
					body  = GameObject.Find("Blue_Body").GetComponent<Rigidbody2D>();
					break;
				case Dino.Player.Player2:
					body = GameObject.Find("Red_Body").GetComponent<Rigidbody2D>();
					break;
			}
			if(body) body.drag = 100;

			Announcer.instance.announce(Announcer.Announcement.OutOfBounds);
			if(dino && dino.Health > 0)	dino.die(Dino.DeathType.Lava);
			if(fireEruption && body && !fired)
			{
				ParticleSystem eruption = GameObject.Instantiate(fireEruption);
				Vector3 position = body.transform.position;
				eruption.transform.position = new Vector3(
					position.x,
					position.y - 5,
					position.z);
				eruption.Play();
				fired = true;
				//showSkeleton(dino);
				dino.showSkeleton();
			}
		}

		IEnumerable showSkeleton(Dino dino)
		{
			yield return new WaitForSeconds(1);
			dino.showSkeleton();
		}
	}
}