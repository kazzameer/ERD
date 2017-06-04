using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Level
{
	public enum SegmentValue {
		Empty = 0,
		Wall,
		PlayerStart,
		Coin
	}
	public class LevelSegment 
	{
		private string _floorPrefab;
		private SegmentValue[,] _plan = new SegmentValue[3, 8];
		public int LastPattern = -1;
		public LevelSegment(string floorPrefab)
		{
			_floorPrefab = floorPrefab;
		}

		public string FloorPrefab
		{
			get {
				return _floorPrefab;
			}
		}

		public SegmentValue Get(int x, int y)
		{
			return _plan[x, y];
		}

		public void Set(int x, int y, SegmentValue cell)
		{
			_plan[x, y] = cell;
		}

		public void PlaceCoins()
		{
			for (int i = 0; i < _plan.GetLength(0); ++i)
			{
				for (int j = 0; j < _plan.GetLength(1); ++j)
				{
					if (_plan[i, j] == SegmentValue.Empty) {
						if (Random.Range(.0f, 10.0f) > 9.0f) {
							_plan[i, j] = SegmentValue.Coin;
						}
					}
				}
			}
		}
	}
}
