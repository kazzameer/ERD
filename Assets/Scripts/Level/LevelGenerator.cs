using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Models;
using System.Linq;

namespace App.Level
{
	public class GeneratorPattern
	{
		public int[,] data;
		public int[] compatible;
		public int id;

		//public string floorGroup;
		public int Height {
			get {
				return data.GetLength(0);
			}
		}

		public bool Compatible(int compatibeTo)
		{
			for(int i = 0; i < compatible.Length; ++i) {
				if ((compatible[i] == compatibeTo) ||
					(compatible[i] == -1)) return true;
			}
			return false;
		}
	}

	public class SegmentPattern {
		public static List<GeneratorPattern> Patterns = new List<GeneratorPattern> {
			new GeneratorPattern {
				data = new int[,] {
					{0, 0, 0}
				},
				compatible = new int[] {-1},
				id = 0
			},
			new GeneratorPattern {
				data = new int[,] {
					{0, 0, 0},
					{0, 0, 0},
					{0, 0, 0}
				},
				compatible = new int[] {-1},
				id = 1
			},
			new GeneratorPattern {
				data = new int[,] {
					{1, 1, 0}
				},
				compatible = new int[] {1, 5},
				id = 2
			},
			new GeneratorPattern {
				data = new int[,] {
					{0, 1, 1}
				},
				compatible = new int[] {1, 4},
				id = 3
			},
			new GeneratorPattern {
				data = new int[,] {
					{0, 0, 1},
					{0, 0, 1},
					{0, 0, 1},
					{0, 0, 1},
					{0, 0, 1},
					{0, 0, 1},
					{0, 0, 1},
					{0, 0, 1},
				},
				compatible = new int[] {1, 3},
				id = 4
			},
			new GeneratorPattern {
				data = new int[,] {
					{1, 0, 0},
					{1, 0, 0},
					{1, 0, 0},
					{1, 0, 0},
					{1, 0, 0},
					{1, 0, 0},
					{1, 0, 0},
					{1, 0, 0},
				},
				compatible = new int[] {1, 2},
				id = 5
			}
		};
		public static GeneratorPattern PickRandomWithMaxHeight(int maxHeight) {
			List<GeneratorPattern> candidates = new List<GeneratorPattern>();
			foreach(var pattern in Patterns) {
				if (pattern.data.GetLength(0) <= maxHeight) {
					candidates.Add(pattern);
				}
			}
			return candidates.Count > 0 ? Utils.GetRandom(candidates) : null;
		}

		public static GeneratorPattern PickCompatibleAndHeight(int maxHeight, int compatibleToId) {
			Debug.LogFormat("pick compatible to {0}, max height = {1}", compatibleToId, maxHeight);
			List<GeneratorPattern> candidates = new List<GeneratorPattern>();
			foreach(var pattern in Patterns) {
				if (pattern.data.GetLength(0) <= maxHeight && pattern.Compatible(compatibleToId)) {
					Debug.LogFormat("add candidate {0}", pattern.id);
					candidates.Add(pattern);
				}
			}
			return candidates.Count > 0 ? Utils.GetRandom(candidates) : null;
		}
	}

	public class LevelGenerator {
		public const int MAX_ROWS = 8;
		public const int MAX_COLUMNS = 3;

		[Inject]
		public LevelGeneratorModel LevelGeneratorModel { get; set; }
		
		public LevelSegment GeneratePattern(LevelSegment previousSegment = null)
		{
			var obstacles = LevelGeneratorModel.Obstacles;
			var floors = LevelGeneratorModel.Floors;
			var floor = Utils.GetRandom(floors);
			LevelSegment segment = new LevelSegment(floor.Prefab);
			int heightBudget = 8;
			
			GeneratorPattern pattern = null;
			if (previousSegment == null) {
				// need to start from double clear row for player start
				pattern = SegmentPattern.Patterns[1];
			} else {
				pattern = SegmentPattern.PickCompatibleAndHeight(heightBudget, previousSegment.LastPattern);
			}
			 
			while( (heightBudget > 0) && (pattern != null))
			{
				var offset = 8 - heightBudget;
				for (int i = 0; i < pattern.data.GetLength(1); ++i) {
					for (int j = 0; j < pattern.data.GetLength(0); ++j) {
						segment.Set(i, offset + j, (SegmentValue)pattern.data[j, i]);
					}
				}
				segment.LastPattern = pattern.id;
				heightBudget -= pattern.Height;
				pattern = SegmentPattern.PickCompatibleAndHeight(heightBudget, pattern.id);
				if (pattern != null)
				{
					Debug.LogFormat("pattern {0} was chosen", pattern.id);
				}
			}
			
			
			return segment;
		}
	}
}
