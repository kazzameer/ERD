using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Models
{
	[System.Serializable]
	public class ObstacleEntry {
		public string PrefabName { get; private set; }
		public string BundleName { get; private set; }
	}

	[System.Serializable]
	public class LevelGeneratorConfig
	{
		public List<ObstacleEntry> Obstacles;
	}
	public class LevelGeneratorModel
	{
		private LevelGeneratorConfig _config = null;
		public void LoadFromJSON(string json)
		{
			try {
				_config = JsonUtility.FromJson<LevelGeneratorConfig>(json);
			} catch(UnityException e) {
				Debug.LogErrorFormat("An error occured while parsing LevelGeneratorConfig ({0})",
				e.Message);
			} finally {
				if (_config != null) {
					UnityEngine.Debug.LogWarningFormat("Loaded {0} generator entities", 
						_config.Obstacles.Count);
				}
			}
			
		}
	}
}