using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Tree {

	/// <summary>
	/// ツリーの要素の増減を監視するリスナー.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void TreeNodeHandler(object sender, TreeNodeEventArgs e);

	/// <summary>
	/// ツリーの要素の増減を通知するイベント.
	/// </summary>
	public class TreeNodeEventArgs : EventArgs {
		/// <summary>
		/// 追加/削除されたノード.
		/// </summary>
		public ITreeNode Node {
			private set;  get;
		}

		/// <summary>
		/// イベントの種類.
		/// </summary>
		public EventType Type {
			private set; get;
		}

		/// <summary>
		/// イベントの種類
		/// </summary>
		public enum EventType {
			Add, Remove
		}

		public TreeNodeEventArgs(ITreeNode parent, EventType t) {
			this.Node = parent;
			this.Type = t;
		}
	}
}
