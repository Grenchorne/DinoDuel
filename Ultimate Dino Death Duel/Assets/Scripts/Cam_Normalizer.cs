using UnityEngine;
using System.Collections;

namespace DinoDuel
{

	public class Cam_Normalizer : MonoBehaviour
	{

		private Transform dino1;
		private Transform dino2;

		private static readonly float Z_DIST_MIN = -6f;
		private static readonly float Z_DIST_MAX = -11f;
		private static readonly float Y_Pos = 4.5f;

		void Start ()
		{
			dino1 = GameObject.Find("Dino1").transform;
			dino2 = GameObject.Find("Dino2").transform;
		}
		void Update ()
		{
			float xPos = Vector2.Lerp(dino1.position, dino2.position, 0.5f).x;
			float zPos = -1* Vector3.Distance(dino1.position, dino2.position);
			if(Mathf.Abs(zPos) < Mathf.Abs(Z_DIST_MIN))		zPos = Z_DIST_MIN;
			else if(Mathf.Abs(zPos) > Mathf.Abs(Z_DIST_MAX))	zPos = Z_DIST_MAX;
			transform.position = new Vector3(xPos, Y_Pos, zPos);
			//float distance = Vector3.Distance(dino1.position, dino2.position);	
		}
	}
}
