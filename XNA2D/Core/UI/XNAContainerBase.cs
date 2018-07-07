using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XNA2D.Core.UI {
	/// <summary>
	/// コンテナの基底クラスです.<br>
	/// 子コンポーネントの保持をサポートします.
	/// </summary>
	public abstract class XNAContainerBase : XNAComponentBase, IXNAContainer {
		public IXNAComponent this[int index] {
			get { return children[index]; }
		}

		public int Count {
			get { return children.Count; }
		}

		public ILayoutManager LayoutManager {
			set; get;
		}

		private List<IXNAComponent> children;

		public XNAContainerBase(ILayoutManager layoutManager) : base() {
			this.LayoutManager = layoutManager;
			this.children = new List<IXNAComponent>();
		}

		public XNAContainerBase() : this(null) {
		}

		/// <summary>
		/// サブコンポーネントを更新します.
		/// </summary>
		/// <param name="time"></param>
		/// <param name="keyState"></param>
		/// <param name="mouseState"></param>
		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
			for(int i = 0; i < Count; i++) {
				this[i].Update(time, keyState, mouseState);
			}
		}

		/// <summary>
		/// サブコンポーネントを描画します.
		/// </summary>
		/// <param name="canvas"></param>
		public override void Draw(Canvas canvas) {
			for(int i = 0; i < Count; i++) {
				this[i].Draw(canvas);
			}
		}

		//
		//階層
		//

		public void Add(IXNAComponent component, ILayoutConstraints c) {
			component.Parent = this;
			children.Add(component);
			LayoutManager?.AddLayout(this, component, c);
			Invalidate();
		}

		public void Add(IXNAComponent component) {
			Add(component, null);
		}

		public void Remove(IXNAComponent component) {
			RemoveAt(children.IndexOf(component));
		}

		public void RemoveAt(int index) {
			children.RemoveAt(index);
			this[index].Parent = null;
			LayoutManager?.RemoveLayout(this, this[index]);
			Invalidate();
		}

		public void RemoveAll() {
			for(int i=0; i<Count; i++) {
				IXNAComponent xc = this[i];
				if(xc is IXNAContainer) {
					((IXNAContainer)xc).RemoveAll();
				}
			}
			children.Clear();
			Validate();
		}

		public override void Invalidate() {
			LayoutManager?.InvalidateLayout(this);
			base.Invalidate();
		}

		public override void Validate() {
			if(!IsValid) {
				ValidateTree();
				base.Validate();
			}
		}

		/// <summary>
		/// このコンテナより下位のコンテナのレイアウトを検証します.
		/// </summary>
		protected void ValidateTree() {
			ValidateTreeImpl2(this);
		}

		/// <summary>
		/// 再帰.<br>
		/// こちらだと手前のコンテナからレイアウトされるので最上位のコンテナが拡張されない<br>
		/// 使ってないけど一応残しておく
		/// </summary>
		/// <param name="root"></param>
		private void ValidateTreeImpl(IXNAContainer root) {
			if(!root.IsValid) {
				ILayoutManager lm = root.LayoutManager;
				lm.LayoutContainer(root);
				if(!(root is IValidRoot)) {
					root.PreferredSize = lm.CalculateSize(root);
					root.Size = root.PreferredSize;
				}
			}
			for(int i=0; i<root.Count; i++) {
				IXNAComponent component = root[i];
				if(component is IXNAContainer && !component.IsValid) {
					ValidateTreeImpl((IXNAContainer)component);
				} else {
					component.Validate();
				}
			}
		}

		/// <summary>
		/// 末尾再帰(深さ優先).<br>
		/// この方法だと常に奥のコンテナからレイアウトされるので正しい結果が得られる
		/// </summary>
		/// <param name="root"></param>
		private void ValidateTreeImpl2(IXNAContainer root) {
			for(int i = 0; i < root.Count; i++) {
				IXNAComponent component = root[i];
				if(component is IXNAContainer && !component.IsValid) {
					ValidateTreeImpl((IXNAContainer)component);
				} else {
					component.Validate();
				}
			}
			if(!root.IsValid) {
				ILayoutManager lm = root.LayoutManager;
				lm.LayoutContainer(root);
				if(!(root is IValidRoot)) {
					root.PreferredSize = lm.CalculateSize(root);
					root.Size = root.PreferredSize;
				}
			}
		}
	}
}
