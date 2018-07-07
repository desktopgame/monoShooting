using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// AnimationMediatorを自動で更新/描画するコンポーネント.
	/// </summary>
	public class AnimationComponent : DrawableGameComponent {
		/// <summary>
		/// 委譲先
		/// </summary>
		public AnimationMediator Animation {
			set; get;
		}

		private SpriteBatch batch;

		public AnimationComponent(Game game, SpriteBatch batch, AnimationMediator animation) : base(game) {
			this.Animation = animation;
			this.batch = batch;
		}

		public override void Initialize() {
			base.Initialize();
		}

		public override void Update(GameTime gameTime) {
			Animation.Update(gameTime);
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			Animation.Draw(gameTime, batch);
			base.Draw(gameTime);
		}
	}
}
