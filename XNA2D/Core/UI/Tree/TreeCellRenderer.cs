using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI;

namespace XNA2D.Core.UI.Tree {
	/// <summary>
	/// ツリーを描画するレンダラです.
	/// </summary>
	public interface TreeCellRenderer {
		/// <summary>
		/// ツリーの指定セルを描画するコンポーネントを返します.
		/// </summary>
		/// <param name="tree"></param>
		/// <param name="node"></param>
		/// <param name="depth"></param>
		/// <param name="row"></param>
		/// <param name="hasFocus"></param>
		/// <param name="isSelected"></param>
		/// <returns></returns>
		IXNAComponent GetTreeCellComponent(Tree tree, ITreeNode node, int depth, int row, bool hasFocus, bool isSelected);
	}
}
