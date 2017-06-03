using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace App.Views
{
	public class MainMenuView : View 
	{
		[SerializeField] Text HiScoresText = null;
		[SerializeField] Text LastScoreText = null;
		public Signal OnPlay = new Signal();
		public void OnPlayButton() {
			OnPlay.Dispatch();
		}

		public string LastScore {
			set {
				LastScoreText.text = value;
			}
		}

		public string HiScore {
			set {
				HiScoresText.text = value;
			}
		}
	}
}
