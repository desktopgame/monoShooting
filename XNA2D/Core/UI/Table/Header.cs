using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Table {
	/// <summary>
	/// ある方向に並ぶ項目の概要を表すヘッダーです.
	/// </summary>
	public interface Header {
		/// <summary>
		/// 指定位置の要素を返します.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		object this[int index] {
			get;
		}

		/// <summary>
		/// 要素の数を返します.
		/// </summary>
		int Count {
			get;
		}
	}
}
