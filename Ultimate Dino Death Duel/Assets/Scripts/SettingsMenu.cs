using UnityEngine;
using UnityEngine.UI;
using Vexe.Runtime.Types;
using System.Collections;

namespace DinoDuel
{
	public class SettingsMenu : Menu
	{
		#region Frontend
		#region Levels
		#region Toggles
		public Toggle mstrToggle;
		public Toggle sfxToggle;
		public Toggle voToggle;
		public Toggle musToggle;

		public bool Master_Toggle
		{
			get { return mstrToggle.isOn; }
			set { mstrToggle.isOn = value; }
		}

		public bool SFX_Toggle
		{
			get { return sfxToggle.isOn; }
			set { sfxToggle.isOn = value; }
		}

		public bool VO_Toggle
		{
			get { return voToggle.isOn; }
			set { voToggle.isOn = value; }
		}

		public bool MUS_Toggle
		{
			get { return musToggle.isOn; }
			set { musToggle.isOn = value; }
		}
		#endregion

		#region Sliders
		public Slider mstrSlider;
		public Slider sfxSlider;
		public Slider voSlider;
		public Slider musSlider;

		public float Master_Level
		{
			get { return mstrSlider.value; }
			set { mstrSlider.value = value; }
		}

		public float SFX_Level
		{
			get { return sfxSlider.value; }
			set { sfxSlider.value = value; }
		}

		public float VO_Level
		{
			get { return voSlider.value; }
			set { voSlider.value = value; }
		}

		public float MUS_Level
		{
			get { return musSlider.value; }
			set { musSlider.value = value; }
		}
		#endregion
		#endregion
		#region Gameplay
		public Toggle pushToggle;
		public Toggle damageToggle;

		public bool Push_Toggle
		{
			get { return pushToggle.isOn; }
			set { pushToggle.isOn = value; }
		}

		public bool Damage_Toggle
		{
			get { return damageToggle.isOn; }
			set { damageToggle.isOn = value; }
		}
		#endregion
		#endregion

		#region Backend
		public UserSettings userSettings;
		#endregion

		void OnEnable()
		{
			Master_Toggle = userSettings.Master_Toggle;
			Master_Level = userSettings.Master_Level;
			SFX_Toggle = userSettings.SFX_Toggle;
			SFX_Level = userSettings.SFX_Level;
			VO_Toggle = userSettings.VO_Toggle;
			VO_Level = userSettings.VO_Level;
			MUS_Toggle = userSettings.Master_Toggle;
			MUS_Level = userSettings.Master_Level;
			Damage_Toggle = userSettings.Damage_Toggle;
			Push_Toggle = userSettings.Push_Toggle;
		}
	}

}
