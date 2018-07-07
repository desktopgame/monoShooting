using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject.Item {
	/// <summary>
	/// 触れることで発動するアイテム.
	/// </summary>
	public abstract class Item : GameObjectSimple {
		private double spawnTime;
		private float alpha;
		private Schedule scheduleOfBlink;

		private double blink;
		private double despawn;

		public Item(string contentPath, double blink, double despawn) : base(contentPath) {
			this.spawnTime = -1d;
			this.alpha = 1f;
			this.scheduleOfBlink = new Schedule(60f);
			this.blink = blink;
			this.despawn = despawn;
		}

		public Item(string contentPath) : this(contentPath, 5000d, 9000d) {
		}

		public override void Update(GameTime gameTime, Field field) {
			//アイテムが出現してから一定時間経過で点滅、さらに経過で消滅
			double current = gameTime.TotalGameTime.TotalMilliseconds;
			double diff = current - spawnTime;
			if(spawnTime == -1d) {
				this.spawnTime = current;
			} else if(diff > blink && diff < despawn) {
				this.alpha = (scheduleOfBlink.Update(gameTime) ? 0f : 1f);
			} else if(diff > despawn) {
				this.IsDespawn = true;
			}
			//プレイヤーと衝突したら使用
			if(field.Player.IsCollision(this)) {
				Excecute(gameTime, field);
				this.IsDespawn = true;
				return;	
			}
			base.Update(gameTime, field);
		}

		public override void Draw(GameTime gameTime, SpriteBatch batch, Field field) {
			batch.Draw(texture, new Vector2(PositionX, PositionY), Color.White * alpha);
		}

		/// <summary>
		/// アイテムを発動します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="field"></param>
		protected abstract void Excecute(GameTime gameTime, Field field);
	}
}
