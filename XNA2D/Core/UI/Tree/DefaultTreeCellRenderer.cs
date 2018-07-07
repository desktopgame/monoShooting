using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI;
using XNA2D.Core.UI.Label;

namespace XNA2D.Core.UI.Tree {
	/// <summary>
	/// TreeCellRendererのデフォルト実装です.
	/// </summary>
	public class DefaultTreeCellRenderer : Label.Label, TreeCellRenderer {

		public DefaultTreeCellRenderer(SpriteFont font) : base(font, "") {
		}

		public IXNAComponent GetTreeCellComponent(Tree tree, ITreeNode node, int depth, int row, bool hasFocus, bool isSelected) {
			string s = node.Value is string ? (string)node.Value : node.Value.ToString();
			Text = s;
			Foreground = tree.Foreground;
			Background = tree.Background;
			return this;
		}
	}
}
