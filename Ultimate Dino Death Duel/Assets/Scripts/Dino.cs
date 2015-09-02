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

		#region Audio
		public AudioClipManager aud_explosion;
		public AudioClipManager aud_hit;
		public AudioClipManager aud_dmg_1;
		public AudioClipManager aud_dmg_2;
		public AudioClipManager aud_dmg_3;
		public AudioClipManager aud_death;
		#endregion

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

		public bool isAlive { get; private set; }
		#endregion

		#region Damage Packing
		public float damageToApply;
		private bool applyingDamage;
		public Dino enemy;
		private Weapon[] weapons;

		private void applyDamage()
		{
			enemy.damage(damageToApply);
			damageToApply = 0;
			applyingDamage = false;
		}

		public void damage(float damage)
		{
			if(damage >= 1 && damage < 10)
			{
				showEye(0);
				aud_dmg_1.playClip();
			}

			else if(damage >= 10 && damage < 15)
			{
				showEye(1);
				aud_dmg_2.playClip();
			}

			else
			{
				showEye(2);
				aud_dmg_3.playClip();
			}
			
			Health -= damage;
			damageEffect.Play();
			Vector2 pos = Camera.main.WorldToViewportPoint(enemyHead.position);
			DamageNumber.Spawn(color, damage, new Vector2(pos.x, pos.y));
		}
		#endregion

		public ParticleSystem damageEffect { get; set; }
		Transform enemyHead;
		Color color;

		public Text loseText;
		public Player player;
		public float speed = 100;

		int directionMod = 1;


		private bool usesInput;
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
			usesInput = true;
			Health = H_STARTING;
			isAlive = true;
			string inputPrefix = "";
			Transform head = null;
			switch(player)
			{
				case Player.Player1:
					inputPrefix = "P1_";
					directionMod = 1;
					enemyHead = GameObject.Find("Red_Head").transform;
					head = GameObject.Find("Blue_Head").transform;
					color = Color.red;
					break;
				case Player.Player2:
					inputPrefix = "P2_";
					directionMod = -1;
					enemyHead = GameObject.Find("Blue_Head").transform;
					head = GameObject.Find("Red_Head").transform;
					color = Color.blue;
					break;
				default: goto case Player.Player1;
			}

			if(head)	findEyes(head);
			weapons = GetComponentsInChildren<Weapon>();

			jawInput = inputPrefix + "Jaw";
			headInput = inputPrefix + "Head";
			pawLInput = inputPrefix + "Paw_L";
			pawRInput = inputPrefix + "Paw_R";
			legLInput = inputPrefix + "Leg_L";
			legRInput = inputPrefix + "Leg_R";

		}

		void pauseGame()
		{
			Timer timer = GameObject.FindObjectOfType<Timer>();
			//timer.section = Timer.Section.Post;
			timer.time = -100;
		}

		// Update is called once per frame
		void Update()
		{
			if(Input.GetAxisRaw("Pause") >= 1)
				pauseGame();
			if(usesInput)
			{
				movePart(Input.GetAxis(jawInput), JawMover);
				movePart(Input.GetAxis(headInput), HeadMover);
				movePart(Input.GetAxis(pawLInput), PawLMover);
				movePart(Input.GetAxis(pawRInput), PawRMover);
				movePart(Input.GetAxis(legLInput), LegLMover);
				movePart(Input.GetAxis(legRInput), LegRMover);
			}

			applyingDamage = false;
			foreach(Weapon d in weapons)
			{
				if(d.applyingDamage)
				{
					applyingDamage = true;
					break;
				}
			}
			if(!applyingDamage && damageToApply > 1)
				applyDamage();
			if(damageToApply < 1)
				damageToApply = 0;
		}

		private void movePart(float axisInput, HingeJoint2D joint)
		{
			JointMotor2D jm = new JointMotor2D();
			jm.maxMotorTorque = 100;

			if(axisInput > 0)	jm.motorSpeed = axisInput * speed * directionMod;
			else				jm.motorSpeed = -100 * directionMod;
			joint.motor = jm;
		}
		
		#region Death Handling
		
		private Rigidbody2D[] rigidBodies;
		[Show]
		public void die(DeathType deathType = DeathType.Damage)
		{
			#if UNITY_EDITOR
			if(!Application.isPlaying)
			{
				Debug.LogWarning("Dino death not available in editor");
				return;
			}
			#endif
			usesInput = false;
			rigidBodies = transform.GetComponentsInChildren<Rigidbody2D>();
			switch(deathType)
			{
				case DeathType.Damage:
					disassemble();
					//aud_death.playClip();
					break;
				case DeathType.Time:
					goto case DeathType.Meteor;
				case DeathType.Meteor:
					explode();
					break;
				case DeathType.Lava:
					//disassemble();
					break;
				default:
					goto case DeathType.Meteor;
			}
			isAlive = false;
			Camera.main.GetComponent<Timer>().section = Timer.Section.Post;
			_health = 0;
		}
		
		private void explode()
		{
			foreach(Rigidbody2D rb in rigidBodies)
			{
				releaseRigidBody(rb);
				noclipRigidBody(rb);
				projectRigidBody(rb);
			}
			
			aud_explosion.playClip();
			this.enabled = false;
		}

		private void disassemble()
		{
			foreach(HingeJoint2D joint in GetComponentsInChildren<Joint2D>())
				joint.useMotor = false;
			
			StartCoroutine(dropComponents());
		}

		private void releaseRigidBody(Rigidbody2D rb)
		{
			rb.fixedAngle = false;
			rb.gameObject.AddComponent<PartDestroyer>();

			HingeJoint2D hingeJoint = rb.GetComponent<HingeJoint2D>();
			if(hingeJoint)
				GameObject.Destroy(hingeJoint);

			rb.transform.parent = null;
		}

		private void noclipRigidBody(Rigidbody2D rb)
		{
			BoxCollider2D boxCollider2D = rb.GetComponent<BoxCollider2D>();
			if(boxCollider2D)
				GameObject.Destroy(boxCollider2D);
		}

		private void projectRigidBody(Rigidbody2D rb)
		{
			float randX = Random.Range(200, 1000);
			float randY = Random.Range(200, 1000);
			rb.AddForce(new Vector2(randX, randY));
		}

		public SpriteRenderer skeletonRenderer;
		public void showSkeleton()
		{
			if(skeletonRenderer)
			{
				foreach(SpriteRenderer sp in GetComponentsInChildren<SpriteRenderer>())
					sp.enabled = false;
				skeletonRenderer.enabled = true;
			}
		}

		IEnumerator dropComponents()
		{
			Rigidbody2D[] _rigidBodies = rigidBodies;
			foreach(Rigidbody2D rb in _rigidBodies)
			{
				releaseRigidBody(rb);
				noclipRigidBody(rb);
				yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
			}
		}
		#endregion

		#region Eye Damage
		SpriteRenderer[] eyes;

		private void findEyes(Transform beholderOftheEyes)
		{
			eyes = new SpriteRenderer[]
			{
				beholderOftheEyes.FindChild("Eyes_DMG1").GetComponent<SpriteRenderer>(),
				beholderOftheEyes.FindChild("Eyes_DMG2").GetComponent<SpriteRenderer>(),
				beholderOftheEyes.FindChild("Eyes_DMG3").GetComponent<SpriteRenderer>()
			};
		}

		void showEye(int index)
		{
			hideEyes();
			eyes[index].enabled = true;
			StartCoroutine(resetEyes());
		}

		void hideEyes()
		{
			foreach(SpriteRenderer eye in eyes)
				eye.enabled = false;
		}

		IEnumerator resetEyes()
		{
			yield return new WaitForSeconds(.5f);
			hideEyes();
		}
		#endregion
	}
}