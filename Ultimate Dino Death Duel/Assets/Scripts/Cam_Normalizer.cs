using UnityEngine;
using System.Collections;

namespace DinoDuel
{
	public class Cam_Normalizer : MonoBehaviour
	{
		private Transform dino1;
		private Transform dino2;
		public bool frozen = false;

		private static readonly float X_MIN = -6f;
		private static readonly float X_MAX = -X_MIN;
		private static readonly float Z_MIN = -6f;
		private static readonly float Z_MAX = -11f;

		private static readonly float Y_Pos = 4.5f;

		void Start ()
		{
			dino1 = GameObject.Find("Dino1").transform;
			dino2 = GameObject.Find("Dino2").transform;
		}
		void Update ()
		{
			if(frozen)	return;
			float xPos = Vector2.Lerp(dino1.position, dino2.position, 0.5f).x;
			if(xPos < X_MIN)		xPos = X_MIN;
			else if(xPos > X_MAX)	xPos = X_MAX;

			float zPos = -1* Vector3.Distance(dino1.position, dino2.position);
			if(Mathf.Abs(zPos) < Mathf.Abs(Z_MIN))		zPos = Z_MIN;
			else if(Mathf.Abs(zPos) > Mathf.Abs(Z_MAX))	zPos = Z_MAX;

			transform.position = new Vector3(xPos, Y_Pos, zPos);
		}
	}
}
