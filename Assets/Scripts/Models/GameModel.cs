using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.signal.impl;

namespace App.Models
{
	public class GameModel 
	{
		private int _hiscore = 0;
		private int _score = 0;
		private int _lastScore = 0;

		public Signal<int> OnScoreChange = new Signal<int>();

		public int HiScore {
			get {
				return _hiscore;
			}
		}

		public void LoadData() {
			_hiscore = PlayerPrefs.GetInt("hi", 0);
		}

		private void SaveData() {
			PlayerPrefs.SetInt("hi", _hiscore);
		}

		public int LastScore {
			get {
				return _lastScore;
			}
		}

		public int Score {
			get {
				return _score;
			}
		}

		public void Reset() {
			if (_score > _hiscore) {
				_hiscore = _score;
			}
			_lastScore = _score;
			_score = 0;
			SaveData();
		}

		public void IncScore(int amount) {
			_score += amount;
			OnScoreChange.Dispatch(_score);
		}
	}
}
