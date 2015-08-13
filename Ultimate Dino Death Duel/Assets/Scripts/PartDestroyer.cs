using UnityEngine;
using System.Collections;

namespace DinoDuel
{
	public class PartDestroyer : MonoBehaviour
	{
		Renderer renderer;
		// Use this for initialization
		void Start ()
		{
			renderer = GetComponent<Renderer>();
			if(!renderer)
				renderer = GetComponentInChildren<Renderer>();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if(!renderer.isVisible)
				GameObject.Destroy(gameObject);
		}
	}
}
