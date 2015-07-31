using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vexe.Runtime.Types;

namespace DinoDuel
{
	public class Dino : BetterBehaviour
	{
		
		public enum Player
		{
			Player1,
			Player2
		}

		#region Health
		private static readonly float H_MAX = 100;
		private static readonly float H_MIN = 0;
		private static readonly float H_STARTING = H_MAX;
		public Slider healthSlider;

		private float _health;
		[Show]
		public float Health
		{
			get { return _health; }
			set
			{
				bool isDead = value < H_MIN;
				if(value > H_MAX)		value = H_MAX;
				else if(value < H_MIN)	value = H_MIN;

				healthSlider.value = value;
				_health = value;
				if(isDead)	die();
			}
		}

		#endregion

		public Player player;
		public float speed = 100;

		int directionMod = 1;

		string jawInput;
		string headInput;
		string pawLInput;
		string pawRInput;
		string legLInput;
		string legRInput;

		public HingeJoint2D HeadMover { get; set; }
		public HingeJoint2D JawMover { get; set; }
		public HingeJoint2D LegLMover { get; set; }
		public HingeJoint2D LegRMover { get; set; }
		public HingeJoint2D PawRMover { get; set; }
		public HingeJoint2D PawLMover { get; set; }		

		// Use this for initialization
		void Start()
		{
			Health = H_STARTING;
			string inputPrefix = "";
			switch(player)
			{
				case Player.Player1:
					inputPrefix = "P1_";
					directionMod = 1;
					break;
				case Player.Player2:
					inputPrefix = "P2_";
					directionMod = -1;
					break;
				default: goto case Player.Player1;
			}

			jawInput = inputPrefix + "Jaw";
			headInput = inputPrefix + "Head";
			pawLInput = inputPrefix + "Paw_L";
			pawRInput = inputPrefix + "Paw_R";
			legLInput = inputPrefix + "Leg_L";
			legRInput = inputPrefix + "Leg_R";
		}

		// Update is called once per frame
		void Update()
		{
			movePart(Input.GetAxis(jawInput), JawMover);
			movePart(Input.GetAxis(headInput), HeadMover);
			movePart(Input.GetAxis(pawLInput), PawLMover);
			movePart(Input.GetAxis(pawRInput), PawRMover);
			movePart(Input.GetAxis(legLInput), LegLMover);
			movePart(Input.GetAxis(legRInput), LegRMover);
		}

		private void movePart(float axisInput, HingeJoint2D joint)
		{			
			JointMotor2D jm = new JointMotor2D();
			jm.maxMotorTorque = 100;

			if(axisInput > 0)	jm.motorSpeed = axisInput * speed * directionMod;
			else				jm.motorSpeed = -100 * directionMod;
			joint.motor = jm;
		}

		private void receiveDamage(float value)
		{
			//SFX
			//VFX
		}

		public void die()
		{
			gameObject.SetActive(false);
		}
	}
}
