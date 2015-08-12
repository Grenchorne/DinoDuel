using UnityEngine;
using System.Collections;

namespace DinoDuel
{
	public class Cam_Normalizer : MonoBehaviour
	{
		Transform dino1;
		Transform dino2;
		public bool frozen = false;

		private static readonly float X_MIN = -8f;
		private static readonly float X_MAX = -X_MIN;
		private static readonly float Z_MIN = -6f;
		private static readonly float Z_MAX = -11f;
		private static readonly float Y_Pos = 4.5f;

		void Awake ()
		{
			dino1 =  this.GetComponent<Timer>().player1.transform.FindChild("Blue_Body").transform;
			dino2 =  this.GetComponent<Timer>().player2.transform.FindChild("Red_Body").transform;
		}
		void Update ()
		{
			if(!dino1 || !dino2)	frozen = true;
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
