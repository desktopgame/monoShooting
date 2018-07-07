using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// 文字列の描画を提供します.
	/// </summary>
	public class TextDrawable : IDrawable<FadeInAnimation>, IDrawable<FadeOutAnimation> {
		private SpriteFont font;
		private string text;
		private Vector2 position;
		private Color foreground;

		public TextDrawable(SpriteFont font, string text, Vector2 position, Color foreground) {
			this.font = font;
			this.text = text;
			this.position = position;
			this.foreground = foreground;
		}

		public TextDrawable(SpriteFont font, string text, Vector2 position) : this(font, text, position, Color.Black) {
		}

		public void Draw(GameTime gameTime, SpriteBatch batch, FadeInAnimation animation) {
			Draw(gameTime, batch, animation.Alpha);
		}

		public void Draw(GameTime gameTime, SpriteBatch batch, FadeOutAnimation animation) {
			Draw(gameTime, batch, animation.Alpha);
		}
		
		private void Draw(GameTime gameTime, SpriteBatch batch, float alpha) {
			batch.DrawString(font, text, position, foreground * alpha);
		}
	}
}
