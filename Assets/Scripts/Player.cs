using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace App
{
	public class Player : MonoBehaviour {
		[SerializeField] Transform CameraAnchor = null;
		public System.Action OnHit { get; set; }
		public System.Action OnCollect { get; set; }
		private Animator _animatorController = null;
		private bool _playerSteering = false;
		
		public Transform Anchor
		{
			get {
				return CameraAnchor;
			}
		}

		public bool IsSteering 
		{
			get 
			{
				return _playerSteering;
			}
		}
		public void ChangeTrack(int currentTrack)
		{
			_playerSteering = true;
			
			transform.DOMoveX(App.Models.Consts.CellOffset[currentTrack], 0.25f).OnComplete(()=>{
				_playerSteering = false;	
			});
		}

		void Awake()
		{
			_animatorController = gameObject.GetComponent<Animator>();
			if (_animatorController == null) {
				throw new UnityException("Player object have no reachable Animator component");
			}
			_animatorController.SetFloat("Forward", 1.0f);
		}

		void OnCollisionEnter(Collision collision)
		{
			foreach (ContactPoint contact in collision.contacts)
			{
				if (contact.otherCollider.gameObject.layer == 8)
				{
					OnHit();
				}

				if (contact.otherCollider.gameObject.layer == 9)
				{
					OnCollect();
				}
			}
		}

		public void Run()
		{
			_animatorController.SetFloat("Forward", 1.0f);
		}

		public void Stop() 
		{
			_animatorController.SetFloat("Forward", .0f);
		}
	}
}
