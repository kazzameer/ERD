using UnityEngine;
using strange.extensions.command.impl;
using App.Models;

namespace App.Commands
{
	public class LoadDataCommand : Command {
		[Inject]
		public LevelGeneratorModel LevelGeneratorModel { get; set; }

		[Inject]
		public GameModel GameModel { get; set; }
		public override void Execute() {
			Debug.Log("LoadDataCommand");
			GameModel.LoadData();
			var textAsset = Resources.Load<TextAsset>("generator_config");
			if (textAsset != null) {
				LevelGeneratorModel.LoadFromJSON(textAsset.text);
			} else {
				throw new UnityException("Can't read generator config file");
			}
			
		}
	}
}
