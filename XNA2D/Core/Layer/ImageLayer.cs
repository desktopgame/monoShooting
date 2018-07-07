using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Layer {
	/// <summary>
	/// 画像を中心に表示するレイヤーです.
	/// </summary>
	public class ImageLayer : LayerBase {
		/// <summary>
		/// 表示される画像です.
		/// </summary>
		public Texture2D Image {
			set; get;
		}

		public ImageLayer(Rectangle allocate, Texture2D image, float alpha) : base(allocate, alpha) {
			this.Image = image;
		}

		public ImageLayer(Rectangle allocate, Texture2D image) : this(allocate, image, 1f) {
		}

		public override void Draw(GameTime gameTime, SpriteBatch batch) {
			float iw = Image.Width;
			float ih = Image.Height;
			float x = Allocate.X + ((Allocate.Width - iw) / 2);
			float y = Allocate.Y + ((Allocate.Height - ih) / 2);
			Vector2 drawPosition = new Vector2(x, y);
			batch.Draw(Image, drawPosition, Background * Alpha);
		}
	}
}
