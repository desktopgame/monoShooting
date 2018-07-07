using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI;

namespace XNA2D.Core.UI.Table {
	/// <summary>
	/// 項目を描画するレンダラです.
	/// </summary>
	public interface TableCellRenderer {
		/// <summary>
		/// 指定の項目を描画するコンポーネントを返します.
		/// </summary>
		/// <param name="table"></param>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <param name="value"></param>
		/// <param name="hasFocus"></param>
		/// <param name="isSelected"></param>
		/// <returns></returns>
		IXNAComponent GetTableCellComponent(Table table, int row, int column, object value, bool hasFocus, bool isSelected);
	}
}
