using UnityEngine;
using UnityEngine.UI;
using Vexe.Runtime.Types;
using System.Collections.Generic;

namespace DinoDuel
{
	[RequireComponent(typeof(AudioClipManager))]
	public class Announcer : BetterBehaviour
	{
		const float WAIT_TIME = 2;
		const string s_announce_READY = "";

		const string s_announce_BLUE_WINS = "VO_Announcer_Blue Wins";
		const string s_announce_BLUE_WINS_BONUS = "VO_Announcer_Blueh Wuens";
		const string s_announce_RED_WINS = "VO_Announcer_Red Wins";
		const string s_announce_DUEL = "VO_Announcer_Duel!";
		const string s_announce_OUTOFBOUNDS = "VO_Announcer_Out Of Bounds";
		const string s_announce_TIMEUP = "VO_Announcer_Time Up";
		const string s_announce_BlueDeath = "VO_DinoBlue Death";
		const string s_announce_RedDeath = "VO_DinoRed Death";

		public Text txt_ready;
		public Text txt_duel;
		public Text txt_outOfBounds;
		public Text txt_timeUp;
		public Text txt_blueWins;
		public Text txt_redWins;

		public enum Announcement
		{
			Ready,
			Duel,
			OutOfBounds,
			TimeUp,
			BlueWins,
			RedWins,
			BlueDeath,
			RedDeath
		}

		bool b_ready;
		bool b_duel;
		bool b_outOfBounds;
		bool b_timeUp;
		bool b_blueWins;
		bool b_redWins;
		bool b_blueDeath;
		bool b_redDeath;

		private AudioClipManager audioClipManager;
		private Queue<OnAnnounce> announcements;
		private delegate void OnAnnounce();

		public static Announcer instance
		{ get { return GameObject.FindObjectOfType<Announcer>(); } }

		float waitTime = 0;
		void Awake()
		{
			b_ready = false;
			b_duel = false;
			b_outOfBounds = false;
			b_timeUp = false;
			b_blueWins = false;
			b_redWins = false;
			b_blueDeath = false;
			b_redDeath = false;

			clearTxt();

			audioClipManager = GetComponent<AudioClipManager>();
			announcements = new Queue<OnAnnounce>();
		}


		void Update()
		{
			if(waitTime > 0)
			{
				waitTime -= Time.deltaTime;
				return;
			}
			if(announcements.Count <= 0)	return;
			OnAnnounce announce = announcements.Peek();
			if(announce != null)
			{
				announce();
				announcements.Dequeue();
			}
		}

		[Show]
		public void announce(Announcement announcement)
		{
			if(announcements == null) announcements = new Queue<OnAnnounce>();
			switch(announcement)
			{
				case Announcement.Ready:
					if(!b_ready)
					{
						announcements.Enqueue(onReady);
						b_ready = true;

					}
					break;
				case Announcement.Duel:
					if(!b_duel)
					{
						announcements.Enqueue(onDuel);
						b_duel = true;
					}
					break;
				case Announcement.OutOfBounds:
					if(!b_outOfBounds)
					{
						announcements.Enqueue(onOutOfBounds);
						b_outOfBounds = true;
						b_timeUp = true;
					}
					break;
				case Announcement.TimeUp:
					if(!b_timeUp)
					{
						announcements.Enqueue(onTimeUp);
						b_timeUp = true;
						b_outOfBounds = true;
					}
					break;
				case Announcement.BlueWins:
					if(!b_blueWins)
					{
						announcements.Enqueue(onBlueWin);
						b_blueWins = true;
						b_timeUp = true;
						b_outOfBounds = true;
					}
					break;
				case Announcement.RedWins:
					if(!b_redWins)
					{
						announcements.Enqueue(onRedWin);
						b_redWins = true;
						b_timeUp = true;
						b_outOfBounds = true;
					}
					break;
				case Announcement.BlueDeath:
					if(!b_blueDeath  && (!b_outOfBounds || !b_timeUp))
					{
						announcements.Enqueue(onBlueDeath);
						b_blueDeath = true;
					}
					break;
				case Announcement.RedDeath:
					if(!b_redDeath && (!b_outOfBounds || !b_timeUp))
					{
						announcements.Enqueue(onRedDeath);
						b_redDeath = true;
					}
					break;
			}
		}

		private void clearTxt()
		{
			txt_ready.gameObject.SetActive(false);
			txt_duel.gameObject.SetActive(false);
			txt_outOfBounds.gameObject.SetActive(false);
			txt_timeUp.gameObject.SetActive(false);
			txt_blueWins.gameObject.SetActive(false);
			txt_redWins.gameObject.SetActive(false);
		}

		private void onBlueWin()
		{
			clearTxt();
			txt_blueWins.gameObject.SetActive(true);
			int rand = Random.Range(0, 10);
			if(rand == 1)	audioClipManager.playClip(s_announce_BLUE_WINS_BONUS);
			else			audioClipManager.playClip(s_announce_BLUE_WINS);
			waitTime = WAIT_TIME;
		}

		private void onRedWin()
		{
			clearTxt();
			txt_redWins.gameObject.SetActive(true);
			audioClipManager.playClip(s_announce_RED_WINS);
			waitTime = WAIT_TIME;
		}

		private void onBlueDeath()
		{
			audioClipManager.playClip(s_announce_BlueDeath);
			waitTime = WAIT_TIME;
		}

		private void onRedDeath()
		{
			audioClipManager.playClip(s_announce_RedDeath);
			waitTime = WAIT_TIME;
		}

		private void onReady()
		{
			clearTxt();
			txt_ready.gameObject.SetActive(true);
			Debug.LogWarning("Need to get READY sound");
			return;
			audioClipManager.playClip(s_announce_READY);
			waitTime = WAIT_TIME;
		}

		private void onDuel()
		{
			clearTxt();
			txt_duel.gameObject.SetActive(true);
			audioClipManager.playClip(s_announce_DUEL);
			waitTime = WAIT_TIME;
		}

		private void onOutOfBounds()
		{
			clearTxt();
			txt_outOfBounds.gameObject.SetActive(true);
			audioClipManager.playClip(s_announce_OUTOFBOUNDS);
			waitTime = WAIT_TIME;
		}

		private void onTimeUp()
		{
			clearTxt();
			txt_timeUp.gameObject.SetActive(true);
			audioClipManager.playClip(s_announce_TIMEUP);
			waitTime = WAIT_TIME;
		}
	}
}
