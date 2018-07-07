using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// 描画の実装を提供するインターフェイスです.
	/// </summary>
	/// <typeparam name="A">このインターフェイスを利用するアニメーション</typeparam>
	public interface IDrawable<A> where A : IAnimationFrame {
		/// <summary>
		/// 描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		void Draw(GameTime gameTime, SpriteBatch batch, A animation);
	}
}
