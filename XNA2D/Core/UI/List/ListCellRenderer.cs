using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.List {
	/// <summary>
	/// 項目を描画するレンダラです.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ListCellRenderer<T> {
		/// <summary>
		/// 指定の項目を描画するコンポーネントを返します.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <param name="hasFocus"></param>
		/// <param name="isSelected"></param>
		/// <returns></returns>
		IXNAComponent GetListCellComponent(ItemList<T> list, int index, T value, bool hasFocus, bool isSelected);
	}
}
