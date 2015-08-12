using UnityEngine;

namespace DinoDuel
{
	public class Menu : MonoBehaviour
	{
		public Animator animator;
		private bool isShown;
			
		public void show()
		{
			if(!isShown)
			{
				animator.SetTrigger("Show");
				isShown = true;
			}
		}

		public void hide()
		{
			if(isShown)
			{
				animator.SetTrigger("Hide");
				isShown = false;
			}
		}
	}
}