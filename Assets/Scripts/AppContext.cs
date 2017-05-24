#pragma warning disable 0414

using UnityEngine;
using System.Collections;

using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using strange.extensions.context.api;
using strange.extensions.signal.api;
using strange.extensions.signal.impl;

using App.Models;
using App.Signals;
using App.Commands;

namespace App
{
    public class AppContext : MVCSContext {
		private Main _main = null;

        public AppContext(Main main): base(main, true)
        {
            _main = main;
        }

		protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        public override void Launch ()
        {
            base.Launch ();
            injectionBinder.GetInstance<InitiateSignal>().Dispatch();
        }

		protected override void mapBindings ()
        {
			injectionBinder.Bind<LevelGeneratorModel>().To<LevelGeneratorModel>().ToSingleton();
            commandBinder.Bind<InitiateSignal>().To<LoadDataCommand>();
		}
	}
}