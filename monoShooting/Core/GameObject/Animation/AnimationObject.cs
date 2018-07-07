using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bean.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject.Animation {
	/// <summary>
	/// アニメーション表示されるオブジェクトです.
	/// </summary>
	public class AnimationObject : GameObjectBase {
		/// <summary>
		/// 1コマを表示する時間.
		/// </summary>
		public float Interval {
			set; get;
		}

		/// <summary>
		/// アニメーションされる画像.
		/// </summary>
		public string[] Paths {
			private set; get;
		}

		/// <summary>
		/// アニメーションの進捗.
		/// </summary>
		public int Progress {
			private set; get;
		}


		private Schedule scheduleOfAnimation;
		private Texture2D[] animations;

		public AnimationObject(string[] paths, int interval) {
			this.scheduleOfAnimation = new Schedule(interval);
			this.Paths = paths;
			this.Progress = 0;
			this.animations = new Texture2D[paths.Length];
			int offs = 0;
			Array.ForEach(paths, path => {
				FlyweightContents.GetInstance().Invoke<Texture2D>(path, texture => {
					animations[offs] = texture;
					offs++;
				});
			});
		}

		public AnimationObject(string[] paths) : this(paths, 200) {
		}

		public override void Draw(GameTime gameTime, SpriteBatch batch, Field field) {
			batch.Draw(animations[Progress], new Vector2(PositionX, PositionY), Color.White);
			//コマを進めるのに必要な時間が経過したら
			if(scheduleOfAnimation.Update(gameTime)) {
				Progress++;
			}
			//全てのコマを表示したら
			if(Progress >= animations.Length) {
				IsDespawn = true;
				Progress = 0;
			}
		}
	}
}
