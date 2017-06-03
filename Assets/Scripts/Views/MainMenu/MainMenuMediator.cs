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

		public override void OnRegister() {
			base.OnRegister();
			View.OnPlay.AddListener(OnPlay);
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
