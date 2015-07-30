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

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			if(Input.GetKey(KeyCode.Q))
				Debug.Log("Move the head.");
			if(Input.GetKey(KeyCode.E))
				Debug.Log("Move the jaw.");
			if(Input.GetKey(KeyCode.W))
				Debug.Log("Move the left arm.");
			if(Input.GetKey(KeyCode.S))
				Debug.Log("Move the right arm.");
			if(Input.GetKey(KeyCode.A))
				Debug.Log("Move the left leg.");
			if(Input.GetKey(KeyCode.D))
				Debug.Log("Move the right leg.");
		}
	}
}
