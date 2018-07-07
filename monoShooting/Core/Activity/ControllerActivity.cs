using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Activity;
using XNA2D.Core.UI;
using XNA2D.Core.UI.Label;
using XNA2D.Core.UI.Table;
using XNA2D.Core.Utils;
using Microsoft.Xna.Framework;

namespace Shoot.Core.Activity {
	/// <summary>
	/// 操作説明
	/// </summary>
	public class ControllerActivity : Window {
		public ControllerActivity() : base(new BorderLayout()) {
            var t = typeof(Microsoft.Xna.Framework.Game);
		}

		public override void LoadContent(ExGame game) {
			SpriteFont font = FlyweightContents.GetInstance().Get<SpriteFont>(game, "Font/MS_26");
			Table table = new Table(new object[,] {
				{ "Arrow", "Move"},
				{ "Z Key" , "Single Shot"},
				{ "X Key(Hold)" , "Laser"},
				{ "B Key", "Bomb"},
				{ "ESC Key" ,"Pause"}
			});
			table.Renderer = new DefaultTableCellRenderer(font);
			Label label = new Label(font, "Return To ESC/Enter");
			ContentPane.Add(table, BorderLayout.Center);
			ContentPane.Add(label, BorderLayout.South);
			ContentPane.Validate();
			base.LoadContent(game);
		}

		public override void Update(ExGame game, GameTime time) {
			KeyboardState s = Keyboard.GetState();
			if(s.IsKeyDown(Keys.Escape) || s.IsKeyDown(Keys.Enter)) {
				game.Goto("Title");
			}
			base.Update(game, time);
		}
	}
}
