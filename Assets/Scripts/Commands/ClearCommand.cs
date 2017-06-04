using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;

namespace App.Commands
{
    public class ClearCommand : Command
    {
        [Inject(Main.Container.UI)]
        public Transform UI { get; set; }

        [Inject(Main.Container.World)]
        public Transform World { get; set; }

        public override void Execute()
        {
			Debug.Log("Clear command");
			
			foreach (Transform child in UI) {
                GameObject.Destroy(child.gameObject);
            }

            foreach (Transform child in World) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

}
