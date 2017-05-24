using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Models;

namespace App.Level
{
	public class LevelGenerator {
		[Inject]
		public LevelGeneratorModel LevelGeneratorModel { get; set; }
		
		private const int MAX_ROWS = 8;
		private const int MAX_COLUMNS = 3;

		private T GetRandom<T>(List<T> list) {
			return list[Random.Range(0, list.Count)];
		}

		public LevelSegment GenerateSegment(LevelSegment previousSegment = null)
		{
			var obstacles = LevelGeneratorModel.Obstacles;
			var floors = LevelGeneratorModel.Floors;

			var floor = GetRandom(floors);
			LevelSegment segment = new LevelSegment(floor.Prefab);
			
			int currentRow = 0;
			int currentColumn = 0;

			if (previousSegment == null) {
				// need to reserve {1, 0} point as player start
				segment.Set(1, 0, SegmentValue.PlayerStart);
				currentRow = 1;
			}

			for (; currentRow < MAX_ROWS; currentRow += 2)
			{
				for (currentColumn = 0; currentColumn < MAX_COLUMNS; currentColumn++)
				{
					if (Random.Range(.0f, 10.0f) <= 5.0f) {
						if (segment.FreeCellsInRow(currentRow) > 1) {
							segment.Set(currentColumn, currentRow, SegmentValue.Wall);
						}
					}
				}
			}
			
			return segment;
		}
	}
}
