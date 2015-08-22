using UnityEngine;
using Vexe.Runtime.Types;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;

namespace DinoDuel
{	
	public class UserSettings : BetterScriptableObject
	{

		#region Min/Max Values
		private static readonly float VAL_MIN = 0;
		private static readonly float VAL_MAX = 1;
		#endregion

		#region IO
		private static string ROOT;
		private static readonly string DIR = "/Settings/";
		private static readonly string FILENAME = "UDDD_UserPrefs";
		private static readonly string EXT = ".txt";
		#endregion

		#region Strings
		private static readonly string _sep = ":";
		#region English
		private static readonly string en_mstr_tog = "Master_Toggle";
		private static readonly string en_mstr_val = "Master_Level";
		private static readonly string en_vo_tog = "VO_Toggle";
		private static readonly string en_vo_val = "VO_Level";
		private static readonly string en_sfx_tog = "SFX_Toggle";
		private static readonly string en_sfx_val = "SFX_Level";
		private static readonly string en_mus_tog = "MUS_Toggle";
		private static readonly string en_mus_val = "MUS_Level";
		#endregion
		#endregion

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

		void findRoot()
		{
			ROOT = Application.dataPath;
		}

		#region Read/Write
		public void ioRead()
		{
			findRoot();
			string file = ROOT + DIR + FILENAME + EXT;
			if(!File.Exists(file))
				initializePrefs(file);

			using(StreamReader prefs = new StreamReader(file))
			{
				string line;
				while((line = prefs.ReadLine()) != null)
				{
					foreach(PropertyInfo pInfo in GetType().GetProperties())
					{
						if(line.Contains(pInfo.Name))
						{
							string sValue = line.Remove(0, line.IndexOf(_sep) + 1);
							Type pInfoType = pInfo.PropertyType;

							object oValue = null;
							switch(pInfoType.Name.ToLower())
							{
								case "string":
									oValue = sValue;
									break;
								case "int32":
									break;
								case "single":
									oValue = Convert.ToSingle(sValue);
									break;
								case "float":
									Debug.Log("Handle float");
									break;
								case "boolean":
									bool bValue = Convert.ToBoolean(sValue);
									oValue = bValue;
									break;
								default:
									Debug.Log("Unhandled type: " + pInfoType);
									break;
							}
							if(oValue == null)	continue;
							pInfo.SetValue(this, oValue, null);
						}
					}
				}
				prefs.Close();
			}				

			#if UNITY_EDITOR
				UnityEditor.AssetDatabase.SaveAssets();
				UnityEditor.AssetDatabase.Refresh();
			#endif
		}

		public void ioWrite()
		{
			findRoot();
			string file = ROOT + DIR + FILENAME + EXT;
			string[] lines = File.ReadAllLines(file);
			using(StreamWriter prefs = new StreamWriter(file))
			{
				foreach(string line in lines)
				{
					foreach(PropertyInfo pInfo in GetType().GetProperties())
					{
						if(line.Contains(pInfo.Name))
						{
							string value = pInfo.GetValue(this, null).ToString();
							prefs.WriteLine(pInfo.Name + _sep + value);
						}
					}
				}
			}
			#if UNITY_EDITOR
				UnityEditor.AssetDatabase.SaveAssets();
				UnityEditor.AssetDatabase.Refresh();
			#endif
		}

		private void initializePrefs(string file)
		{
			string subdir = ROOT + DIR;
			Directory.CreateDirectory(subdir);

			using (StreamWriter prefs = new StreamWriter(file))
			{
				prefs.WriteLine(en_mstr_tog + _sep + "True");
				prefs.WriteLine(en_vo_tog + _sep + "True");
				prefs.WriteLine(en_sfx_tog + _sep + "True");
				prefs.WriteLine(en_mus_tog + _sep + "True");
				prefs.WriteLine(en_mstr_val + _sep + VAL_MAX);
				prefs.WriteLine(en_vo_val + _sep + VAL_MAX);
				prefs.WriteLine(en_sfx_val + _sep + VAL_MAX);
				prefs.WriteLine(en_mus_val + _sep + VAL_MAX);
				prefs.Close();
			}

			#if UNITY_EDITOR
				UnityEditor.AssetDatabase.SaveAssets();
				UnityEditor.AssetDatabase.Refresh();
			#endif
		}
		#endregion
	}
}