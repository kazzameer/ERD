using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
	public class Player : MonoBehaviour {
		[SerializeField] Transform CameraAnchor = null;
		private Animator _animatorController = null;

		public Transform Anchor
		{
			get {
				return CameraAnchor;
			}
		}
		void Awake()
		{
			_animatorController = gameObject.GetComponent<Animator>();
			_animatorController.SetFloat("Forward", 0.0f);
		}
	}

}
