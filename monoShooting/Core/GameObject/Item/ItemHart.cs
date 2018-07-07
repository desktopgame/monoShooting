using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Shoot.Core.GameObject.Item {
	/// <summary>
	/// 体力回復.
	/// </summary>
	public class ItemHart : Item {
		
		public ItemHart() : base("Image/Item/hart") {
		}

		protected override void Excecute(GameTime gameTime, Field field) {
			float newHP = field.Player.HitPointValue + 10;
			field.Player.HitPointValue = Math.Min(field.Player.HitPointMax, newHP);
		}
	}
}
