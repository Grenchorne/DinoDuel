using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace DinoDuel
{
	public class AudioManager : MonoBehaviour
	{
		public void playSource(AudioSource audioSource)
		{
			audioSource.Play();
		}
	}

	interface iAudio
	{
		void updateLevel();
	}
}