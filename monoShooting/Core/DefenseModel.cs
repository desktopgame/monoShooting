using Bean.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core {
	/// <summary>
	/// 防衛ゲージです.
	/// </summary>
	public interface DefenseModel {
		/// <summary>
		/// 防衛ゲージの最大値の変動を監視するリスナーのリストです.
		/// </summary>
		event PropertyChangeHandler<float> OnDefenceMaximumChanged;

		/// <summary>
		/// 防衛ゲージの現在値の変動を監視するリスナーのリストです.
		/// </summary>
		event PropertyChangeHandler<float> OnDefenceValueChanged;

		/// <summary>
		/// ゲージの最大値です.
		/// </summary>
		float Maximum {
			get;
		}

		/// <summary>
		/// ゲージの現在値です.
		/// </summary>
		float Value {
			set; get;
		}
	}
}
