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
			if(dino)	dino.die(Dino.DeathType.Lava);
			Debug.Log(dino);
		}
	}
}
