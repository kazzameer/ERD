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
		public LevelSegment(string floorPrefab)
		{
			_floorPrefab = floorPrefab;
		}

		public int FreeCellsInRow(int rowIndex) {
			SegmentValue[] row = {_plan[0, rowIndex], _plan[1, rowIndex], _plan[2, rowIndex]};
			int freeCells = 0;
			for (int i = 0; i < row.Length; ++i) {
				if (row[i] == SegmentValue.Empty)
				{
					freeCells++;	
				}
			}
			return freeCells;
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
