﻿using UnityEngine;
using Vexe.Runtime.Types;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;
using UnityEngine.Audio;

namespace DinoDuel
{	
	[RequireComponent(typeof(AudioSource))]
	public class MusicManager : BetterBehaviour
	{
		public float Level
		{
			get { return audioSource.volume; }
			set { audioSource.volume = value; }
		}
		private AudioSource audioSource;
		public enum Track
		{
			MainMenu,
			Duel,
			DinoBlueWin,
			DinoRedWin,
		}

		[Serialize]
		[Hide]
		private Track _activeTrack;
		[Show]
		public Track ActiveTrack
		{
			get { return _activeTrack; }
			set
			{
				switch(value)
				{
					case Track.MainMenu:
						currentTrack = MainMenuTrack;
						break;
					case Track.Duel:
						currentTrack = DuelTrack;
						break;
					case Track.DinoBlueWin:
						currentTrack = DinoBlueWin;
						break;
					case Track.DinoRedWin:
						currentTrack = DinoRedWin;
						break;
				}
				_activeTrack = value;
			}
		}

		public AudioClip MainMenuTrack { get; set; }
		public AudioClip DuelTrack { get; set; }
		public AudioClip DinoRedWin { get; set; }
		public AudioClip DinoBlueWin { get; set; }
		private AudioClip currentTrack { get; set; }

		public static MusicManager Instance	{ get; set; }

		void Awake()
		{
			if(!Instance && Instance != this)
			{
				Destroy(gameObject);
				return;
			}

			Debug.Log("Got here.");
			Instance = this;
			DontDestroyOnLoad(gameObject);
			audioSource = GetComponent<AudioSource>();
			configureAudioSource();
		}

		void Start()
		{
			switch(Application.loadedLevelName)
			{
				case "MainMenu":
					ActiveTrack = Track.MainMenu;
					fadeIn();
					break;
				case "Scene1":
					ActiveTrack = Track.Duel;
					audioSource.volume = 1;
					break;
			}
			audioSource.clip = currentTrack;
			audioSource.Play();

		}

		private void configureAudioSource()
		{
			audioSource.loop = true;
		}

		private void fadeIn()
		{
			Level = 0;
			StartCoroutine(fade(1, 2, 0.5F));
		}

		private void fadeOut()
		{
			Level = 1;
			StartCoroutine(fade(0, 2, 0));
		}

		IEnumerator	fade(float value, float time, float waitTime)
		{
			for(float t = 0; t < waitTime; t+= Time.deltaTime)
				yield return null;

			for(float v = 0; v < value; v+= Time.deltaTime / time)
			{
				Level = v;
				yield return null;
			}
		}

	}
}