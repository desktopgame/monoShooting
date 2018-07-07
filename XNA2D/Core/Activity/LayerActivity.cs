using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2D.Core.Activity {
	public class LayerActivity : ActivityAdapter {
		private IActivity prev;
		public static readonly string BG = "BG";


		public LayerActivity() {
		}

		public override void Draw(ExGame game, GameTime time, SpriteBatch batch) {
			prev.Draw(game, time, batch);
			DrawForeground(game, time, batch);
		}

		protected virtual void DrawForeground(ExGame game, GameTime time, SpriteBatch batch) {
		}

		public override void Show(ExGame game, ITransitionInfo info) {
			this.prev = (IActivity)info[BG];
		}
	}
}
