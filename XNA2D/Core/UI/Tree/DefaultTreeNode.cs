using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Tree {
	/// <summary>
	/// ITreeNodeのデフォルト実装です.
	/// </summary>
	public class DefaultTreeNode : ITreeNode {
		public event TreeNodeHandler OnTreeChanged;
		public event EventHandler OnValueChanged;

		public ITreeNode this[int index] {
			get { return children[index]; }
		}

		public int Count {
			get { return children.Count; }
		}

		public ITreeNode Parent {
			private set; get;
		}

		public object Value {
			set {
				this.value = value;
				OnValueChanged?.Invoke(this, EventArgs.Empty);
			}
			get { return value; }
		}

		public bool IsExpanded {
			set {
				this.isExpanded = value;
			}
			get { return isExpanded; }
		}

		private List<ITreeNode> children;
		private object value;
		private bool isExpanded;
		

		public DefaultTreeNode(object value, bool isExpanded) {
			this.children = new List<ITreeNode>();
			this.Value = value;
			this.IsExpanded = isExpanded;
		}

		public DefaultTreeNode(object value) : this(value, true) {
		}

		/// <summary>
		/// 末尾に要素を追加します.
		/// </summary>
		/// <param name="node"></param>
		public void Add(ITreeNode node) {
			children.Add(node);
			OnTreeChanged?.Invoke(this, new TreeNodeEventArgs(node, TreeNodeEventArgs.EventType.Add));
		}

		/// <summary>
		/// 指定位置の要素を削除します.
		/// </summary>
		/// <param name="index"></param>
		public void RemoveAt(int index) {
			ITreeNode node = children[index];
			children.RemoveAt(index);
			OnTreeChanged?.Invoke(this, new TreeNodeEventArgs(node, TreeNodeEventArgs.EventType.Remove));
		}
	}
}
