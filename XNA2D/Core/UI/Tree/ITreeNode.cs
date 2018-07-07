using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Tree {
	/// <summary>
	/// ツリーを構成する要素です.
	/// </summary>
	public interface ITreeNode {

		/// <summary>
		/// ツリー内の要素の増減を監視するリスナーのリストです.
		/// </summary>
		event TreeNodeHandler OnTreeChanged;

		/// <summary>
		/// ツリー内の表示値の変更を監視するリスナーのリストです.
		/// </summary>
		event EventHandler OnValueChanged;

		/// <summary>
		/// 親要素を返します.
		/// </summary>
		ITreeNode Parent {
			get;
		}

		/// <summary>
		/// 表示する値.
		/// </summary>
		object Value {
			set; get;
		}

		/// <summary>
		/// 折り畳みの状態.
		/// </summary>
		bool IsExpanded {
			set; get;
		}

		/// <summary>
		/// 子要素の数.
		/// </summary>
		int Count {
			get;
		}

		/// <summary>
		/// 指定位置の子要素を返します.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		ITreeNode this[int index] {
			get;
		}
	}
}
