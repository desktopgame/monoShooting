using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core.GameObject.Bullet {
	/// <summary>
	/// 弾道.
	/// </summary>
	public interface IBallistic {
		/// <summary>
		/// 次の加速方向を返します.
		/// </summary>
		/// <param name="bullet"></param>
		/// <returns></returns>
		Vector2 GetNextDirection(Bullet bullet);
	}
}
