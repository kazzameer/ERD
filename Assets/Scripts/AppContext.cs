#pragma warning disable 0414

using UnityEngine;
using System.Collections;

using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using strange.extensions.context.api;
using strange.extensions.signal.api;
using strange.extensions.signal.impl;

namespace App
{
    public class AppContext : MVCSContext {
		private Main _main = null;

        public AppContext(Main main): base(main, true)
        {
            _main = main;
        }
	}
}