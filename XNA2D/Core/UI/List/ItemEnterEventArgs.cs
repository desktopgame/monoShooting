using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.List {

	/// <summary>
	/// アイテムの決定を監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ItemEnterHandler(object sender, ItemEnterEventArgs e);

	/// <summary>
	/// アイテムの決定を通知するイベントです.
	/// </summary>
	public class ItemEnterEventArgs : EventArgs {
		/// <summary>
		/// 項目の位置です.
		/// </summary>
		public int Index {
			private set; get;
		}

		/// <summary>
		/// 決定された項目の値です.
		/// </summary>
		public object Value {
			private set; get;
		}

		public ItemEnterEventArgs(int index, object value) {
			this.Index = index;
			this.Value = value;
		}
	}
}
