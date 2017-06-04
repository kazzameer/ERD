using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

using App.Signals;

namespace App.Views
{
	public class LevelHUDMediator : Mediator 
	{
		[Inject]
		public LevelHUDView View { get; set; }

		[Inject]
		public MoveLeftSignal MoveLeft { get; set; }
		[Inject]
		public MoveRightSignal MoveRight { get; set; }
		[Inject]
		public App.Models.GameModel GameModel { get; set; }
		[Inject]
		public CountDownSignal CountdownComplete { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			View.OnLeft.AddListener(OnLeft);
			View.OnRight.AddListener(OnRight);
			View.OnCountdown.AddListener(OnCountdown);
			GameModel.OnScoreChange.AddListener(OnScoreChange);

			View.StartCountdown();
		}

		private void OnCountdown()
		{
			CountdownComplete.Dispatch();
		}

		private void OnScoreChange(int score)
		{
			View.Score = score.ToString("D4");
		}

		private void OnLeft()
		{
			MoveLeft.Dispatch();
		}

		private void OnRight()
		{
			MoveRight.Dispatch();
		}

		public override void OnRemove()
		{
			base.OnRemove();
			View.OnLeft.RemoveListener(OnLeft);
			View.OnRight.RemoveListener(OnRight);
			GameModel.OnScoreChange.RemoveListener(OnScoreChange);
		}
	}
}
