using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// ダメージを受ける原因.
	/// </summary>
	public class DamageSource {
		/// <summary>
		/// ダメージの原因(アイテム、敵ユニット等).
		/// </summary>
		public IGameObject Attacker {
			private set; get;
		}

		/// <summary>
		/// 攻撃力.
		/// </summary>
		public float Value {
			private set; get;
		}

		public DamageSource(IGameObject attacker, float value) {
			this.Attacker = attacker;
			this.Value = value;
		}
	}
}
