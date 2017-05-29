using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using strange.extensions.mediation.impl;
using DG.Tweening;

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
		private int _currentTrack = 1;
		private GameObject _currentSegment = null;
		private bool _playerSteering = false;
		private int _segmentCounter = 0;
		private float _segmentProgress = .0f;
		private List<GameObject> _segments = new List<GameObject>();

		private bool _pendNewSegment = false;
		private int _lastSegment = 0;
		private bool _removedLastTile = false;


		private Dictionary<int, float> _cellOffset = new Dictionary<int, float> {
			{0, -1.0f},
			{1, .0f},
			{2, 1.0f}
		};
		public void GenerateInitialSegment()
		{
			BuildSegment(LevelGenerator.GenerateSegment());
			BuildSegment(LevelGenerator.GenerateSegment());
			BuildSegment(LevelGenerator.GenerateSegment());
		}
		
		public void MoveLeft()
		{
			if (_currentTrack > 0 && !_playerSteering)
			{
				_playerSteering = true;
				_currentTrack--;
				ChangeTrack();
			}
		}
		private void ChangeTrack()
		{
			_player.transform.DOMoveX(_cellOffset[_currentTrack], 0.35f).OnComplete(()=>{
				_playerSteering = false;
			});
		}
		public void MoveRight()
		{
			if (_currentTrack < LevelGenerator.MAX_COLUMNS - 1 && !_playerSteering)
			{
				_playerSteering = true;
				_currentTrack++;
				ChangeTrack();
			}
		}

		public void AttachCamera(Camera camera)
		{
			var controller = camera.GetComponent<UnityStandardAssets.Utility.SmoothFollow>();
			controller.SetTarget(_player.Anchor);
		}

	private void BuildSegment(LevelSegment segment)
		{
			GameObject segmentInstance = new GameObject();
			segmentInstance.transform.SetParent(this.transform, false);
			segmentInstance.transform.position = new Vector3(0, 0, _segmentCounter * 8.0f);
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
							inst.transform.localPosition = new Vector3(_cellOffset[i], 0, j);	
						} break;

						case SegmentValue.PlayerStart: {
//							_player.transform.position = new Vector3(_cellOffset[i], 0, j);
						} break;
					}
				}
			}
			
			_segmentCounter++;
			_segments.Add(segmentInstance);
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
		
		void LateUpdate()
		{
			if (_player != null)
			{
				var advance = _player.gameObject.transform.position.z;
				int segmentNumber = (int)(advance / 8.0f);

				if (segmentNumber != _lastSegment) {
					_lastSegment = segmentNumber;
					_pendNewSegment = false;
					_removedLastTile = false;
				}

				_segmentProgress = (advance - segmentNumber * 8.0f ) / 8.0f;

				if (_segmentProgress > 0.95f && !_pendNewSegment) {
					_pendNewSegment = true;
					BuildSegment(LevelGenerator.GenerateSegment());
				}

				if (_segmentProgress > 0.25 && !_removedLastTile && segmentNumber > 0) {
					_removedLastTile = true;
					PopSegment();
				}
			}
		}
		private void PopSegment()
		{
			Destroy(_segments[0].gameObject);
			_segments.RemoveAt(0);
		}
	}
}
