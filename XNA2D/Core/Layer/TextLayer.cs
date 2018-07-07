using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Layer {
	/// <summary>
	/// 文字を中心に表示するレイヤーです.
	/// </summary>
	public class TextLayer : LayerBase {
		/// <summary>
		/// 描画時のフォントです.
		/// </summary>
		public SpriteFont Font {
			set; get;
		}

		/// <summary>
		/// 描画されるテキストです.
		/// </summary>
		public string Text {
			set; get;
		}

		public TextLayer(Rectangle allocate, SpriteFont font, string text, float alpha) : base(allocate, alpha) {
			this.Font = font;
			this.Text = text;
		}

		public TextLayer(Rectangle allocate, SpriteFont font, string text) : this(allocate, font, text, 1f) {
		}

		public TextLayer(Rectangle allocate, SpriteFont font) : this(allocate, font, "") {
		}

		public override void Draw(GameTime gameTime, SpriteBatch batch) {
			Vector2 fontSize = Font.MeasureString(Text);
			float iw = fontSize.X;
			float ih = fontSize.Y;
			float x = Allocate.X + ((Allocate.Width - iw) / 2);
			float y = Allocate.Y + ((Allocate.Height - ih) / 2);
			Vector2 drawPosition = new Vector2(x, y);
			batch.DrawString(Font, Text, drawPosition, Foreground * Alpha);
		}
	}
}
