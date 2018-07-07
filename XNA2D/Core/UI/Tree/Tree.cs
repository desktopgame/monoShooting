using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.UI;
using XNA2D.Core.UI.Tree;

namespace XNA2D.Core.UI.Tree {
	//FIXME:雑
	/// <summary>
	/// 要素を階層的に表示するコンポーネントです.
	/// </summary>
	public class Tree : XNAComponentBase {
		/// <summary>
		/// ルート要素.
		/// </summary>
		public ITreeNode Root {
			set; get;
		}

		/// <summary>
		/// セルを描画するレンダラー.
		/// </summary>
		public TreeCellRenderer Renderer {
			set; get;
		}

		public Tree(ITreeNode root) {
			this.Root = root;
		}

		public Tree() : this(CreateSampleTree()) {
		}

		private static ITreeNode CreateSampleTree() {
			DefaultTreeNode res = new DefaultTreeNode("ルート");
			DefaultTreeNode menuA = new DefaultTreeNode("MenuA");
			menuA.Add(new DefaultTreeNode("ItemA"));
			menuA.Add(new DefaultTreeNode("ItemB"));
			menuA.Add(new DefaultTreeNode("ItemC"));
			DefaultTreeNode menuB = new DefaultTreeNode("MenuB");
			DefaultTreeNode menuC = new DefaultTreeNode("MenuC");
			menuC.Add(new DefaultTreeNode("ItemF"));
			menuC.Add(new DefaultTreeNode("ItemG"));
			menuB.Add(new DefaultTreeNode("ItemD"));
			menuB.Add(new DefaultTreeNode("ItemE"));
			menuB.Add(menuC);
			res.Add(menuA);
			res.Add(menuB);
			return res;
		}

		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
		}

		public override void Draw(Canvas canvas) {
			canvas.Clear(this);
			Draw(canvas, Root, 0, Point.X, Point.Y);
		}

		private float Draw(Canvas canvas, ITreeNode root, int depth, float x, float y) {
			TreeCellRenderer renderer = Renderer;
			//親要素の描画
			IXNAComponent parent = renderer.GetTreeCellComponent(this, root, depth, 0, HasFocus, false);
			parent.Point.X = x;
			parent.Point.Y = y;
			parent.Size = parent.PreferredSize;
			parent.Draw(canvas);
			//折りたたまれてるなら子要素は無視
			if(!root.IsExpanded) {
				return y;
			}
			depth++;
			//子要素の描画
			for(int i=0; i<root.Count; i++) {
				ITreeNode node = root[i];
				y = Draw(canvas, node, depth, x + 50, y + (20 * depth));
			}
			return y;
		}
	}
}
