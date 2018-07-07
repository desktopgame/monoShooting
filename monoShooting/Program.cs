using Shoot.Core.Activity;
using System;
using XNA2D.Core;
using XNA2D.Core.Activity;

namespace Shoot {
#if WINDOWS || XBOX
	class Program : Application {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args) {
			Program program = new Program();
			program.Launch(args);
		}

		protected override ActivityMap CreateActivityMap(ExGame game) {
			ActivityMap map = new ActivityMap("Title", new TitleActivity());
			map["Game"] = new GameActivity();
			map["Pause"] = new PauseActivity();
			map["GameOver"] = new GameOverActivity();
			map["Controller"] = new ControllerActivity();
			return map;
		}
	}
#endif
}

