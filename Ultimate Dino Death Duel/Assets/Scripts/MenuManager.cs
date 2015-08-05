using UnityEngine;
using System.Collections;

namespace DinoDuel
{
	public class MenuManager : MonoBehaviour
	{
		public Menu mainMenu;
		public Menu controlsMenu;

		public void showMenu(Menu menu)
		{
			Menu otherMenu = null;

			if(menu == mainMenu)
				otherMenu = controlsMenu;

			else if(menu == controlsMenu)
				otherMenu = mainMenu;

			otherMenu.gameObject.SetActive(false);
			menu.gameObject.SetActive(true);
		}

		void Start()
		{
			showMenu(mainMenu);
		}
	}
}