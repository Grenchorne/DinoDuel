using UnityEngine;
using Vexe.Runtime.Types;
using System.Collections.Generic;

namespace DinoDuel
{
	[RequireComponent(typeof(AudioSource))]
	public class AudioClipManager : BetterBehaviour, iAudio
	{
		public enum PlaybackType
		{
			Last,
			Shuffle,
			Queue,
		}

		public enum ClipType
		{
			SFX,
			VO,
			MUS
		}

		public ClipType clipType;

		private const int CLIPS_MAX = 20;

		private AudioSource audioSource;
		private int index;

		public AudioClip[] AudioClips;

		[Serialize]
		[Hide]
		private PlaybackType _clipType;
		[Show]
		public PlaybackType Playback_Type
		{
			get { return _clipType; }
			set { _clipType = value; }
		}

		[Show]
		public void playClip()
		{
			playClip(this.Playback_Type);
		}

		[Show]
		public void playClip(PlaybackType playbackType)
		{
			switch(playbackType)
			{
				case PlaybackType.Last:	break;
				case PlaybackType.Queue:
					index++;
					break;
				case PlaybackType.Shuffle:
					index = Random.Range(0, AudioClips.Length);
					break;
			}
			audioSource.clip = getClip(index);
			audioSource.Play();
		}

		public void playClip(string clipName)
		{
			for(int i = 0; i < AudioClips.Length; i++)
			{
				AudioClip audioClip = AudioClips[i];
				if(audioClip.name == clipName)
				{
					index = i;
					audioSource.clip = getClip(i);
					audioSource.Play();
					return;
				}
			}
		}

		void Start()
		{
			audioSource = GetComponent<AudioSource>();
			updateLevel();
		}

		public void updateLevel()
		{
			UserSettings.BindLevel(audioSource, clipType);
		}

		private AudioClip getClip(int index)
		{
			AudioClip audioClip;

			int index_in = index;
			if(index >= AudioClips.Length)	index = 0;
			while((audioClip = AudioClips[index]) == null)
			{
				index++;
				if(index > CLIPS_MAX)
					index = 0;
				if(index == index_in)
				{
					Debug.LogError("No Clip found in " + this.ToString());
					return null;
				}
			}

			//#if UNITY_EDITOR
			//	UnityEditor.Highlighter.Highlight("Inspector", audioClip.name);
			//#endif
			this.index = index;
			return audioClip;
		}

		public override void Reset()
		{
			base.Reset();
			audioSource = GetComponent<AudioSource>();
			Playback_Type = PlaybackType.Shuffle;
		}
	}
}