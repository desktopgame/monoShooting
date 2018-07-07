using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// 描画の詳細な実装を他クラスへ委譲する実装です.<br>
	/// サブクラスは型バインドにサブクラス自身の型をバインドします。(再帰ジェネリクス)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class AnimationFrameProxy<T> : AnimationFrameBase where T : AnimationFrameProxy<T> {
		/// <summary>
		/// 委譲先のオブジェクト.
		/// </summary>
		public IDrawable<T> Delegate {
			set; get;
		}

		public AnimationFrameProxy(IDrawable<T> delegatez) {
			this.Delegate = delegatez;
		}

		public override void Draw(int offset, GameTime gameTime, SpriteBatch batch) {
			Delegate.Draw(gameTime, batch, (T)this);
		}
	}
}
