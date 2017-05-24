using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
	public class Player : MonoBehaviour {
		private Animator _animatorController = null;
		void Awake()
		{
			_animatorController = gameObject.GetComponent<Animator>();
			_animatorController.SetFloat("Forward", 0.5f);
		}
	}

}
