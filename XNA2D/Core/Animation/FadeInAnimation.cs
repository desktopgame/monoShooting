using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// フェードインアニメーションです.<br>
	/// 最初は透明な状態で、徐々に何かが現れます。
	/// </summary>
	public class FadeInAnimation : AnimationFrameProxy<FadeInAnimation>, IAnimationFrameFactory {
		/// <summary>
		/// 透明度を表すプロパティ.
		/// </summary>
		public float Alpha {
			private set; get;
		}


		public FadeInAnimation(IDrawable<FadeInAnimation> delegatez) : base(delegatez) {
		}

		protected override void BeginImpl() {
			this.Alpha = 0;
		}

		public override void Update(int offset, GameTime gameTime) {
			Alpha += 0.01f;
			if(Alpha >= 1f) {
				this.HasNext = false;
			}
		}

		protected override void EndImpl(int offset, List<string> list) {
		}

		public IAnimationFrame Create(params object[] args) {
			return new FadeInAnimation(Delegate);
		}
	}
}
