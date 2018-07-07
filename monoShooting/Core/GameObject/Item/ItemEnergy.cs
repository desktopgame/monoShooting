using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Shoot.Core.GameObject.Item {
	/// <summary>
	/// エネルギー.
	/// </summary>
	public class ItemEnergy : Item {

		public ItemEnergy() : base("Image/Item/energy") {
		}

		protected override void Excecute(GameTime gameTime, Field field) {
			field.Player.EnergyPointValue = field.Player.EnergyPointMax;
		}
	}
}
