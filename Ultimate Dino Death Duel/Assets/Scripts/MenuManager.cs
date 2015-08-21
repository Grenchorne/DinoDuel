using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DinoDuel
{
	public class MenuManager : MonoBehaviour
	{
		public Menu mainMenu;
		private Menu activeMenu;

		public void showMenu(Menu menu)
		{
			if(activeMenu)	activeMenu.gameObject.SetActive(false);
			activeMenu = menu;
			menu.gameObject.SetActive(true);
		}

		void Start()
		{
			showMenu(mainMenu);
		}
	}
}