using UnityEngine;
using System.Collections;

namespace DinoDuel
{
	public class PartDestroyer : MonoBehaviour
	{
		SpriteRenderer spriteRenderer;
		// Use this for initialization
		void Start ()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if(!spriteRenderer.isVisible)
				GameObject.Destroy(gameObject);
		}
	}
}
