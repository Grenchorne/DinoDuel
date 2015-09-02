using UnityEngine;
using UnityEditor;
using Vexe.Runtime.Types;
using System.Collections.Generic;
using DinoDuel;

namespace DinoDuel_Editor
{
	public class UserSettings_Editor : Editor
	{
		static readonly string DIR = "Assets/Resources/Prefs/";
		static readonly string TITLE = "UserSettingsAsset";
		static readonly string EXT = ".asset";
		static readonly string PATH_FULL = DIR + TITLE + EXT;

		[MenuItem("DinoDuel/Create User Settings")]
		public static void ForceCreate()
		{
			string[] assets = AssetDatabase.FindAssets(TITLE);
			bool exists = false;
			UserSettings userSettings = null;
			string path = null;

			foreach(string s in assets)
			{
				if(s == null)	exists = false;
				else
				{
					exists = true;
					path = s;
					break;
				}
				
			}

			if(exists)
			{
				userSettings =
					(UserSettings)AssetDatabase
						.LoadAssetAtPath(
							AssetDatabase.GUIDToAssetPath(path), typeof(UserSettings));
			}

			else
			{
				userSettings = BetterScriptableObject.CreateInstance<UserSettings>();
				AssetDatabase.CreateAsset(userSettings, PATH_FULL);
				AssetDatabase.SaveAssets();
			}
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = userSettings;
		}
	}
}