using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Shoot.Core.GameObject.Bullet {
	/// <summary>
	/// 追尾弾.
	/// </summary>
	public class TrackingBallistic : IBallistic {
		/// <summary>
		/// 追尾対象.
		/// </summary>
		public IGameObject Target {
			private set; get;
		}

		/// <summary>
		/// 曲がる強さ
		/// </summary>
		public float Curve {
			set; get;
		}

		public TrackingBallistic(IGameObject target, float curve) {
			this.Target = target;
			this.Curve = curve;
		}

		public TrackingBallistic(IGameObject target) : this(target, 0.25f) {
		}

		public Vector2 GetNextDirection(Bullet bullet) {
			float diffX = bullet.PositionX - Target.PositionX;
			float diffY = bullet.PositionY - Target.PositionY;
			Vector2 ret = new Vector2(1, 0);
			if(diffY > 0) {
				ret.Y = -Curve;
			} else if(diffY < 0) {
				ret.Y = Curve;
			}
			return ret;
		}

		private float Convert(float f) {
			if(f > 0) {
				return 1;
			}
			if(f < 0) {
				return -1;
			}
			return 0;
		}
	}
}
