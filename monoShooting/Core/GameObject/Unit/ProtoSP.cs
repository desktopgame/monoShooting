using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Shoot.Core.GameObject.Bullet;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// Protoの強化版.
	/// </summary>
	public class ProtoSP : Proto {
		private Schedule scheduleOfFire;
		public ProtoSP() : base() {
			this.SpeedX = 4.5f;
			this.SpeedY = 4.5f;
			this.scheduleOfFire = new Schedule(600);
		}
		

		protected override void Fire(GameTime gameTime, Field field) {
			for(int i = 0; i < 4; i++) {
				int n = i + 1;
				Bullet.Bullet bullet = CreateBullet(gameTime, field, i);
				bullet.PositionX = (PositionX - (n * 6));
				bullet.PositionY = PositionY + (Height / 2);
				bullet.SpeedX = -10;
				field.Model.Add(bullet);
			}
		}

		private Bullet.Bullet CreateBullet(GameTime gameTime, Field field, int index) {
			switch(index) {
				case 0:
				case 1:
					return new Bullet.Bullet("Image/enemyBeam", this, new TrackingBallistic(field.Player, 0.5f), 2);
				case 2:
					return new Bullet.Bullet("Image/enemyBeam", this, new ArcBallistic(this), 2);
				case 3:
					return new Bullet.Bullet("Image/enemyBeam", this, new WaveBallistic(120), 2);
				default:
					throw new ArgumentException();
			}
		}
	}
}
