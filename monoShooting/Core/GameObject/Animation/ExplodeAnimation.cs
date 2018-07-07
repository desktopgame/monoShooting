using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core.GameObject.Animation {
	/// <summary>
	/// 爆発アニメーション.
	/// </summary>
	public class ExplodeAnimation : AnimationObject {
		public ExplodeAnimation(IGameObject o) : 
			base(new string[] { "Image/Animation/bomb", "Image/Animation/bomb2"}) {
			this.PositionX = o.PositionX;
			this.PositionY = o.PositionY;
		}
	}
}
