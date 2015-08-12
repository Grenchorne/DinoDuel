using UnityEngine;
using System.Collections;

namespace DinoDuel
{
	[RequireComponent(typeof(GUIText))]
	public class DamageNumber : MonoBehaviour
	{
		Color colour = Color.red;
		float scroll = .5f;
		float duration = 1.5f;
		float alpha;

		GUIText gText;

		void Start()
		{
			alpha = 1;
			gText = GetComponent<GUIText>();
		}
	
		void Update()
		{
			float deltaTime = Time.deltaTime;
			if(alpha > 0)
			{
				Vector2 pos = transform.position;
				transform.position = new Vector2(pos.x, pos.y += scroll * deltaTime);

				alpha -= deltaTime / duration;
				gText.material.color = new Color(colour.r, colour.g, colour.b, alpha);
			}
			else
				Destroy(gameObject);
		}

		public static void Spawn(Dino.Player player, float points, Vector2 position)
		{
			DamageNumber dn = new GameObject("DamageNumber").AddComponent<DamageNumber>();
			dn.transform.position = position;
			dn.transform.rotation = Quaternion.identity;
			dn.GetComponent<GUIText>().text = points.ToString();
			switch(player)
			{
				case Dino.Player.Player1:
					dn.colour = Color.blue;
					break;
				case Dino.Player.Player2:
					dn.colour = Color.red;
					break;
				default:
					goto case Dino.Player.Player1;
			}
		}
	}
}
