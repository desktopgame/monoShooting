using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.List {
	/// <summary>
	/// リストの要素の変更を通知するイベント.
	/// </summary>
	public class ListDataEventArgs : EventArgs {
		/// <summary>
		/// 変更の開始位置.<br>
		/// 一つのみなら終了位置と同じ.
		/// </summary>
		public int StartIndex {
			private set; get;
		}

		/// <summary>
		/// 変更の終了位置.
		/// </summary>
		public int EndIndex {
			private set; get;
		}

		public EventType Type {
			private set; get;
		}

		/// <summary>
		/// イベントの種類.
		/// </summary>
		public enum EventType {
			Add, Remove
		}

		public ListDataEventArgs(int startIndex, int endIndex, EventType type) : base() {
			this.StartIndex = startIndex;
			this.EndIndex = endIndex;
			this.Type = type;
		}
	}

	/// <summary>
	/// ListModelの要素の追加/削除を監視するリスナー.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ListDataHandler(object sender, ListDataEventArgs e);
}
