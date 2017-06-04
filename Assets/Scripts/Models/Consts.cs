using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Models
{
	public static class Consts
	{
		public static Dictionary<int, float> CellOffset = new Dictionary<int, float> {
			{0, -1.0f},
			{1, .0f},
			{2, 1.0f}
		};
	}
}