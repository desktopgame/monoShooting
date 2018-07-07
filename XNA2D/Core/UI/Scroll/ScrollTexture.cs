using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Scroll {
	/// <summary>
	/// スクロール対応の画像です.<br>
	/// <a href="https://msdn.microsoft.com/ja-jp/library/bb203868(v=xnagamestudio.40).aspx">スクロール バックグラウンドの作成</a>
	/// </summary>
	public class ScrollTexture {
		public IScrollManager scrollManager;
		public Texture2D texture;
		private Vector2 position;
		private Vector2 origin;
		public int screenWidth;
		public int screenHeight;

		public ScrollTexture(int screenWidth, int screenHeight, IScrollManager scroll, Texture2D texture) {
			this.texture = texture;
			this.screenWidth = screenWidth;
			this.screenHeight = screenHeight;
			this.scrollManager = scroll;
			Vector2 screenSize = new Vector2(screenWidth, screenHeight);
			this.position = scroll.CreatePosition(screenSize);
			this.origin = scroll.CreateOrigin(screenSize);
		}

		/// <summary>
		/// 経過時間に応じてスクロールします.
		/// </summary>
		/// <param name="gameTime"></param>
		public void Scroll(GameTime gameTime) {
			this.position = scrollManager.GetScrollPosition(gameTime, position, new Vector2(texture.Width, texture.Height));
		}

		/// <summary>
		/// 画面を指定の量スクロールします.
		/// </summary>
		/// <param name="length"></param>
		public void Scroll(float length) {
			this.position = scrollManager.Scroll(position, length);
			while(position.X > texture.Width) {
				position.X -= texture.Width;
			}
			while(position.Y > texture.Height) {
				position.Y -= texture.Height;
			}
			while(position.X < 0) {
				position.X += texture.Width;
			}
			while(position.Y < 0) {
				position.Y += texture.Height;
			}
		}

		/// <summary>
		/// 画像を描画します.
		/// </summary>
		/// <param name="batch"></param>
		public void Draw(SpriteBatch batch) {
			// Draw the texture, if it is still onscreen.
			if(position.Y < screenHeight || position.Y < 0 || position.X < screenWidth || position.X < 0) {
				batch.Draw(texture, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0f);
			}
			// Draw the texture a second time, behind the first,
			// to create the scrolling illusion.
			batch.Draw(texture, position - scrollManager.GetScrollSize(texture.Width, texture.Height), null,
				 Color.White, 0, origin, 1, SpriteEffects.None, 0f);
		}
	}

	/// <summary>
	/// ScrollTextureのスクロール方向です.
	/// </summary>
	public interface IScrollManager {
		/// <summary>
		/// スクロールの最初の位置を返します.
		/// </summary>
		/// <param name="screenSize"></param>
		/// <returns></returns>
		Vector2 CreatePosition(Vector2 screenSize);

		/// <summary>
		/// 画像の最初の切り抜き位置を返します.
		/// </summary>
		/// <param name="screenSize"></param>
		/// <returns></returns>
		Vector2 CreateOrigin(Vector2 screenSize);

		/// <summary>
		/// 次のスクロール位置.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="current"></param>
		/// <param name="textureSize"></param>
		/// <returns></returns>
		Vector2 GetScrollPosition(GameTime gameTime, Vector2 current, Vector2 textureSize);

		/// <summary>
		/// スクロールの幅を返します.<br>
		/// 横スクロールなら横幅、縦スクロールなら縦幅を返します。
		/// <param name="screenWidth"></param>
		/// <param name="screenHeight"></param>
		/// </summary>
		Vector2 GetScrollSize(int screenWidth, int screenHeight);

		/// <summary>
		/// 現在位置から指定の量だけスクロールします.
		/// </summary>
		/// <param name="current"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		Vector2 Scroll(Vector2 current, float length);
	}

	/// <summary>
	/// 水平方向のスクロールをサポートする実装です.
	/// </summary>
	public class HorizontalScroll : IScrollManager {
		private int vector;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="vector">-1なら左, 1なら右</param>
		public HorizontalScroll(int vector) {
			if(vector != -1 && vector != 1) {
				throw new ArgumentException();
			}
			this.vector = vector;
		}

		public Vector2 CreatePosition(Vector2 screenSize) {
			return new Vector2(0, screenSize.Y / 2);
		}

		public Vector2 CreateOrigin(Vector2 screenSize) {
			return new Vector2(0, screenSize.Y / 2);
		}

		public Vector2 GetScrollPosition(GameTime gameTime, Vector2 current, Vector2 textureSize) {
			Vector2 ret = new Vector2(current.X, current.Y);
			float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
			ret.X += ((delta * 100) * vector);
			ret.X = ret.X % textureSize.X;
			return ret;
		}

		public Vector2 GetScrollSize(int screenWidth, int screenHeight) {
			return new Vector2(screenWidth * vector, 0);
		}

		public Vector2 Scroll(Vector2 current, float length) {
			return new Vector2(current.X + (vector * length), current.Y);
		}
	}

	/// <summary>
	/// 垂直方向のスクロールをサポートする実装です.
	/// </summary>
	public class VerticalScroll : IScrollManager {
		private int vector;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="vector">-1なら上, 1なら下</param>
		public VerticalScroll(int vector) {
			if(vector != -1 && vector != 1) {
				throw new ArgumentException();
			}
			this.vector = vector;
		}

		public Vector2 CreatePosition(Vector2 screenSize) {
			return new Vector2(0, screenSize.Y / 2);
		}

		public Vector2 CreateOrigin(Vector2 screenSize) {
			return new Vector2(screenSize.X / 2, 0);
		}

		public Vector2 GetScrollPosition(GameTime gameTime, Vector2 current, Vector2 textureSize) {
			Vector2 ret = new Vector2(current.X, current.Y);
			float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
			ret.Y += ((delta * 100) * vector);
			ret.Y = ret.Y % textureSize.Y;
			return ret;
		}

		public Vector2 GetScrollSize(int screenWidth, int screenHeight) {
			return new Vector2(0, screenHeight * vector);
		}

		public Vector2 Scroll(Vector2 current, float length) {
			return new Vector2(current.X, current.Y + (vector * length));
		}
	}
}
