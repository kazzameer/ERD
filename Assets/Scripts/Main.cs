using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;

namespace App
{
	public class Main : ContextView {
		void Awake()
		{
			Random.InitState(128);
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			context = new AppContext(this);
			context.Start();
		}		
	}
}
