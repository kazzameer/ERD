using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

using App.Signals;

namespace App.Views
{
	public class LevelViewMediator : Mediator
	{
		[Inject]
		public LevelView View { get; set; }

		[Inject]
		public MoveLeftSignal MoveLeft { get; set; }
		
		[Inject]
		public MoveRightSignal MoveRight { get; set; }

		[Inject]
		public App.Models.GameModel GameModel { get; set; }

		[Inject]
		public ShowMenuSignal ShowMenu { get; set; }
		public override void OnRegister()
        {
            base.OnRegister();
			View.OnHit.AddOnce(OnHit);
			View.OnCollect.AddListener(OnCollect);
			
			View.SpawnPlayer();
			View.AttachCamera(Camera.main);
			View.GenerateInitialSegment();
			
			MoveLeft.AddListener(OnMoveLeft);
			MoveRight.AddListener(OnMoveRight);
		}

		private void OnHit()
		{
			GameModel.Reset();
			ShowMenu.Dispatch();
		}

		private void OnCollect()
		{
			GameModel.IncScore(1);
		}

		private void OnMoveLeft()
		{
			View.MoveLeft();
		}

		private void OnMoveRight()
		{
			View.MoveRight();
		}

		public override void OnRemove()
		{
			MoveLeft.RemoveListener(OnMoveLeft);
			MoveRight.RemoveListener(OnMoveRight);
		}

	}
}
