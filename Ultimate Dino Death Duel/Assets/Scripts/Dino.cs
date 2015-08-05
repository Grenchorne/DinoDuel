using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Vexe.Runtime.Types;

namespace DinoDuel
{
	public class Dino : BetterBehaviour
	{
		public enum Player
		{
			None = 0,
			Player1 = 2,
			Player2 = 4
		}

		public enum DeathType
		{
			Damage,
			Time,
			Meteor,
			Lava
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

		#region Damage Packing
		public float damageToApply { get; set; }
		private bool applyingDamage;
		public Dino enemy;
		private Damager[] damagers;

		private void applyDamage()
		{
			if(damageToApply >= 1 && damageToApply < 4)
			{
				Debug.Log("small hit");
			}

			else if(damageToApply >= 4 && damageToApply < 8)
			{
				Debug.Log("med hit");
			}

			else
			{
				Debug.Log("big hit");
			}

			enemy.Health -= damageToApply;
			damageToApply = 0;
			applyingDamage = false;
		}
		#endregion


		public Text loseText;
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

			damagers = GetComponentsInChildren<Damager>();

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

			applyingDamage = false;
			foreach(Damager d in damagers)
			{
				if(d.applyingDamage)
				{
					applyingDamage = true;
					break;
				}
			}

			if(!applyingDamage && damageToApply > 1)
			{
				applyDamage();
			}	
		}

		private void movePart(float axisInput, HingeJoint2D joint)
		{
			JointMotor2D jm = new JointMotor2D();
			jm.maxMotorTorque = 100;

			if(axisInput > 0)	jm.motorSpeed = axisInput * speed * directionMod;
			else				jm.motorSpeed = -100 * directionMod;
			joint.motor = jm;
		}

		public void die(DeathType deathType = DeathType.Damage)
		{
			switch(deathType)
			{
				case DeathType.Damage:
					explode();
					break;
				case DeathType.Time:
					goto case DeathType.Damage;
				case DeathType.Meteor:
					goto case DeathType.Damage;
				case DeathType.Lava:
					goto case DeathType.Damage;
				default: goto case DeathType.Damage;
			}
			this.enabled = false;
			Camera main = Camera.main;
			main.GetComponent<Cam_Normalizer>().frozen = true;
			main.GetComponent<Timer>().roundOver = true;

			if(!main.GetComponent<Timer>().roundOver)
				loseText.gameObject.SetActive(true);
		}

		[Show]
		private void explode()
		{
			List<Rigidbody2D> rigidBodies = new List<Rigidbody2D>();
			foreach(Rigidbody2D rb in transform.GetComponentsInChildren<Rigidbody2D>())
			{
				rb.fixedAngle = false;
				rb.gameObject.AddComponent<PartDestroyer>();
				rigidBodies.Add(rb);
				
				HingeJoint2D hingeJoint = rb.GetComponent<HingeJoint2D>();
				if(hingeJoint)		GameObject.Destroy(hingeJoint);
				
				BoxCollider2D boxCollider2D = rb.GetComponent<BoxCollider2D>();
				if(boxCollider2D)	GameObject.Destroy(boxCollider2D);
			}
			transform.DetachChildren();

			foreach(Rigidbody2D rb in rigidBodies)
			{
				float randX = Random.Range(200, 1000);
				float randY = Random.Range(200, 1000);
				rb.AddForce(new Vector2(randX, randY));
			}
			this.enabled = false;
		}
	}
}