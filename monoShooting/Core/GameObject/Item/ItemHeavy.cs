using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNA2D.Core.Utils;
using Microsoft.Xna.Framework.Audio;

namespace Shoot.Core.GameObject.Item {
	/// <summary>
	/// Y方向の移動を遅く
	/// </summary>
	public class ItemHeavy : Item {
		public ItemHeavy() : base("Image/Item/heavy32", 1000, 3000) {
			this.AccelerationX = -6f;
		}

		protected override void Excecute(GameTime gameTime, Field field) {
			//FIXME:FieldController
			FlyweightContents.GetInstance().Get<SoundEffect>("Sound/Effect/status_down").Play();
			field.Player.SpeedY = Math.Max(2, field.Player.SpeedY - 2);
		}
	}
}
