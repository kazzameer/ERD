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
using App.Level;
using App.Views;

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
            // models
			injectionBinder.Bind<LevelGeneratorModel>().To<LevelGeneratorModel>().ToSingleton();
            injectionBinder.Bind<LevelGenerator>().To<LevelGenerator>().ToSingleton();
            
            // views and mediators
            mediationBinder.Bind<LevelView>().To<LevelViewMediator>();
            mediationBinder.Bind<LevelHUDView>().To<LevelHUDMediator>();
            mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();

            // signals
            injectionBinder.Bind<MoveLeftSignal>().ToSingleton();
            injectionBinder.Bind<MoveRightSignal>().ToSingleton();

            // commands
            commandBinder.Bind<StartGameSignal>().To<StartGameCommand>();

            commandBinder.Bind<InitiateSignal>().InSequence().
            To<LoadDataCommand>().
            To<ShowMainMenuCommand>();

            // utility
            injectionBinder.Bind<Transform>().To(_main.World).ToName(Main.Container.World);
            injectionBinder.Bind<Transform>().To(_main.UIRoot).ToName(Main.Container.UI);
		}
	}
}