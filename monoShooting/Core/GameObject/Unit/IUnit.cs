using Bean.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// フィールドに存在する機体です.
	/// </summary>
	public interface IUnit : IGameObject {
		/// <summary>
		/// HP最大値の変更を監視するリスナーのリストです.
		/// </summary>
		event PropertyChangeHandler<float> OnHitPointMaxChanged;

		/// <summary>
		/// HP現在値の変更を監視するリスナーのリストです.
		/// </summary>
		event PropertyChangeHandler<float> OnHitPointValueChanged;

		/// <summary>
		/// 被ダメージを監視するリスナーのリストです.
		/// </summary>
		event DamageHandler OnDamage;

		/// <summary>
		/// HPの最大値.
		/// </summary>
		float HitPointMax {
			set; get;
		}

		/// <summary>
		/// HPの現在値.
		/// </summary>
		float HitPointValue {
			set; get;
		}

		/// <summary>
		/// １フレームで加速するX方向の加速度です.
		/// </summary>
		float SpeedX {
			set; get;
		}

		/// <summary>
		/// １フレームで加速するY方向の加速度です.
		/// </summary>
		float SpeedY {
			set; get;
		}

		/// <summary>
		/// ダメージを受けます.
		/// </summary>
		/// <param name="src"></param>
		void DamageFrom(DamageSource src);
	}
}
