using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils  {
		public static T GetRandom<T>(List<T> list) {
			return list[Random.Range(0, list.Count)];
		}
		public static void ClearTransform(Transform parent)
		{
			foreach(Transform child in parent) {
				GameObject.Destroy(child.gameObject);
			}
		}
}
