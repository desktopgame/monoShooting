using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.List {
	/// <summary>
	/// リスト形式でデータを格納するモデルです.
	/// </summary>
	public interface ListModel<T> {
		/// <summary>
		/// リスト要素の更新を監視するリスナーリストです.
		/// </summary>
		event ListDataHandler OnListDataChanged;

		/// <summary>
		/// 要素数を返します.
		/// </summary>
		int Count {
			get;
		}	

		/// <summary>
		/// 要素を返します.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		T this [int index] {
			get;
		}
	}
}
