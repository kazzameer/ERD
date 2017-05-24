using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace App.Views
{
	public class LevelViewMediator : Mediator
	{
		[Inject]
		public LevelView View { get; set; }
		public override void OnRegister()
        {
            base.OnRegister();
			View.GenerateInitialSegment();
			View.SpawnPlayer();
		}

		public override void OnRemove()
		{

		}

	}
}
