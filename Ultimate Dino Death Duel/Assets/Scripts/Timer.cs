using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vexe.Runtime.Types;

namespace DinoDuel
{
	public class Timer : BetterBehaviour
	{
		private static float READY_TIME = 3;
		private static float DUEL_TIME = 60;
		private static float POST_TIME = 0;

		public enum Section { None, Ready, Duel, Post }
		
		[Serialize][Hide]
		private Section _section;
		[Show]
		public Section section
		{
			get { return _section; }
			set
			{
				if(_section == value)	return;
				switch(_section)
				{
					case Section.Ready:
						break;
					case Section.Duel:
						break;
					case Section.Post:
						//endPost
						break;
				}

				switch(value)
				{
					case Section.Ready:
						time = READY_TIME;
						restartMenu.gameObject.SetActive(false);
						readyText.gameObject.SetActive(true);
						lockControls();
						break;
					case Section.Duel:
						time = DUEL_TIME;
						readyText.gameObject.SetActive(false);
						duelText.gameObject.SetActive(true);
						restartMenu.gameObject.SetActive(false);
						unlockControls();
						break;
					case Section.Post:
						time = POST_TIME;
						restartMenu.gameObject.SetActive(true);
						lockControls();
						killPlayers();
						//GetComponent<Cam_Normalizer>().frozen = true;
						break;
				}
				_section = value;
			}
		}

		public float time;

		public Text timerText;
		public Text readyText;
		public Text duelText;

		public Dino player1;
		public Dino player2;

		public Menu restartMenu;
		public Meteor endRoundMeteor;
	
		void Awake()
		{
			section = Section.Ready;
		}

		void Update()
		{
			time -= Time.deltaTime;
			switch(section)
			{
				case Section.Ready:
					if(time <= 0)	section = Section.Duel;
					timerText.text = Mathf.Floor(time).ToString();
					break;

				case Section.Duel:
					if(time <= 0)	section = Section.Post;
					if(duelText.isActiveAndEnabled && time < DUEL_TIME - 2)
						duelText.gameObject.SetActive(false);
					timerText.text = Mathf.Floor(time).ToString();
					break;
			}
		}

		private void lockControls()
		{
			if(player1)	player1.enabled = false;
			if(player2)	player2.enabled = false;
		}

		private void unlockControls()
		{
			if(player1)	player1.enabled = true;
			if(player2)	player2.enabled = true;
		}

		private void killPlayers()
		{
			if(player1)
			{
				Meteor meteor = GameObject.Instantiate<Meteor>(endRoundMeteor);
				meteor.target = player1;
			}

			if(player2)
			{
				Meteor meteor = GameObject.Instantiate<Meteor>(endRoundMeteor);
				meteor.target = player2;
			}
		}
	}
}