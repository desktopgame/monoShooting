using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Table {

	/// <summary>
	/// テーブルの要素の置換を監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void TableDataHandler(object sender, TableDataEventArgs e);

	/// <summary>
	/// テーブルの要素の置換を通知するイベントです.
	/// </summary>
	public class TableDataEventArgs : EventArgs {
		/// <summary>
		/// 置換された行.
		/// </summary>
		public int Row {
			private set; get;
		}

		/// <summary>
		/// 置換された列.
		/// </summary>
		public int Column {
			private set; get;
		}

		/// <summary>
		/// 新しい値.
		/// </summary>
		public object Value {
			private set; get;
		}

		public TableDataEventArgs(int row, int column, object value) {
			this.Row = row;
			this.Column = column;
			this.Value = value;
		}
	}
}
