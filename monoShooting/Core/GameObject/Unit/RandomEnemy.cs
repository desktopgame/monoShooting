using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNA2D.Core.Utils;
using Shoot.Core.GameObject.Bullet;
using Shoot.Core.GameObject.Item;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// 敵ユニット(lv1).
	/// </summary>
	public class RandomEnemy : UnitEnemyBase, IUnitEnemy {
		protected static readonly Random Random = new Random();
		private static readonly string[] Names = { "enemy", "enemy2" };
		private static readonly int[] Intervals = { 400, 800, 1200 };

		private Vector2 buffer;
		private Schedule scheduleOfFire;
		private Schedule scheduleOfReverse;
		
		public RandomEnemy() : base(RandomName(), 10) {
			this.SpeedX = 2;
			this.SpeedY = 2;
			this.scheduleOfFire = new Schedule((RandomInterval() * 2) + 1000);
			this.scheduleOfReverse = new Schedule(RandomInterval());
			float x = Random.Next(1, 3);
			float y = Random.Next(1, 3) * -1;
			this.buffer = new Vector2(x, y);
		}

		/// <summary>
		/// ランダムな画像パスを返します.
		/// </summary>
		/// <returns></returns>
		public static string RandomName() {
			int index = Random.Next(0, Names.Length);
			return "Image/Unit/" + Names[index];
		}

		private static int RandomInterval() {
			int index = Random.Next(0, Intervals.Length);
			return Intervals[index];
		}

		public override void Update(GameTime gameTime, Field field) {
			this.AccelerationX = -SpeedX;
			this.AccelerationY = SpeedY * buffer.Y;
			//指定時間経過したら軌道変更
			if(scheduleOfReverse.Update(gameTime)) {
				Reverse();
			}
			//指定時間経過したら発射
			if(scheduleOfFire.Update(gameTime)) {
				//弾を発射
				Fire(gameTime, field);
			}
			base.Update(gameTime, field);
		}

		protected virtual void Fire(GameTime gameTime, Field field) {
			//エネミーは左しか向けないので左に発射する
			Bullet.Bullet bullet = new Bullet.Bullet("Image/enemyBeam", this, new TrackingBallistic(field.Player), 10);
			bullet.PositionX = PositionX + Width;
			bullet.PositionY = PositionY + (Height / 2);
			bullet.SpeedX = -6;
			field.Model.Add(bullet);
		}

		private void Reverse() {
			float tmp = buffer.X;
			buffer.X = buffer.Y;
			buffer.Y = tmp;
		}

		public override Item.Item CreateDropItem() {
			int index = Random.Next(0, 10);
			if(index != 0) {
				return null;
			}
			return new ItemHart();
		}
	}
}
