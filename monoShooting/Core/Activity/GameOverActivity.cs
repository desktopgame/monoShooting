using Microsoft.Xna.Framework;
using XNA2D.Core.Activity;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.UI;
using XNA2D.Core.UI.Table;
using XNA2D.Core.Utils;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.UI.Label;

namespace Shoot.Core.Activity {
	/// <summary>
	/// ゲーム画面.
	/// </summary>
	public class GameOverActivity : Window {
		private ScoreModel model;

		public GameOverActivity() : base(new BorderLayout()) {
		}

		public override void Update(ExGame game, GameTime time) {
			KeyboardState s = Keyboard.GetState();
			if(s.IsKeyDown(Keys.Enter) || s.IsKeyDown(Keys.Escape)) {
				game.Goto("Title");
			}
		}

		public override void Show(ExGame game, ITransitionInfo prefs) {
			this.model = (ScoreModel)prefs[GameActivity.Score];
			ContentPane.RemoveAll();
			SpriteFont font = FlyweightContents.GetInstance().Get<SpriteFont>("Font/MS_26");
			Table table = new Table(new object[,] {
				{ "Shot Count", model.FireCount.ToString().PadLeft(3)},
				{ "Hit Count", model.HitCount.ToString().PadLeft(3)},
				{ "Kill Count", model.KillCount.ToString().PadLeft(3)},
				{ "Accuracy", (model.HitProbability + "%").PadLeft(3)},
				{ "Rank", model.CalculateRank().ToString().PadLeft(3)}
			});
			table.Renderer = new DefaultTableCellRenderer(font);
			Label label = new Label(font, "Return To ESC/Enter");
			ContentPane.Add(table, BorderLayout.Center);
			ContentPane.Add(label, BorderLayout.South);
			ContentPane.Validate();
		}
	}
}
