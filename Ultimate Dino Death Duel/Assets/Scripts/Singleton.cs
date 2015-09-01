using UnityEngine;
using Vexe.Runtime.Types;
using System.Collections;

namespace DinoDuel
{
	public class Singleton<T> : BetterBehaviour where T : BetterBehaviour
	{
		private static T _instance;
		private static object _lock = new object();

		public static T Instance
		{
			get
			{
				if(applicationIsQuitting)
				{
#if UNITY_EDITOR
					//Debug.LogWarning("[Singleton] Instance " + typeof(T) +
					//	" already destroyed on application quit." +
					//	"\nWon't create again; returning null.");
					//return null;
#endif
				}

				lock(_lock)
				{
					if(!_instance)
					{
						_instance = FindObjectOfType<T>();
						if(FindObjectsOfType<T>().Length > 1)
						{
#if UNITY_EDITOR
							//Debug.LogError("[Singleton] Something went really wrong. " + 
							//	"There should never be more than one singleton per type." + 
							//	"\n Reopening the scene might fix it.");
#endif
							return _instance;
						}
						if(!_instance)
						{
							_instance = new GameObject()
								.AddComponent<T>();
#if UNITY_EDITOR
							//Debug.Log("[Singleton] An instance of " +
							//	typeof(T) + " is needed in scene, so '" + 
							//	_instance.name + "' was created." + 
							//"\nDontDestroyOnLoad added on Awake.");	
#endif

						}
						else
						{
							//Debug.Log("[Singleton] Using instance already created: " + 
							//		_instance.name);
						}
					}
					return _instance;
				}
			}
		}

		protected virtual void Awake()
		{
			T instance = Instance;
			if(instance != this)
			{
				Debug.Log("Destroying " + name);
				Destroy(gameObject);
				return;
			}
			DontDestroyOnLoad(instance);
		}
		
		private static bool applicationIsQuitting = false;
		public void OnDestroy()
		{
			applicationIsQuitting = true;
		}

		public override void Reset()
		{
			base.Reset();
			name = typeof(T).ToString() + "_" +  Application.loadedLevelName;
		}
	}
}