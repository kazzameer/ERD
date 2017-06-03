using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace App.Views
{
	public class MainMenuView : View 
	{
		public Signal OnPlay = new Signal();
		public void OnPlayButton() {
			OnPlay.Dispatch();
		}
	}
}
