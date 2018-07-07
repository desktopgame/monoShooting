using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.RangeBar {
	/// <summary>
	/// 最小値,現在値,最大値を表すモデルです.
	/// </summary>
	public interface RangeModel {
		/// <summary>
		/// 最小値.<br>
		/// 現在値は常にこれより大きい値です.
		/// </summary>
		float Minimum {
			set; get;
		}

		/// <summary>
		/// 現在値.
		/// </summary>
		float Value {
			set; get;
		}

		/// <summary>
		/// 最大値.<br>
		/// 現在値は常にこれより小さい値です.
		/// </summary>
		float Maximum {
			set; get;
		}
	}
}
