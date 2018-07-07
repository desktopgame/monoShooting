using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// 再生中のアニメーションに関するデータオブジェクト.
	/// </summary>
	public class AnimationInstance {
		/// <summary>
		/// 委譲先のフレーム(IAnimationFrameFactoryにその都度生成される).
		/// </summary>
		public IAnimationFrame Frame {
			private set; get;
		}

		/// <summary>
		/// 現在再生中の位置(描画のたびに1加算される).
		/// </summary>
		public int Offset {
			private set; get;
		}

		/// <summary>
		/// ポーズ状態.
		/// </summary>
		public bool IsPause {
			set; get;
		}

		/// <summary>
		/// 再生中であるかどうか.
		/// Frame.HasNext && !IsPause
		/// </summary>
		public bool IsPlaying {
			get { return Frame.HasNext && !IsPause; }
		}

		private AnimationMediator mediator;

		public AnimationInstance(AnimationMediator mediator, IAnimationFrame frame) {
			this.mediator = mediator;
			this.Frame = frame;
			this.Offset = 0;
			this.IsPause = false;
		}

		/// <summary>
		/// アニメーションの初期化処理
		/// </summary>
		protected internal virtual void Begin() {
			Frame.Begin();
		}

		/// <summary>
		/// アニメーションの更新処理
		/// </summary>
		/// <param name="gameTime"></param>
		protected internal virtual void Update(GameTime gameTime) {
			if(!Frame.HasNext || IsPause) {
				return;
			}
			Frame.Update(Offset, gameTime);
		}

		/// <summary>
		/// アニメーションの描画処理
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		protected internal virtual void Draw(GameTime gameTime, SpriteBatch batch) {
			if(!Frame.HasNext || IsPause) {
				return;
			}
			Frame.Draw(Offset++, gameTime, batch);
		}

		/// <summary>
		/// アニメーションの終了処理
		/// </summary>
		/// <param name="list"></param>

		protected internal virtual void End(List<string> list) {
			Frame.End(Offset, list);
		}
	}
}
