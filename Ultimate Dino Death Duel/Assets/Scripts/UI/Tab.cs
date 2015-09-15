using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DinoDuel
{	
	[RequireComponent(typeof(Image))]
	public class Tab : Button
	{
		public Sprite up;
		public Sprite down;

		public float glue;

		public override void OnSubmit(BaseEventData eventData)
		{
			base.OnSubmit(eventData);
			image.sprite = down;
		}
	}
}