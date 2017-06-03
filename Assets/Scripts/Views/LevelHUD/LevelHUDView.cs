using UnityEngine;
using UnityEngine.UI;

using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace App.Views
{
	public class LevelHUDView : View 
	{
		[SerializeField] Text ScoreText = null;
		public Signal OnLeft = new Signal();
		public Signal OnRight = new Signal();

		public string Score {
			set {
				ScoreText.text = value;
			}
		}

		public void OnLeftClick()
		{
			OnLeft.Dispatch();
		}

		public void OnRightClick() 
		{
			OnRight.Dispatch();
		}
	}
}
