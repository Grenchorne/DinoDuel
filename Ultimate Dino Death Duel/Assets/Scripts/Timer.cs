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
		Announcer announcer;
		
		[Serialize][Hide]
		private Section _section;
		[Show]
		public Section section
		{
			get { return _section; }
			set
			{
				if(_section == value)	return;
				switch(value)
				{
					case Section.Ready:
						announcer.announce(Announcer.Announcement.Ready);
						time = READY_TIME;
						restartMenu.gameObject.SetActive(false);
						readyText.gameObject.SetActive(true);
						lockControls();
						break;
					case Section.Duel:
						announcer.announce(Announcer.Announcement.Duel);
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
						if(player1)
						{
							if(player1.isAlive)
							{
								announcer.announce(Announcer.Announcement.RedDeath);
								announcer.announce(Announcer.Announcement.BlueWins);
							}
						}
						
						if(player2)
						{
							if(player2.isAlive)
							{
								announcer.announce(Announcer.Announcement.BlueDeath);
								announcer.announce(Announcer.Announcement.RedWins);
							}
						}

						//killPlayers();
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
			announcer = FindObjectOfType<Announcer>();
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
			if(player1.isAlive)
			{
				Meteor meteor = GameObject.Instantiate<Meteor>(endRoundMeteor);
				meteor.target = player1;
			}

			if(player2.isAlive)
			{
				Meteor meteor = GameObject.Instantiate<Meteor>(endRoundMeteor);
				meteor.target = player2;
			}
		}

		private void setPlayerWin(Dino.Player player)
		{
			switch(player)
			{
				case Dino.Player.Player1:
					break;
				case Dino.Player.Player2:
					break;
			}
		}

		public override void Reset()
		{
			base.Reset();
			announcer = GetComponent<Announcer>();
		}
	}
}