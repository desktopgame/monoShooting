using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Shoot.Core.GameObject.Bullet {
	/// <summary>
	/// 直線.
	/// </summary>
	public class StraightBallistic : IBallistic {
		private static Vector2 Right = new Vector2(1, 0);
		private static Vector2 Left = new Vector2(-1, 0);
		private Vector2 vec;

		private StraightBallistic(Vector2 vec) {
			this.vec = vec;
		}

		/// <summary>
		/// 左へ進行する弾道.
		/// </summary>
		/// <returns></returns>
		public static StraightBallistic CreateLeft() {
			return new StraightBallistic(Left);
		}
		
		/// <summary>
		/// 右へ進行する弾道.
		/// </summary>
		/// <returns></returns>
		public static StraightBallistic CreateRight() {
			return new StraightBallistic(Right);
		}

		public Vector2 GetNextDirection(Bullet bullet) {
			return Right;
		}
	}
}
