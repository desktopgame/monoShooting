using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Shoot.Core.GameObject.Bullet {
	/// <summary>
	/// 弧.
	/// </summary>
	public class ArcBallistic : IBallistic {
		private float top;
		private bool fall;

		/// <summary>
		/// 最高高度.
		/// </summary>
		/// <param name="top"></param>
		public ArcBallistic(float top) {
			this.top = top;
			this.fall = false;
		}

		/// <summary>
		/// 現在位置-30を最高高度にします.
		/// </summary>
		/// <param name="o"></param>
		public ArcBallistic(IGameObject o) : this(Math.Max(0, o.PositionY - 30)) {
		}

		public Vector2 GetNextDirection(Bullet bullet) {
			Vector2 vec = new Vector2(1, 0);
			if(bullet.PositionY > top && !fall) {
				vec.Y = -0.5f;
			} else if(bullet.PositionY <= top || fall) {
				this.fall = true;
				vec.Y = 0.5f;
			}
			return vec;
		}
	}
}
