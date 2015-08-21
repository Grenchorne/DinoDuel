using UnityEngine;
using Vexe.Runtime.Types;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DinoDuel
{	
	public class UserSettings : BetterScriptableObject
	{

		private static readonly float VAL_MIN = 0;
		private static readonly float VAL_MAX = 1;

		private static readonly string ROOT;
		private static readonly string PATH = " ";
		private static readonly string FILENAME = "DD_UserPrefs";
		private static readonly string EXT = ".txt";	

		#region Settings
		#region Toggles
		[Serialize]
		[Hide]
		private bool _Master_Toggle;
		[Show]
		public bool Master_Toggle
		{
			get { return _Master_Toggle; }

			set
			{
				if(value == _Master_Toggle)	return;
				_Master_Toggle = value;
				VO_Toggle  = value;
				SFX_Toggle  = value;
				MUS_Toggle  = value;
			}
		}

		[Serialize]
		[Hide]
		private bool _VO_Toggle;
		[Show]
		public bool VO_Toggle
		{
			get { return _VO_Toggle; }

			set
			{
				if(value == _VO_Toggle)	return;
				_VO_Toggle = value;
			}
		}

		[Serialize]
		[Hide]
		private bool _SFX_Toggle;
		[Show]
		public bool SFX_Toggle
		{
			get { return _SFX_Toggle; }
			set
			{
				if(value == _SFX_Toggle)	return;
				_SFX_Toggle = value;
			}
		}

		[Serialize]
		[Hide]
		private bool _MUS_Toggle;
		[Show]
		public bool MUS_Toggle
		{
			get { return _MUS_Toggle; }
			set
			{
				if(value == _MUS_Toggle)		return;
				_MUS_Toggle = value;
			}
		}
		#endregion

		#region Levels
		[Serialize][Hide]
		private float  _Master_Level;
		[Show]
		public float Master_Level
		{
			get { return _Master_Level; }
			set
			{
				bindValue(ref value);
				if(value == _Master_Level)	return;
				_Master_Level = value;
			}
		}

		[Serialize]
		[Hide]
		private float  _VO_Level;
		[Show]
		public float  VO_Level
		{
			get { return _VO_Level; }
			set
			{
				bindValue(ref value);
				if(value == _VO_Level)		return;
				_VO_Level = value;
			}
		}

		[Serialize]
		[Hide]
		private float _SFX_Level;
		[Show]
		public float SFX_Level
		{
			get { return _SFX_Level; }
			set
			{
				bindValue(ref value);
				if(value == _SFX_Level)		return;
				_SFX_Level = value;
			}
		}

		[Serialize]
		[Hide]
		private float _MUS_Level;
		[Show]
		public float MUS_Level
		{
			get { return _MUS_Level; }
			set
			{
				bindValue(ref value);
				if(value == _MUS_Level)		return;
				_MUS_Level = value;
			}
		}
		#endregion
		#endregion

		private void bindValue(ref float value)
		{
			if(value <= VAL_MIN)	value = VAL_MIN;
			else if(value > VAL_MAX)value = VAL_MAX;
			decimal.Round((decimal)value, 3);
		}

		[Show]
		public void reset()
		{
			Master_Toggle = true;
			VO_Toggle = true;
			SFX_Toggle = true;
			MUS_Toggle = true;
			Master_Level = VAL_MAX;
			VO_Level = VAL_MAX;
			SFX_Level = VAL_MAX;
			MUS_Level = VAL_MAX;
		}

		#region Read/Write
		public void ioRead()
		{
			Debug.Log("Read");

		}

		public void ioWrite()
		{
			Debug.Log("Write");
		}
		#endregion
	}
}