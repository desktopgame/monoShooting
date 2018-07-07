using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI;

namespace XNA2D.Core.Utils {
	/// <summary>
	/// 描画に関するユーティリティクラスです.
	/// </summary>
	public static class DrawUtils {
		/// <summary>
		/// 画面の真ん中に画像を描画します.
		/// </summary>
		/// <param name="device"></param>
		/// <param name="batch"></param>
		/// <param name="texture"></param>
		/// <param name="color"></param>
		public static void DrawImage(GraphicsDeviceManager device, SpriteBatch batch, Texture2D texture, Color color) {
			Vector2 vec2 = GetCenterPosition(device, texture.Width, texture.Height);
			batch.Draw(texture, new Vector2(vec2.X, vec2.Y), color);
		}

		/// <summary>
		/// 白で描画します.
		/// </summary>
		/// <param name="device"></param>
		/// <param name="batch"></param>
		/// <param name="texture"></param>
		public static void DrawImage(GraphicsDeviceManager device, SpriteBatch batch, Texture2D texture) {
			DrawImage(device, batch, texture, Color.White);
		}

		/// <summary>
		/// 画面の真ん中に文字を描画します.
		/// </summary>
		/// <param name="device"></param>
		/// <param name="batch"></param>
		/// <param name="font"></param>
		/// <param name="text"></param>
		/// <param name="color"></param>
		public static void DrawString(GraphicsDeviceManager device, SpriteBatch batch, SpriteFont font, string text, Color color) {
			Vector2 size = font.MeasureString(text);
			Vector2 vec2 = GetCenterPosition(device, size.X, size.Y);
			batch.DrawString(font, text, vec2, color);
		}

		/// <summary>
		/// 黒色で描画します.
		/// </summary>
		/// <param name="device"></param>
		/// <param name="batch"></param>
		/// <param name="font"></param>
		/// <param name="text"></param>
		public static void DrawString(GraphicsDeviceManager device, SpriteBatch batch, SpriteFont font, string text) {
			DrawString(device, batch, font, text, Color.Black);
		}

		/// <summary>
		/// 画面中央に矩形を描画します.
		/// </summary>
		/// <param name="device"></param>
		/// <param name="batch"></param>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public static void DrawRect(GraphicsDeviceManager device, SpriteBatch batch, Rectangle rect, Color color) {
			Vector2 vec2 = GetCenterPosition(device, rect.Width, rect.Height);
			Canvas canvas = DrawManager.GetCurrentCanvas(batch);
			rect.X = (int)vec2.X;
			rect.Y = (int)vec2.Y;
			canvas.DrawRectangle(rect, color);
		}

		/// <summary>
		/// 画面中央に矩形を描画します.
		/// </summary>
		/// <param name="device"></param>
		/// <param name="batch"></param>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public static void FillRect(GraphicsDeviceManager device, SpriteBatch batch, Rectangle rect, Color color) {
			Vector2 vec2 = GetCenterPosition(device, rect.Width, rect.Height);
			Canvas canvas = DrawManager.GetCurrentCanvas(batch);
			rect.X = (int)vec2.X;
			rect.Y = (int)vec2.Y;
			canvas.FillRectangle(rect, color);
		}

		private static Vector2 GetCenterPosition(GraphicsDeviceManager device, float width, float height) {
			float x = (device.PreferredBackBufferWidth - width) / 2;
			float y = (device.PreferredBackBufferHeight - height) / 2;
			return new Vector2(x, y);
		}
	}
}
