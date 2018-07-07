using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// テクスチャの描画を提供します.
	/// </summary>
	public class TextureDrawable : IDrawable<FadeInAnimation>, IDrawable<FadeOutAnimation> {
		private Texture2D texture;
		private Vector2 position;


		public TextureDrawable(Texture2D texture, Vector2 position) {
			this.texture = texture;
			this.position = position;
		}

		public void Draw(GameTime gameTime, SpriteBatch batch, FadeOutAnimation animation) {
			Draw(gameTime, batch, animation.Alpha);
		}

		public void Draw(GameTime gameTime, SpriteBatch batch, FadeInAnimation animation) {
			Draw(gameTime, batch, animation.Alpha);
		}

		private void Draw(GameTime gameTime, SpriteBatch batch, float alpha) {
			batch.Draw(texture, position, Color.White * alpha);
		}
	}
}
