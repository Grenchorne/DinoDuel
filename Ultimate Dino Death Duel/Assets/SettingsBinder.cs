using UnityEngine;
using System.Collections.Generic;

namespace DinoDuel
{
	public class SettingsBinder : MonoBehaviour
	{
		public PrefsManager prefsManager;
		void OnDisable()
		{
			prefsManager.updateAudio();
		}
	}
}