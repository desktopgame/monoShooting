using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Shoot.Core.GameObject.Item;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// IUnitEnemyの基底クラスです.
	/// </summary>
	public abstract class UnitEnemyBase : UnitBase, IUnitEnemy {
		public event EventHandler OnGoaled;


		public UnitEnemyBase(string contentPath, float max) : base(contentPath, max) {
		}

		public override void Update(GameTime gameTime, Field field) {
			//プレイヤーと接触したらダメージ
			if(field.Player.IsCollision(this)) {
				field.Player.DamageFrom(new DamageSource(this, 10));
			}
			//画面左端に到達したら消える
			if(PositionX <= 0) {
				OnGoaled?.Invoke(this, EventArgs.Empty);
				this.IsDespawn = true;
			}
			base.Update(gameTime, field);
		}

		public abstract Item.Item CreateDropItem();
	}
}
