using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Models
{
	[System.Serializable]
	public class ConfigEntry {
		public string Prefab;
		public string Bundle;
	}

	[System.Serializable]
	public class LevelGeneratorConfig
	{
		public List<ConfigEntry> Obstacles;
		public List<ConfigEntry> Floors;
	}
	public class LevelGeneratorModel
	{
		private LevelGeneratorConfig _config = null;
		private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
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
					UnityEngine.Debug.LogWarningFormat("Loaded {0} generator floors", 
						_config.Floors.Count);

					foreach(var entry in _config.Obstacles) {
						LoadPrefab(entry);
					}

					foreach(var entry in _config.Floors) {
						LoadPrefab(entry);
					}
				}
			}
		}

		private void LoadPrefab(ConfigEntry entry)
		{
			Debug.LogFormat("Loading {0}", entry.Prefab);
			if (!_prefabs.ContainsKey(entry.Prefab))
			{
				var prefab = Resources.Load<GameObject>("Prefabs/" + entry.Prefab);
				if (prefab != null)
				{
					_prefabs[entry.Prefab] = prefab;
				} else {
					Debug.LogErrorFormat("Can't load a prefab for {0}", entry.Prefab);
				}
					
			}
		}
	}
}