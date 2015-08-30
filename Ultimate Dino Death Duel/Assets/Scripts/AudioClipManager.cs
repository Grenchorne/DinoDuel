using UnityEngine;
using Vexe.Runtime.Types;
using System.Collections.Generic;

namespace DinoDuel
{
	[RequireComponent(typeof(AudioSource))]
	public class AudioClipManager : BetterBehaviour
	{
		public enum ClipType
		{
			Last,
			Shuffle,
			Queue,
		}

		private const int CLIPS_MAX = 20;

		private AudioSource audioSource;
		private int index;

		public AudioClip[] AudioClips;

		[Serialize]
		[Hide]
		private ClipType _clipType;
		[Show]
		public ClipType Clip_Type
		{
			get { return _clipType; }
			set { _clipType = value; }
		}

		[Show]
		public void playClip()
		{
			playClip(this.Clip_Type);
		}

		[Show]
		public void playClip(ClipType clipType)
		{
			switch(clipType)
			{
				case ClipType.Last:	break;
				case ClipType.Queue:
					index++;
					break;
				case ClipType.Shuffle:
					index = Random.Range(0, AudioClips.Length);
					break;
			}
			audioSource.clip = getClip(index);
			audioSource.Play();
		}

		public static void CreateInstance(ClipType clipType)
		{

		}

		void Start()
		{
			audioSource = GetComponent<AudioSource>();
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

			#if UNITY_EDITOR
				UnityEditor.Highlighter.Highlight("Inspector", audioClip.name);
			#endif
			this.index = index;
			return audioClip;
		}

		public override void Reset()
		{
			base.Reset();
			audioSource = GetComponent<AudioSource>();
		}
	}
}