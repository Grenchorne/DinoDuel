using UnityEngine;
using Vexe.Runtime.Types;

namespace DinoDuel
{
	public class DinoDebug : Singleton<DinoDebug>
	{
		const int i_MainMenu = 0;
		const int i_Scene1 = 1;

		const KeyCode KK_KillPlayer1		= KeyCode.Alpha1;
		const KeyCode KK_KillPlayer2		= KeyCode.Alpha2;
		const KeyCode KK_DecreaseTime		= KeyCode.Minus;
		const KeyCode KK_IncreaseTime		= KeyCode.Plus;

		bool b_mod1 = false;
		const KeyCode KK_Mod1				= KeyCode.LeftShift;
		const KeyCode KK_MainMenu			= KeyCode.Alpha0;
		const KeyCode KK_Level1				= KeyCode.Alpha1;

		const KeyCode KK_ToggleInvicible_P1 = KeyCode.Alpha1;
		const KeyCode KK_ToggleInvicible_P2 = KeyCode.Alpha2;

		bool b_mod2 = false;
		const KeyCode KK_Mod2				= KeyCode.LeftControl;
		const KeyCode KK_TogglePushable_P1  = KeyCode.Alpha1;
		const KeyCode KK_TogglePushable_P2  = KeyCode.Alpha2;

		const KeyCode KK_ReloadScene		= KeyCode.Space;

		protected DinoDebug() { }
		protected override void Awake()
		{
			base.Awake();
			#if !UNITY_EDITOR || UNITY_RELEASE
				Destroy(gameObject);
			#endif
		}

		private Dino player1;
		private Dino player2;
		private Timer timer;
		const float timeIncrement = 10;

		void Start()
		{
			rebind();
		}

		void rebind()
		{
			timer = GameObject.FindObjectOfType<Timer>();
			Dino[] dinos = GameObject.FindObjectsOfType<Dino>();
			foreach(Dino dino in dinos)
			{
				if(dino.player == Dino.Player.Player1)
					player1 = dino;
				else if(dino.player == Dino.Player.Player2)
					player2 = dino;
			}
		}

		void Update()
		{
			//Hold modification
			if(Input.GetKeyDown(KK_Mod1))			b_mod1 = true;
			if(Input.GetKeyDown(KK_Mod2))			b_mod2 = true;

			//Get input
			//Left shft
			if(b_mod1)
			{
				//Left shft + left ctrl
				if(b_mod2)
				{
					if(Input.GetKeyDown(KK_ReloadScene))
						Application.LoadLevel(currentLevel);
					if(Input.GetKeyDown(KK_MainMenu))
						Application.LoadLevel(i_MainMenu);
					else if(Input.GetKeyDown(KK_Level1))
						Application.LoadLevel(i_Scene1);
				}

				//Only left shft
				else
				{
					if(player1)
					{
						if(Input.GetKeyDown(KK_ToggleInvicible_P1))
							Debug.Log("toggle invincible p1");
					}
					if(player2)
					{
						if(Input.GetKeyDown(KK_ToggleInvicible_P2))
							Debug.Log("toggle invincible p2");
					}
				}
			}

			//Left ctrl
			else if(b_mod2)
			{
				if(player1)
				{
					if(Input.GetKeyDown(KK_TogglePushable_P1))
						Debug.Log("toggle pushable p1");
				}
				if(player2)
				{
					if(Input.GetKeyDown(KK_TogglePushable_P2))
						Debug.Log("toggle pushable p2");
				}
			}

			//No modification key pressed
			else
			{
				//Timer
				if(timer)
				{
					if(Input.GetKeyDown(KK_IncreaseTime))
						timer.time += timeIncrement;
					if(Input.GetKeyDown(KK_DecreaseTime))
						timer.time -= timeIncrement;
				}

				//Dino1
				if(player1)
				{
					if(Input.GetKeyDown(KK_KillPlayer1))
						player1.Health = -100;
				}
				//Dino2
				if(player2)
				{
					if(Input.GetKeyDown(KK_KillPlayer2))
						player2.Health = -100;
				}
			}
			
			//Release modification
			if(Input.GetKeyUp(KK_Mod1))				b_mod1 = false;
			if(Input.GetKeyUp(KK_Mod2))				b_mod2 = false;
		}

		int currentLevel = 0;
		void OnLevelWasLoaded(int level)
		{
			currentLevel = level;
			rebind();
		}
	}
}
