using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.List {
	/// <summary>
	/// 選択項目を管理するモデル.
	/// </summary>
	public interface SingleSelectionModel {
		/// <summary>
		/// 位置の変更を監視するリスナーのリスト.
		/// </summary>
		event EventHandler OnStateChanged;

		/// <summary>
		/// 選択されている項目の位置.
		/// </summary>
		int SelectedIndex {
			set; get;
		}
	}
}
