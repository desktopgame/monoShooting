using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Layer {
	public class LayerComponent : DrawableGameComponent {
		public Layer Delegate {
			set; get;
		}

		private SpriteBatch batch;

		public LayerComponent(Game game, SpriteBatch batch, Layer layer) : base(game) {
			this.Delegate = layer;
			this.batch = batch;
		}

		public override void Update(GameTime gameTime) {
			Delegate.Update(gameTime);
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			Delegate.Draw(gameTime, batch);
			base.Draw(gameTime);
		}
	}
}
