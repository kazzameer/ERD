using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;

namespace App
{
	public class Main : ContextView {
		[SerializeField] Transform Root;
		[SerializeField] Transform UI;
		[SerializeField] AudioClip CoinSound = null;
		private AudioSource _audioSource = null;

		private static Main __instance = null;

		public static Main Instance {
			get {
				return __instance;
			}
		}

		void Awake()
		{
			__instance = this;
			//Random.InitState(128);
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			context = new AppContext(this);
			context.Start();
			_audioSource = gameObject.GetComponent<AudioSource>();

		}

		public void PlayCoinCollectSound() {
			_audioSource.PlayOneShot(CoinSound);
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
