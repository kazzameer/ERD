using UnityEngine;
using strange.extensions.command.impl;
namespace App.Commands
{
	public class StartGameCommand: Command {
		[Inject(Main.Container.World)]
		public Transform World { get; set; }


		[Inject(Main.Container.UI)]
		public Transform UI { get; set; }
		public override void Execute() {
			Utils.ClearTransform(UI);
			Utils.ClearTransform(World);
			var levelViewPrefab = Resources.Load<GameObject>("Views/LevelView");
			var instance = GameObject.Instantiate<GameObject>(levelViewPrefab);
			instance.transform.SetParent(World, false);

			var hudPrefab = Resources.Load<GameObject>("Views/LevelHUD");
			var hudInstance = GameObject.Instantiate<GameObject>(hudPrefab);
			hudInstance.transform.SetParent(UI, false);
		}
	}
}
