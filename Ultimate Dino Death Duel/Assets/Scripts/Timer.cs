using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DinoDuel
{
	public class Timer : MonoBehaviour
	{
		public float roundTime;
		public bool roundOver = false;
		public Text timerText;

		public Dino player1;
		public Dino player2;
		
		void Start ()
		{
			roundOver = false;
			roundTime = 10;
		}
	
		void Update()
		{
			roundTime -= Time.deltaTime;
			if(roundTime <= 0)
			{
				roundTime = 0;
				roundOver = true;
				endRound();
			}


			timerText.text = Mathf.Floor(roundTime).ToString();
		}

		private void endRound()
		{
			Meteor meteor_p1 = new GameObject("MeteorP1").AddComponent<Meteor>();
			Meteor meteor_p2 = new GameObject("MeteorP2").AddComponent<Meteor>();
			
			Vector3 p1Pos = player1.transform.position;
			meteor_p1.transform.position = new Vector3(p1Pos.x, 50, p1Pos.z);

			Vector3 p2Pos = player2.transform.position;
			meteor_p2.transform.position = new Vector3(p2Pos.x, 50, p2Pos.z);
			//player1.die(Dino.DeathType.Time);
			//player2.die(Dino.DeathType.Time);
		}
	}
}