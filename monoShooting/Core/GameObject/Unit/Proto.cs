using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNA2D.Core.Utils;
using Shoot.Core.GameObject.Bullet;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// ボス.
	/// </summary>
	public class Proto : BossBase {
		public Proto() : base("Image/Unit/proto", 300, 800) {
			this.SpeedX = 5f;
			this.SpeedY = 5f;
		}
		
		protected override float[][] CreateRoute() {
			return new float[][] {
				new float[]{ 1, 0, 500},
				new float[]{ 0, -1, 1000},
				new float[]{ -1, 0, 500},
				new float[]{ 0, 1, 1500},
				new float[]{ -1, 0, 500},
				new float[]{ 1, 0, 900},
				new float[]{ -1, -1, 300},
			};
		}

		protected override void Fire(GameTime gameTime, Field field) {
			for(int i = 0; i < 3; i++) {
				int n = i + 1;
				Bullet.Bullet bullet = CreateBullet(gameTime, field, i);
				bullet.PositionX = (PositionX - (n * 8));
				bullet.PositionY = PositionY + (Height / 2);
				bullet.SpeedX = -10;
				field.Model.Add(bullet);
			}
		}

		private Bullet.Bullet CreateBullet(GameTime gameTime, Field field, int index) {
			switch(index) {
				case 0:
				case 1:
					return new Bullet.Bullet("Image/enemyBeam", this, StraightBallistic.CreateLeft(), 5);
				case 2:
					return new Bullet.Bullet("Image/enemyBeam", this, new ArcBallistic(this), 5); ;
				default:
					throw new ArgumentException();
			}
		}
	}
}
