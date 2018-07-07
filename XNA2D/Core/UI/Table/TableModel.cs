using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace XNA2D.Core.UI.Table {
	/// <summary>
	/// 行と列でデータを扱うモデルです.
	/// </summary>
	public interface TableModel {
		/// <summary>
		/// テーブルの要素の置換を監視するリスナーのリストです.
		/// </summary>
		event TableDataHandler OnDataChanged;

		/// <summary>
		/// 指定位置のデータを返します.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		object this[int row, int column] {
			get;
		}
		
		/// <summary>
		/// 行数を返します.
		/// </summary>
		int RowCount {
			get;
		}

		/// <summary>
		/// 列数を返します.
		/// </summary>
		int ColumnCount {
			get;
		}

		/// <summary>
		/// 全ての要素を訪問します.
		/// </summary>
		/// <param name="a"></param>
		void ForEach(Action<object> a);

		/// <summary>
		/// 全ての要素を訪問します.
		/// </summary>
		/// <param name="a"></param>
		void ForEach(TableAction<object> a);
	}
}
