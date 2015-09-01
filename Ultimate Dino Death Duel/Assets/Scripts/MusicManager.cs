using UnityEngine;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;
using UnityEngine.Audio;

namespace DinoDuel
{	
	[RequireComponent(typeof(AudioSource))]
	public class MusicManager : MonoBehaviour
	{
		[SerializeField]
		static bool spawned = false;
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

		[SerializeField][HideInInspector]
		private Track _activeTrack;
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
				audioSource.clip = currentTrack;
				audioSource.Play();
			}
		}

		void OnDestroy()
		{
			Debug.Log("Destroying " + name);
		}

		public AudioClip MainMenuTrack;
		public AudioClip DuelTrack;
		public AudioClip DinoRedWin;
		public AudioClip DinoBlueWin;
		private AudioClip currentTrack;

		public MusicManager instance;

		[SerializeField]
		[HideInInspector]
		int currentLevel;
		void OnLevelWasLoaded(int level)
		{
			if(level == currentLevel)
				return;
			switch(Application.loadedLevelName)
			{
				case "MainMenu":
					Debug.Log("Main menu loaded");
					ActiveTrack = Track.MainMenu;
					fadeIn();
					break;
				case "Scene1":
					Debug.Log("Scene1 loaded");
					ActiveTrack = Track.Duel;
					audioSource.volume = 1;
					break;
			}
			currentLevel = Application.loadedLevel;
		}

		void Awake()
		{
			//if(spawned)
			//{
			//	Debug.Log("Spawned");
			//	if(instance)
			//	{
			//		Debug.Log("Instance exists");
			//		if(instance == this)
			//		{
			//			Debug.Log("This is instance");
			//		}
			//		else
			//		{
			//			Debug.Log("This is not instance");
			//		}
			//	}
			//	else
			//	{
			//		Debug.Log("Instance does not exist");
			//	}
			//}
			//else
			//{
			//	Debug.Log("Not spawned");
			//}
			Debug.Log("Awake");

			Debug.Log("Spawned = " + spawned);

			if(instance)
				Debug.Log("Instance exists: " + instance.name);
			else
				Debug.Log("Instance doesn't exist.");
			if(instance == this)
				Debug.Log("Instance = this");
			else
				Debug.Log("Instance != this");

			//if(instance && instance != this)
			//{
			//	Debug.Log("Instance exists");
			//	Destroy(gameObject);
			//	return;
			//}

			if(instance && instance != this)
			{
				Debug.Log("<color=red>Mark " + name + " for destroy</color>");
				Destroy(gameObject);
				return;
			}

			Debug.Log("<color=green>Assigning instance to " + name + "</color> ");
			instance = this;
			DontDestroyOnLoad(this);
			audioSource = GetComponent<AudioSource>();
			configureAudioSource();
			spawned = true;
		}

		void Start()
		{
			//Debug.Log("Start");
			//switch(Application.loadedLevelName)
			//{
			//	case "MainMenu":
			//		Debug.Log("Main menu loaded");
			//		ActiveTrack = Track.MainMenu;
			//		fadeIn();
			//		break;
			//	case "Scene1":
			//		Debug.Log("Scene1 loaded");
			//		ActiveTrack = Track.Duel;
			//		audioSource.volume = 1;
			//		break;
			//}
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