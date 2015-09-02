using UnityEngine;

namespace DinoDuel
{
	public class SceneManager : MonoBehaviour
	{
		public void quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

		public void changeScene(string name)
		{
			Application.LoadLevel(name);
		}
	}
}