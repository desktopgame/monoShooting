using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject.Item {
	/// <summary>
	/// X軸方向の移動を遅く
	/// </summary>
	public class ItemSlowly : Item {
		
		public ItemSlowly() : base("Image/Item/slowly32", 1000, 3000) {
			this.AccelerationX = -6f;
		}

		protected override void Excecute(GameTime gameTime, Field field) {
			//FIXME:FieldController
			FlyweightContents.GetInstance().Get<SoundEffect>("Sound/Effect/status_down").Play();
			field.Player.SpeedX = Math.Max(2, field.Player.SpeedX - 2);
		}
	}
}
