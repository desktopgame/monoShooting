using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject {
	/// <summary>
	/// 画像をそのまま描画する実装です.
	/// </summary>
	public class GameObjectSimple : GameObjectBase {
		protected readonly string contentPath;
		protected Texture2D texture;

		public GameObjectSimple(string contentPath) {
			this.contentPath = contentPath;
			FlyweightContents.GetInstance().Invoke<Texture2D>(contentPath, content => {
				this.texture = content;
				this.Width = texture.Width;
				this.Height = texture.Height;
			});
		}

		public override void Draw(GameTime gameTime, SpriteBatch batch, Field field) {
			batch.Draw(texture, new Vector2(PositionX, PositionY), Color.White);
		}
	}
}
