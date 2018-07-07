using Shoot.Core.GameObject.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNA2D.Core.Utils;
using Microsoft.Xna.Framework.Audio;

namespace Shoot.Core.GameObject.Bullet {
	/// <summary>
	/// 敵と衝突するとダメージを与える弾.
	/// </summary>
	public class Bullet : UnitBase {
		/// <summary>
		/// 弾を発射したオブジェクト.<br>
		/// このエンティティ自身がこの弾と衝突することはない。
		/// </summary>
		public IGameObject GameObject {
			private set; get;
		}

		/// <summary>
		/// 弾道.
		/// </summary>
		public IBallistic Ballistic {
			private set; get;
		}

		/// <summary>
		/// 攻撃力.
		/// </summary>
		public float Damage {
			private set; get;
		}

		private bool fire;

		public Bullet(string contentPath, IGameObject gameObject, IBallistic ballistic, float damage) : base(contentPath, 1) {
			this.GameObject = gameObject;
			this.Ballistic = ballistic;
			this.Damage = damage;
			this.SpeedX = 10;
			this.SpeedY = 10;
			this.fire = false;
		}

		public override void Update(GameTime gameTime, Field field) {
			if(!fire) {
				if(GameObject is IUnitPlayer) {
					field.ScoreModel.FireCount++;
				}
				this.fire = true;
				FlyweightContents.GetInstance().Get<SoundEffect>("Sound/Effect/pistol").Play();
			}
			CheckPosition(gameTime, field);
			CheckCollision(gameTime, field);
			base.Update(gameTime, field);
		}

		private void CheckPosition(GameTime gameTime, Field field) {
			Vector2 vec = Ballistic.GetNextDirection(this);
			this.AccelerationX = SpeedX * vec.X;
			this.AccelerationY = SpeedY * vec.Y;
		}

		private void CheckCollision(GameTime gameTime, Field field) {
			//あたり判定を検証
			Tuple<bool, IUnit[]> result = GameObjectUtils.Validate(this, GameObject, field, 20, false);
			if(GameObject is IUnitPlayer && result.Item1) {
				this.IsDespawn = true;
				field.ScoreModel.HitCount++;
				if(result.Item2[0].HitPointValue <= 0) {
					field.ScoreModel.KillCount++;
				}
			}
			//壁に衝突したので消滅する
			if(PositionX <= 0 || PositionX + Width >= field.Width ||
			   PositionY - Height <= 0 || PositionY + Height >= field.Height) {
				this.IsDespawn = true;
			}
		}
	}
}
