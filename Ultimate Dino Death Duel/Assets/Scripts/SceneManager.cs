using UnityEngine;

namespace DinoDuel
{
	public class SceneManager : MonoBehaviour
	{
		public void quit()
		{
			Application.Quit();
		}

		public void changeScene(string name)
		{
			Application.LoadLevel(name);
		}
	}
}