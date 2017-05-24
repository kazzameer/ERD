using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

using App.Level;
using App.Models;

namespace App.Views
{
	public class LevelView : View {
		[SerializeField] GameObject PlayerPrefab = null;

		[Inject]
		public LevelGenerator LevelGenerator { get; set; }

		[Inject]
		public LevelGeneratorModel LevelGeneratorModel { get; set; }

		private Player _player = null;
		private GameObject _currentSegment = null;
		private Dictionary<int, float> _cellOffset = new Dictionary<int, float> {
			{0, -1.0f},
			{1, .0f},
			{2, 1.0f}
		};
		public void GenerateInitialSegment()
		{
			BuildSegment(LevelGenerator.GenerateSegment());
		}
		
		public void AttachCamera(Camera camera)
		{
			var controller = camera.GetComponent<UnityStandardAssets.Utility.SmoothFollow>();
			controller.SetTarget(_player.Anchor);
		}

		private GameObject BuildSegment(LevelSegment segment)
		{
			GameObject segmentInstance = new GameObject();
			segmentInstance.transform.SetParent(this.transform, false);
			var obstacles = LevelGeneratorModel.Obstacles;

			var floorInst = Instantiate<GameObject>(LevelGeneratorModel[segment.FloorPrefab]);
			floorInst.transform.SetParent(segmentInstance.transform, false);

			for (int j = 0; j < LevelGenerator.MAX_ROWS; ++j)
			{
				for (int i = 0; i < LevelGenerator.MAX_COLUMNS; ++i)
				{
					var cell = segment.Get(i, j);
					switch(cell)
					{
						case SegmentValue.Wall: {
							var obstacle = Utils.GetRandom(obstacles);
							var obstaclePrefab = LevelGeneratorModel[obstacle.Prefab];
							var inst = Instantiate<GameObject>(obstaclePrefab);
							inst.transform.SetParent(segmentInstance.transform, false);
							inst.transform.position = new Vector3(_cellOffset[i], 0, j);	
						} break;

						case SegmentValue.PlayerStart: {
							_player.transform.position = new Vector3(_cellOffset[i], 0, j);
						} break;
					}
				}
			}
			

			return segmentInstance;
		}

		public void SpawnPlayer()
		{
			if (_player == null)
			{
				var playerInstance = Instantiate<GameObject>(PlayerPrefab);
				playerInstance.transform.SetParent(this.transform, false);
				_player = playerInstance.GetComponent<Player>();
			}
			
		}
	}
}
