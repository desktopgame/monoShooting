using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// フェードアウトアニメーションです.<br>
	/// 既に何か現れていて、徐々に消えていきます。
	/// </summary>
	public class FadeOutAnimation : AnimationFrameProxy<FadeOutAnimation>, IAnimationFrameFactory {
		/// <summary>
		/// 透明度を表すプロパティ.
		/// </summary>
		public float Alpha {
			private set; get;
		}

		public FadeOutAnimation(IDrawable<FadeOutAnimation> delegatez) : base(delegatez) {
		} 

		protected override void BeginImpl() {
			this.Alpha = 1f;
		}

		public override void Update(int offset, GameTime gameTime) {
			Alpha -= 0.01f;
			if(Alpha <= 0f) {
				this.HasNext = false;
			}
		}

		protected override void EndImpl(int offset, List<string> list) {
		}

		public IAnimationFrame Create(params object[] args) {
			return new FadeOutAnimation(Delegate);
		}
	}
}
