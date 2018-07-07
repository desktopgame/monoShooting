using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNA2D.Core.UI.Label {
	/// <summary>
	/// 文字/画像を描画するコンポーネント.
	/// </summary>
	public class Label : XNAComponentBase {
		/// <summary>
		/// 描画される文字列.
		/// </summary>
		public string Text {
			set { 
				this.text = value;
				try {
					Vector2 vec = font.MeasureString(text);
					PreferredSize.Width = vec.X;
					PreferredSize.Height = vec.Y;
				} catch(ArgumentException e) {
					throw new ArgumentException("使用出来ない文字列です。コンテンツを確認してください：" + text, e);
				}
			}
			get { return text; }
		}

		/// <summary>
		/// 描画されるアイコン.
		/// </summary>
		public Texture2D Texture {
			set { 
				this.texture = value;
				PreferredSize.Width = value.Width;
				PreferredSize.Height = value.Height;
			}
			get { return texture; }
		}


		private SpriteFont font;
		private string text;
		private Texture2D texture;

		public Label(SpriteFont font, string text) : base() {
			this.font = font;
			if(font == null) {
				throw new ArgumentException("フォントがnullです.");
			}
			this.Text = text;
			this.texture = null;
		}

		public Label(Texture2D texture) : base() {
			if(texture == null) {
				throw new ArgumentException("テクスチャがnullです.");
			}
			this.Texture = texture;
		}

		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
		}

		public override void Draw(Canvas canvas) {
			canvas.Clear(this);
			//文字/画像を描画
			Vector2 vec = Point.ToVector2();
			if(Texture == null) {
				canvas.DrawString(font, Text, vec, Foreground);
			} else {
				canvas.DrawImage(Texture, vec, Color.White);
			}
		}
	}
}
