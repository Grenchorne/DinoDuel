using UnityEngine;
using System.Collections;
using Vexe.Runtime.Types;

namespace DinoDuel
{
	public class DinoMover : BetterBehaviour
	{
		public HingeJoint2D HeadMover { get; set; }
		public HingeJoint2D JawMover { get; set; }
		public HingeJoint2D LegLMover { get; set; }
		public HingeJoint2D LegRMover { get; set; }
		public HingeJoint2D PawRMover { get; set; }
		public HingeJoint2D PawLMover { get; set; }

		public float speed = 100;

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			movePart(Input.GetAxis("Jaw"), JawMover);
			movePart(Input.GetAxis("Head"), HeadMover);
			movePart(Input.GetAxis("Paw_L"), PawLMover);
			movePart(Input.GetAxis("Paw_R"), PawRMover);
			movePart(Input.GetAxis("Leg_L"), LegLMover);
			movePart(Input.GetAxis("Leg_R"), LegRMover);
		}

		private void movePart(float axisInput, HingeJoint2D joint)
		{

			JointMotor2D jm = new JointMotor2D();
			jm.maxMotorTorque = 100;
			if(axisInput > 0)
				jm.motorSpeed = axisInput * speed;
			else
				jm.motorSpeed = -100;
			
			joint.motor = jm;

		}
	}
}
