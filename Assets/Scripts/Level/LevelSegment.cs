using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Level
{
	public enum SegmentValue {
		Empty = 0,
		Wall,
		PlayerStart
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
	}
}
