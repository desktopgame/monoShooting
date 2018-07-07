using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI {
	/// <summary>
	/// IXNAComponentに委譲する実装です.
	/// </summary>
	public class DrawableGameComponentProxy : DrawableGameComponent {
		/// <summary>
		/// 委譲先のコンポーネントです.
		/// </summary>
		public IXNAComponent Delegate {
			set; get;
		}

		private SpriteBatch batch;

		public DrawableGameComponentProxy(Game game, SpriteBatch batch, IXNAComponent dg) : base(game) {
			this.batch = batch;
			this.Delegate = dg;
		}

		public override void Update(GameTime gameTime) {
			Delegate.Update(gameTime);
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			Delegate.Draw(batch);
			base.Draw(gameTime);
		}
	}
}
