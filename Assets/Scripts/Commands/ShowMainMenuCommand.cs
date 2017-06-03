using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;

namespace App.Commands
{
	public class ShowMainMenuCommand : Command
	{
		[Inject(Main.Container.UI)]
		public Transform UI { get; set; }
		
		public override void Execute() {
			var hudPrefab = Resources.Load<GameObject>("Views/MainMenu");
			var hudInstance = GameObject.Instantiate<GameObject>(hudPrefab);
			hudInstance.transform.SetParent(UI, false);
		}
	}
}
