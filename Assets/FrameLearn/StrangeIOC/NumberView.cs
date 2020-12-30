using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace FrameLearn.StrangeIOC
{
	public class NumberView :View
	{
		public Text NumberText { get; set; }

		public void Init()
        {
			NumberText = GetComponent<Text>();
        }
		
	}
}