using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.Activity;
using XNA2D.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Shoot.Core.Activity {
	/// <summary>
	/// ポーズ画面.
	/// </summary>
	public class PauseActivity : ActivityAdapter {
		/// <summary>
		/// 背景としてゲーム画面を描画します.
		/// </summary>
		public GameActivity Background {
			private set; get;
		}


		public override void LoadContent(ExGame game) {
			FlyweightContents.GetInstance().Get<Texture2D>(game, "Image/pause");
		}

		public override void Update(ExGame game, GameTime time) {
			KeyboardState s = Keyboard.GetState();
			if(s.IsKeyDown(Keys.Escape)) {
				game.Goto("Title");
			} else if(s.IsKeyDown(Keys.Enter)) {
				DefaultTransitionInfo info = new DefaultTransitionInfo();
				info[GameActivity.ReturnFromPause] = true;
				game.Goto("Game", info);
			}
			base.Update(game, time);
		}

		public override void Draw(ExGame game, GameTime time, SpriteBatch batch) {
			Background.Draw(game, time, batch);
			Texture2D texture = FlyweightContents.GetInstance().Get<Texture2D>("Image/pause");
			DrawUtils.DrawImage(game.GraphicsDeviceManager, batch, texture);
		}

		public override void Show(ExGame game, ITransitionInfo prefs) {
			this.Background = (GameActivity)prefs[GameActivity.Background];
		}
	}
}
