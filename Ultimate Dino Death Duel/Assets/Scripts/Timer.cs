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

		public Meteor endRoundMeteor;
		
		void Start ()
		{
			roundOver = false;
		}
	
		void Update()
		{
			if(roundOver)
				return;
			
			roundTime -= Time.deltaTime;
			
			if(roundTime <= 0 && !roundOver)
			{
				roundTime = 0;
				roundOver = true;
				endRound();
			}


			timerText.text = Mathf.Floor(roundTime).ToString();
		}

		private void endRound()
		{
			if(player1.isActiveAndEnabled)
			{
				//Disable Input
				Meteor meteor = GameObject.Instantiate<Meteor>(endRoundMeteor);
				meteor.target = player1;
			}

			if(player2.isActiveAndEnabled)
			{
				//Disable Input
				Meteor meteor = GameObject.Instantiate<Meteor>(endRoundMeteor);
				meteor.target = player2;
			}
		}
	}
}