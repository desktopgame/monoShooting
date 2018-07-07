using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace XNA2D.Core.UI {
	/// <summary>
	/// コンテナに描画を行うためのキャンバスです.<br>
	/// <a href="http://stackoverflow.com/questions/2792694/draw-rectangle-with-xna">Draw Rectangle with XNA</a>
	/// </summary>
	public class Canvas : IDisposable {
		private SpriteBatch batch;
		private Texture2D nullTexture;

		public Canvas(SpriteBatch batch) {
			this.batch = batch;
			//空のデータが設定されたテクスチャ
			this.nullTexture = new Texture2D(batch.GraphicsDevice, 1, 1);
			this.nullTexture.SetData(new Color[] { Color.White });
		}

		/// <summary>
		/// コンポーネントが不透明なら背景色で塗りつぶします.
		/// </summary>
		/// <param name="c"></param>
		public void Clear(IXNAComponent c) {
			if(c.IsOpaque) {
				FillRectangle(c.Bounds.ToRectangle(), c.Background);
			}
		}

		#region SpriteBatchへの委譲
		/// <summary>
		/// SpriteBatchの同名メソッドを参照してください.
		/// </summary>
		/// <param name="font"></param>
		/// <param name="text"></param>
		/// <param name="position"></param>
		/// <param name="color"></param>
		public void DrawString(SpriteFont font, string text, Vector2 position, Color color) {
			batch.DrawString(font, text, position, color);
		}

		/// <summary>
		/// DrawUtilsの同名メソッドを参照してください.
		/// </summary>
		/// <param name="deviceManager"></param>
		/// <param name="texture"></param>
		public void DrawImage(GraphicsDeviceManager deviceManager, Texture2D texture) {
			DrawUtils.DrawImage(deviceManager, batch, texture);
		}

		/// <summary>
		/// SpriteBatchの同名メソッドを参照してください.
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public void DrawImage(Texture2D texture, Rectangle rect, Color color) {
			batch.Draw(texture, rect, color);
		}

		/// <summary>
		/// SpriteBatchの同名メソッドを参照してください.
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="dst"></param>
		/// <param name="src"></param>
		/// <param name="color"></param>
		public void DrawImage(Texture2D texture, Rectangle dst, Nullable<Rectangle> src, Color color) {
			batch.Draw(texture, dst, src, color);
		}

		/// <summary>
		/// SpriteBatchの同名メソッドを参照してください.
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="dst"></param>
		/// <param name="src"></param>
		/// <param name="color"></param>
		/// <param name="rotation"></param>
		/// <param name="origin"></param>
		/// <param name="effects"></param>
		/// <param name="layerDepth"></param>
		public void DrawImage(Texture2D texture, Rectangle dst, Nullable<Rectangle> src, Color color, Single rotation, Vector2 origin, SpriteEffects effects, Single layerDepth) {
			batch.Draw(texture, dst, src, color, rotation, origin, effects, layerDepth);
		}

		/// <summary>
		/// SpriteBatchの同名メソッドを参照してください.
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="position"></param>
		/// <param name="color"></param>
		public void DrawImage(Texture2D texture, Vector2 position, Color color) {
			batch.Draw(texture, position, color);
		}

		/// <summary>
		/// SpriteBatchの同名メソッドを参照してください.
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="position"></param>
		/// <param name="src"></param>
		/// <param name="color"></param>
		public void DrawImage(Texture2D texture, Vector2 position, Nullable<Rectangle> src, Color color) {
			batch.Draw(texture, position, src, color);
		}

		/// <summary>
		/// SpriteBatchの同名メソッドを参照してください.
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="position"></param>
		/// <param name="src"></param>
		/// <param name="color"></param>
		/// <param name="rotation"></param>
		/// <param name="origin"></param>
		/// <param name="scale"></param>
		/// <param name="effects"></param>
		/// <param name="layerDepth"></param>
		public void DrawImage(Texture2D texture, Vector2 position, Nullable<Rectangle> src, Color color, Single rotation, Vector2 origin, Single scale, SpriteEffects effects, Single layerDepth) {
			batch.Draw(texture, position, src, color, rotation, origin, scale, effects, layerDepth);
		}

		/// <summary>
		/// SpriteBatchの同名メソッドを参照してください.
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="position"></param>
		/// <param name="src"></param>
		/// <param name="color"></param>
		/// <param name="rotation"></param>
		/// <param name="origin"></param>
		/// <param name="scale"></param>
		/// <param name="effects"></param>
		/// <param name="layerDepth"></param>
		public void DrawImage(Texture2D texture, Vector2 position, Nullable<Rectangle> src, Color color, Single rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, Single layerDepth) {
			batch.Draw(texture, position, src, color, rotation, origin, scale, effects, layerDepth);
		}
		#endregion

		#region 空テクスチャを利用した図形の描画等
		/// <summary>
		/// 指定の矩形を塗りつぶします.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public void FillRectangle(Rectangle rect, Color color) {
			batch.Draw(nullTexture, rect, color);
		}

		/// <summary>
		/// 指定の矩形を枠のみ描画します.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public void DrawRectangle(Rectangle rect, Color color) {
			batch.Draw(nullTexture, new Rectangle(rect.X, rect.Y, rect.Width, 1), color);
			batch.Draw(nullTexture, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width, 1), color);
			batch.Draw(nullTexture, new Rectangle(rect.X, rect.Y, 1, rect.Height), color);
			batch.Draw(nullTexture, new Rectangle(rect.X + rect.Width, rect.Y, 1, rect.Height), color);
		}

		/// <summary>
		/// 開始位置から終了位置にかけて線を引きます.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="color"></param>
		public void DrawLine(Vector2 start, Vector2 end, Color color) {
			float length = (end - start).Length();
			float rotation = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
			batch.Draw(nullTexture, start, null, color, rotation, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
		}
		#endregion

		/// <summary>
		/// 内部的に利用されている空テクスチャを破棄します.
		/// </summary>
		public void Dispose() {
			nullTexture.Dispose();
		}
	}
}
