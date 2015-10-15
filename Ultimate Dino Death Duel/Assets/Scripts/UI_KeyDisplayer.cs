using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace DinoDuel
{	
	public class UI_KeyDisplayer : MonoBehaviour
	{
		const string s_p1Prefix = "P1_";
		const string s_p2Prefix = "P2_";
		const string s_jawInput = "Jaw";
		const string s_headInput = "Head";
		const string s_pawLInput = "Paw_L";
		const string s_pawRInput = "Paw_R";
		const string s_legLInput = "Leg_L";
		const string s_legRInput = "Leg_R";

		#region Player1
		public GameObject i_q;
		public GameObject i_w;
		public GameObject i_e;
		public GameObject i_a;
		public GameObject i_s;
		public GameObject i_d;
		
		public GameObject k_q;
		public GameObject k_w;
		public GameObject k_e;
		public GameObject k_a;
		public GameObject k_s;
		public GameObject k_d;

		string p1Jaw;
		string p1Head;
		string p1pawL;
		string p1pawR;
		string p1legL;
		string p1legR;

		#endregion

		#region Player2
		public GameObject i_u;
		public GameObject i_i;
		public GameObject i_o;
		public GameObject i_j;
		public GameObject i_k;
		public GameObject i_l;

		public GameObject k_u;
		public GameObject k_i;
		public GameObject k_o;
		public GameObject k_j;
		public GameObject k_k;
		public GameObject k_l;

		string p2Jaw;
		string p2Head;
		string p2pawL;
		string p2pawR;
		string p2legL;
		string p2legR;
		#endregion

		void Start()
		{
			p1Jaw = s_p1Prefix + s_jawInput;
			p1Head = s_p1Prefix + s_headInput;
			p1pawL = s_p1Prefix + s_pawLInput;
			p1pawR = s_p1Prefix + s_pawRInput;
			p1legL = s_p1Prefix + s_legLInput;
			p1legR = s_p1Prefix + s_legRInput;

			p2Jaw = s_p2Prefix + s_jawInput;
			p2Head = s_p2Prefix + s_headInput;
			p2pawL = s_p2Prefix + s_pawLInput;
			p2pawR = s_p2Prefix + s_pawRInput;
			p2legL = s_p2Prefix + s_legLInput;
			p2legR = s_p2Prefix + s_legRInput;

		}

		void Update()
		{
			#region Player1
			if(Input.GetAxisRaw(p1Jaw) >= 1)
			{
				i_s.SetActive(true);
				k_s.SetActive(true);
			}
			else
			{
				i_s.SetActive(false);
				k_s.SetActive(false);
			}
			if(Input.GetAxisRaw(p1Head) >= 1)
			{
				i_w.SetActive(true);
				k_w.SetActive(true);
			}
			else
			{
				i_w.SetActive(false);
				k_w.SetActive(false);
			}
			if(Input.GetAxisRaw(p1pawL) >= 1)
			{
				i_q.SetActive(true);
				k_q.SetActive(true);
			}
			else
			{
				i_q.SetActive(false);
				k_q.SetActive(false);
			}
			if(Input.GetAxisRaw(p1pawR) >= 1)
			{
				i_e.SetActive(true);
				k_e.SetActive(true);
			}
			else
			{
				i_e.SetActive(false);
				k_e.SetActive(false);
			}
			if(Input.GetAxisRaw(p1legL) >= 1)
			{
				i_a.SetActive(true);
				k_a.SetActive(true);
			}
			else
			{
				i_a.SetActive(false);
				k_a.SetActive(false);
			}
			if(Input.GetAxisRaw(p1legR) >= 1)
			{
				i_d.SetActive(true);
				k_d.SetActive(true);
			}
			else
			{
				i_d.SetActive(false);
				k_d.SetActive(false);
			}
			#endregion
			#region Player2
			if(Input.GetAxisRaw(p2Jaw) >= 1)
			{
				i_k.SetActive(true);
				k_k.SetActive(true);
			}
			else
			{
				i_k.SetActive(false);
				k_k.SetActive(false);
			}		 
			if(Input.GetAxisRaw(p2Head) >= 1)
			{
				i_i.SetActive(true);
				k_i.SetActive(true);
			}
			else
			{
				i_i.SetActive(false);
				k_i.SetActive(false);
			}
			if(Input.GetAxisRaw(p2pawL) >= 1)
			{
				i_u.SetActive(true);
				k_u.SetActive(true);
			}
			else
			{
				i_u.SetActive(false);
				k_u.SetActive(false);
			}		 
			if(Input.GetAxisRaw(p2pawR) >= 1)
			{
				i_o.SetActive(true);
				k_o.SetActive(true);
			}
			else
			{
				i_o.SetActive(false);
				k_o.SetActive(false);
			}		 
			if(Input.GetAxisRaw(p2legL) >= 1)
			{
				i_j.SetActive(true);
				k_j.SetActive(true);
			}
			else
			{
				i_j.SetActive(false);
				k_j.SetActive(false);
			}		 
			if(Input.GetAxisRaw(p2legR) >= 1)
			{
				i_l.SetActive(true);
				k_l.SetActive(true);
			}
			else
			{
				i_l.SetActive(false);
				k_l.SetActive(false);
			}
			#endregion
		}
	}
}