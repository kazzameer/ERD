using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;

namespace App
{
	public class Main : ContextView {
		[SerializeField] Transform Root;
		[SerializeField] Transform UI;
		void Awake()
		{
			//Random.InitState(128);
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			context = new AppContext(this);
			context.Start();
		}

		public Transform World {
			get {
				return Root;
			}
		}

		public Transform UIRoot {
			get {
				return UI;
			}
		}

		public enum Container {
			World = 0,
			UI
		}
	}
}
