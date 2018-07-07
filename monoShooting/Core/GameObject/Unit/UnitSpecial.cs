using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// 無敵時間を持つユニット.
	/// </summary>
	public class UnitSpecial : UnitBase {
		private Schedule scheduleOfInvisible;
		private bool isInvisible;

		public UnitSpecial(string contentPath, float max) : base(contentPath, max) {
			this.scheduleOfInvisible = new Schedule(1500);
			this.isInvisible = false;
		}

		public override void DamageFrom(DamageSource src) {
			if(isInvisible) {
				return;
			}
			this.isInvisible = true;
			base.DamageFrom(src);
		}

		public override void Update(GameTime gameTime, Field field) {
			if(scheduleOfInvisible.Update(gameTime) && isInvisible) {
				this.isInvisible = false;
			}
			base.Update(gameTime, field);
		}

		public override void Draw(GameTime gameTime, SpriteBatch batch, Field field) {
			Color white = Color.White;
			if(isInvisible) {
				white *= 0.5f;
			}
			batch.Draw(texture, new Vector2(PositionX, PositionY), white);
		}
	}
}
