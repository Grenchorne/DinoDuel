using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DinoDuel
{
	public class Timer : MonoBehaviour
	{
		public float roundTime;

		private bool _roundOver;
		public bool roundOver
		{
			get { return _roundOver; }
			set
			{
				_roundOver = value;
				if(value)	endRound();
			}
		}

		public Text timerText;

		public Dino player1;
		public Dino player2;

		public Menu restartMenu;

		public Meteor endRoundMeteor;
		
		void Awake ()
		{
			roundOver = false;
			restartMenu.gameObject.SetActive(false);
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
				//endRound();
			}
			timerText.text = Mathf.Floor(roundTime).ToString();
		}

		private void endRound()
		{
			if(player1.isActiveAndEnabled)
			{
				Meteor meteor = GameObject.Instantiate<Meteor>(endRoundMeteor);
				meteor.target = player1;
				player1.enabled = false;
			}

			if(player2.isActiveAndEnabled)
			{
				Meteor meteor = GameObject.Instantiate<Meteor>(endRoundMeteor);
				meteor.target = player2;
				player2.enabled = false;
			}
			restartMenu.gameObject.SetActive(true);
		}
	}
}