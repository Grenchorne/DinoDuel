using UnityEngine;
using System.Collections.Generic;
using Vexe.Runtime.Types;

namespace DinoDuel
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class Lava : BetterBehaviour
	{
		void OnTriggerEnter2D(Collider2D collider)
		{
			Dino dino = collider.GetComponentInParent<Dino>();
			Announcer.instance.announce(Announcer.Announcement.OutOfBounds);
			if(dino && dino.Health > 0)	dino.die(Dino.DeathType.Lava);
		}
	}
}
