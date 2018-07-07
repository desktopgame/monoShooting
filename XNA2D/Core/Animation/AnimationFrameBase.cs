using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// インターフェイスの空実装を提供します.<br>
	/// サブクラスは適切に各メソッドをオーバーライドしてください。
	/// </summary>
	public abstract class AnimationFrameBase : IAnimationFrame {
		public virtual bool HasNext {
			set; get;
		}

		public AnimationFrameBase() {
			this.HasNext = true;
		}

		/// <summary>
		/// まだ前回起動したアニメーションが完了していないのに呼び出されると例外を投げます.<br>
		/// サブクラスは代わりにImplを実装してください。
		/// </summary>
		public void Begin() {
			if(!HasNext) {
				throw new InvalidOperationException();
			}
			BeginImpl();
		}

		/// <summary>
		/// Beginの実装です.
		/// </summary>
		protected abstract void BeginImpl();

		/// <summary>
		/// アニメーション完了フラグを立てます.<br>
		/// Beginを呼び出し可能になります。<br>
		/// サブクラスは代わりにImplを実装してください。
		/// </summary>
		/// <param name="list"></param>

		public virtual void End(int offset, List<string> list) {
			this.HasNext = true;
			EndImpl(offset, list);
		}

		/// <summary>
		/// Endの実装です.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="list"></param>
		protected abstract void EndImpl(int offset, List<string> list);

		public virtual void Update(int offset, GameTime gameTime) {
		}

		public virtual void Draw(int offset, GameTime gameTime, SpriteBatch batch) {
		}
	}
}
