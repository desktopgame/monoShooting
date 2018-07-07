using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject.Item {
	public class ItemSpring : Item {
		public ItemSpring() : base("Image/Item/spring32") {
		}

		protected override void Excecute(GameTime gameTime, Field field) {
			//FIXME:FieldController
			FlyweightContents.GetInstance().Get<SoundEffect>("Sound/Effect/status_up").Play();
			field.Player.SpeedY = Math.Min(20, field.Player.SpeedY + 2);
		}
	}
}
