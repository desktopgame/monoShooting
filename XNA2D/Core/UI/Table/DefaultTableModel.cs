using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace XNA2D.Core.UI.Table {
	/// <summary>
	/// TableModelのデフォルト実装です.
	/// </summary>
	public class DefaultTableModel : TableModel {
		public event TableDataHandler OnDataChanged;

		public object this[int row, int column] {
			set { 
				values[row, column] = value;
				OnDataChanged?.Invoke(this, new TableDataEventArgs(row, column, value));
			}
			get { return values[row, column];  }
		}

		public int ColumnCount {
			private set; get;
		}

		public int RowCount {
			private set;  get;
		}

		private object[,] values;


		public DefaultTableModel(object[,] values) {
			this.values = values;
			this.RowCount = values.GetLength(0);
			this.ColumnCount = values.GetLength(1);
		}

		public DefaultTableModel(int rowCount, int columnCount) : this(new object[rowCount, columnCount]) {
		}

		public void ForEach(Action<object> a) {
			ForEach((row, column, value) => {
				a(value);
				return false;
			});
		}

		public void ForEach(TableAction<object> a) {
			for(int i=0; i<RowCount; i++) {
				for(int j=0; j<ColumnCount; j++) {
					if(a(i, j, this[i, j])) {
						break;
					}
				}
			}
		}
	}
}
