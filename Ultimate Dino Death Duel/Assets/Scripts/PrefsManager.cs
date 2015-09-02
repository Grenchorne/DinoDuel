using UnityEngine;
using Vexe.Runtime.Types;
using System.Collections.Generic;

namespace DinoDuel
{
	public class PrefsManager : BetterBehaviour
	{
		public List<AudioSource> sfx;
		public List<AudioSource> vo;
		public List<AudioSource> mus;
		
		void Start()
		{
			updateAudio();
			//UserSettings.UpdateSceneLevels();
		}

		public void updateAudio()
		{
			UserSettings.BindSFXLevel(sfx.ToArray());
			UserSettings.BindVOLevel(mus.ToArray());
			UserSettings.BindMUSLevel(mus.ToArray());
		}
	}

}
