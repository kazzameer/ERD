using UnityEngine;
using strange.extensions.command.impl;
namespace App.Commands
{
	public class StartGameCommand: Command {
		[Inject(Main.Container.World)]
		public Transform World { get; set; }

		public override void Execute() {
			var levelViewPrefab = Resources.Load<GameObject>("Views/LevelView");
			var instance = GameObject.Instantiate<GameObject>(levelViewPrefab);
			instance.transform.SetParent(World, false);
		}
	}
}
