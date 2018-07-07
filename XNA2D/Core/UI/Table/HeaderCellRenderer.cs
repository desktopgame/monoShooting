using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI;

namespace XNA2D.Core.UI.Table {
	/// <summary>
	/// ヘッダ要素を描画するレンダラです.
	/// </summary>
	public interface HeaderCellRenderer {
		/// <summary>
		/// ヘッダを描画するコンポーネントを返します.
		/// </summary>
		/// <param name="table"></param>
		/// <param name="header"></param>
		/// <param name="index">コーナーを描画するときは-1</param>
		/// <param name="value">コーナーを描画するときはnull</param>
		/// <returns></returns>
		IXNAComponent GetHeaderCellComponent(Table table, Header header, int index, object value);
	}
}
