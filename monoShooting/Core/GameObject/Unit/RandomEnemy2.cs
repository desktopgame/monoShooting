using Shoot.Core.GameObject.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shoot.Core.GameObject.Item;
using Microsoft.Xna.Framework;
using Shoot.Core;
using XNA2D.Core.Utils;
using Shoot.Core.GameObject.Bullet;

namespace Shoot.Core.GameObject.Unit {
	public class RandomEnemy2 : RandomEnemy {
		private int index;
		public RandomEnemy2() : base() {
			this.index = Random.Next(0, 5);
		}

		protected override void Fire(GameTime gameTime, Field field) {
			if(index != 1) {
				base.Fire(gameTime, field);
				return;
			}
			int n = Random.Next(0, 2);
			Item.Item item = n == 0 ? (Item.Item)new ItemHeavy() : new ItemSlowly();
			item.PositionX = PositionX;
			item.PositionY = PositionY;
			field.Model.Add(item);
		}

		public override Item.Item CreateDropItem() {
			if(index != 1) {
				return base.CreateDropItem();
			}
			//たまに能力上昇系のやつを落とす
			int n = Random.Next(0, 5);
			if(n == 0) {
				return new ItemSpring();
			} else if(n == 1) {
				return new ItemAccelerator();
			}
			return null;
		}
	}
}
