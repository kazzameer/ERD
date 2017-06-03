using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace App.Views
{
	public class MainMenuMediator : Mediator
	{
		[Inject]
		public MainMenuView View { get; set; }

		[Inject]
		public App.Signals.StartGameSignal StartGameSignal { get; set; }

		[Inject]
		public App.Models.GameModel GameModel { get; set; }

		public override void OnRegister() {
			base.OnRegister();
			View.OnPlay.AddListener(OnPlay);
			View.HiScore = string.Format("Hi score {0}", GameModel.HiScore.ToString("D4"));
			View.LastScore = string.Format("Last score {0}", GameModel.LastScore.ToString("D4"));
		}

		private void OnPlay() {
			StartGameSignal.Dispatch();
		}

		public override void OnRemove() {
			base.OnRemove();
			View.OnPlay.RemoveListener(OnPlay);
		}
	}
}
