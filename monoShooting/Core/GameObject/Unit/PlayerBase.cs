using Microsoft.Xna.Framework;
using Shoot.Core.GameObject.Bullet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// IUnitPlayerの基底クラスです.
	/// </summary>
	public class PlayerBase : UnitSpecial, IUnitPlayer {
		public float EnergyPointMax {
			set; get;
		}

		public float EnergyPointValue {
			set; get;
		}

		public float BomPointMax {
			set; get;
		}

		public float BomPointValue {
			set; get;
		}

		private Schedule scheduleOfFireBullet;
		private Schedule scheduleOfHealEnergy;
		private Laser.Laser lastLaser;

		public PlayerBase() : base("Image/Unit/player", 200) {
			this.SpeedX = 10;
			this.SpeedY = 10;
			this.EnergyPointMax = 100;
			this.EnergyPointValue = 100;
			this.BomPointMax = 100;
			this.BomPointValue = 0;
			this.scheduleOfFireBullet = new Schedule(100);
			this.scheduleOfHealEnergy = new Schedule(1200);
		}

		public override void Update(GameTime gameTime, Field field) {
			//指定時間が経過したらエネルギーゲージを回復
			if(scheduleOfHealEnergy.Update(gameTime)) {
				EnergyPointValue = Math.Min(EnergyPointMax, EnergyPointValue + 20);
			}
			base.Update(gameTime, field);
		}

		/// <summary>
		/// 前方に弾を発射します.<br>
		/// 発射レートや残りエネルギー残量によっては何も行いません
		/// </summary>
		/// <param name="time"></param>
		/// <param name="field"></param>
		protected void Fire(GameTime time, Field field) {
			//まだ弾の発射に必要な待機時間が経過していない、エネルギーが足りないなら終了
			if(!scheduleOfFireBullet.UpdateFirst(time) || EnergyPointValue < 10) {
				return;
			}
			//弾を発射
			//プレイヤーは右しか向けないので右に発射する
			Bullet.Bullet bullet = new Bullet.Bullet("Image/playerBeam", this, StraightBallistic.CreateRight(), 10);
			bullet.PositionX = PositionX + Width;
			bullet.PositionY = PositionY + (Height / 2);
			field.Model.Add(bullet);
			this.EnergyPointValue -= 5;
		}

		/// <summary>
		/// 前方にレーザーを照射します.<br>
		/// 残りエネルギー残量によっては何も行いません
		/// </summary>
		/// <param name="time"></param>
		/// <param name="field"></param>
		protected void Irradiation(GameTime time, Field field) {
			if(EnergyPointValue <= 0) {
				lastLaser?.End();
				lastLaser = null;
				return;
			}
			if((lastLaser != null && lastLaser.IsIrradiation)) {
				this.EnergyPointValue = Math.Max(0, EnergyPointValue - 3.0f);
				return;
			}
			Laser.Laser laser = new Laser.Laser(this, Color.Purple);
			laser.PositionX = PositionX + Width;
			laser.PositionY = PositionY + (Height / 2);
			laser.Begin();
			field.Model.Add(laser);
			this.lastLaser = laser;
		}

		/// <summary>
		/// レーザーの照射をキャンセルします.
		/// </summary>
		protected void Cancel() {
			lastLaser?.End();
			lastLaser = null;
		}

		/// <summary>
		/// ボムを発動します.<br>
		/// ボムゲージがマックス出ない場合は何もしません。
		/// </summary>
		/// <param name="field"></param>
		protected void Bomb(Field field) {
			if(BomPointValue != BomPointMax ) {
				return;
			}
			field.Model.ForEach(elem => {
				if(!(elem is IUnit)) {
					return;
				}
				IUnit unit = (IUnit)elem;
				unit.DamageFrom(new DamageSource(this, 100));
				if(unit.HitPointValue <= 0) {
					field.ScoreModel.KillCount++;
				}
			});
			this.BomPointValue = 0;
		}
	}
}
