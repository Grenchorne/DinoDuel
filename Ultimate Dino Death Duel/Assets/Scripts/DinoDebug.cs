using UnityEngine;
using Vexe.Runtime.Types;

namespace DinoDuel
{
	public class DinoDebug : BetterBehaviour
	{

		void Awake()
		{
			#if !UNITY_EDITOR || UNITY_RELEASE
				Destroy(gameObject);
			#endif
		}

	}
}
