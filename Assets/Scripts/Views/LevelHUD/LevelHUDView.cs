using UnityEngine;
using UnityEngine.UI;

using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace App.Views
{
	public class LevelHUDView : View 
	{
		[SerializeField] Text ScoreText = null;
		[SerializeField] Text CountdownText = null;

		public Signal OnLeft = new Signal();
		public Signal OnRight = new Signal();
		public Signal OnCountdown = new Signal();

		private float _timer = 4.0f;
		private bool _counting = false;

		public string Score {
			set {
				ScoreText.text = value;
			}
		}

		public void StartCountdown()
		{
			_counting = true;
		}

		void LateUpdate()
		{
			if (_counting) {
				_timer -= Time.deltaTime;
				CountdownText.text = ((int)_timer).ToString();
				if (_timer <= 1) {
					CountdownText.gameObject.SetActive(false);
					_counting = false;
					OnCountdown.Dispatch();
				}
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
