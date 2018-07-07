using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject.Item {
	public class ItemAccelerator : Item {
		public ItemAccelerator() : base("Image/Item/accelerator32") {
		}

		protected override void Excecute(GameTime gameTime, Field field) {
			//FIXME:FieldController
			FlyweightContents.GetInstance().Get<SoundEffect>("Sound/Effect/status_up").Play();
			field.Player.SpeedX = Math.Min(20, field.Player.SpeedX + 2);
		}
	}
}
