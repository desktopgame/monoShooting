using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// ボスの基底クラスです.
	/// </summary>
	public abstract class BossBase : UnitSpecial, IBoss {
		private float[][] route;
		private int offset;
		private Schedule scheduleOfChange;
		private Schedule scheduleOfFire;

		public BossBase(string contentPath, float max, float fireLate) : base(contentPath, max) {
			this.route = CreateRoute();
			this.offset = 0;
			this.scheduleOfChange = new Schedule(200);
			this.scheduleOfFire = new Schedule(fireLate);
		}

		public override void Update(GameTime gameTime, Field field) {
			//発射
			if(scheduleOfFire.Update(gameTime)) {
				Fire(gameTime, field);
			}
			//移動
			Move(gameTime, field);
			base.Update(gameTime, field);
		}

		/// <summary>
		/// 攻撃を行います.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="field"></param>
		protected abstract void Fire(GameTime gameTime, Field field);

		/// <summary>
		/// 移動を行います.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="field"></param>
		private void Move(GameTime gameTime, Field field) {
			float[] narray = route[offset];
			this.AccelerationX = SpeedX * narray[0];
			this.AccelerationY = SpeedY * narray[1];
			scheduleOfChange.Interval = narray[2];
			if(scheduleOfChange.Update(gameTime)) {
				offset++;
			}
			if(offset >= route.GetLength(0)) {
				offset = 0;
			}
		}

		/// <summary>
		/// 移動規則を生成します.
		/// [0] = X方向の加速度
		/// [1] = Y方向の加速度
		/// [2] = その移動を継続する時間(ﾐﾘ秒)
		/// </summary>
		/// <returns></returns>
		protected abstract float[][] CreateRoute();
	}
}
