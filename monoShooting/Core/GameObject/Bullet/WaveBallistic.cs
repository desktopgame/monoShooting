using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Shoot.Core.GameObject.Bullet {
	/// <summary>
	///	波
	/// </summary>
	public class WaveBallistic : IBallistic {
		private float height;
		private float targetTopY;
		private float targetBottomY;
		private bool init = false;


		public WaveBallistic(float height) {
			this.height = height;
		}

		public WaveBallistic() : this(20) {
		}

		public Vector2 GetNextDirection(Bullet bullet) {
			Vector2 vec = new Vector2(1, 0);
			if(!init) {
				this.targetTopY = bullet.PositionY - height;
				this.targetBottomY = bullet.PositionY + height;
				this.init = true;
			}
			if(bullet.PositionY > targetTopY) {
				vec.Y = -0.5f;
			} else if(bullet.PositionY <= targetBottomY) {
				vec.Y = 0.5f;
			}
			return vec;
		}
	}
}
