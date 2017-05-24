using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace App.Views
{
	public class LevelHUDView : View 
	{
		public Signal OnLeft = new Signal();
		public Signal OnRight = new Signal();

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
