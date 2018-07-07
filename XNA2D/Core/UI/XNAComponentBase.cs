using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNA2D.Core.UI {
	/// <summary>
	/// IXNAComponentの基底クラスです.
	/// </summary>
	public abstract class XNAComponentBase : IXNAComponent {
		public event EventHandler OnFocusChanged;

		public IXNAContainer Parent {
			set { 
				//既に親が設定されていて、既存の親の子リストにまだ自分が含まれている
				if(parent != null && value != null) {
					for(int i=0; i<parent.Count; i++) {
						IXNAComponent c = parent[i];
						if(c.Equals(this)) {
							throw new InvalidOperationException();
						}
					}
				}
				this.parent = value; 
			} 
			get { return parent; }
		}

		public XNAPoint Point {
			set {
				if(point != null) {
					point.OnLocationChanged -= OnLocationChanged;
				}
				this.point = value;
				point.OnLocationChanged += OnLocationChanged;
				DoRevalidate();
			} 
			get { return point; }
		}

		public XNASize Size {
			set { 
				if(size != null) {
					size.OnSizeChanged -= OnSizeChanged;
				}
				this.size = value;
				size.OnSizeChanged += OnSizeChanged;
				DoRevalidate();
			} 
			get { return size; }
		}

		public XNASize MinimumSize {
			set {
				if(minimumSize != null) {
					minimumSize.OnSizeChanged -= OnSizeChanged;
				}
				this.minimumSize = value;
				minimumSize.OnSizeChanged += OnSizeChanged;
				DoRevalidate();
			}
			get { return minimumSize; }
		}

		public XNASize PreferredSize {
			set { 
				if(preferredSize != null) {
					preferredSize.OnSizeChanged -= OnSizeChanged;
				}
				this.preferredSize = value;
				preferredSize.OnSizeChanged += OnSizeChanged;
				if(IsDynamicLayout) {
					this.Size = value;
				}
				DoRevalidate();
			} 
			get { return preferredSize; }
		}

		public XNASize MaximumSize {
			set { 
				if(maximumSize != null) {
					maximumSize.OnSizeChanged -= OnSizeChanged;
				}
				this.maximumSize = value;
				maximumSize.OnSizeChanged += OnSizeChanged;
				DoRevalidate();
			} 
			get { return maximumSize; }
		}

		public Color Foreground {
			set { this.foreground = value; }
			get { return foreground; }
		}

		public Color Background {
			set { this.background = value; }
			get { return background; }
		}

		public XNARectangle Bounds {
			get { return new XNARectangle(Point, Size); }
			set { Point = value.Point; Size = value.Size; }
		}

		public bool IsOpaque {
			set { this.isOpaque = value; }
			get { return isOpaque; }
		}

		public bool HasFocus {
			set { 
				this.hasFocus = value;
				OnFocusChanged?.Invoke(this, EventArgs.Empty);
			}
			get { return hasFocus; }
		}

		public bool IsValid {
			protected set; get;
		}
		
		/// <summary>
		/// コンポーネントの推奨サイズが変更されたときに、その推奨サイズをそのまま現在のサイズに設定する場合はtrueにします。
		/// デフォルトではfalseです。
		/// </summary>
		public bool IsDynamicLayout {
			set; get;
		}

		/// <summary>
		/// 現在マウスポインタがコンポーネントに載っているかどうかを返します.
		/// </summary>
		public bool IsMount {
			get {
				MouseState mouseState = Mouse.GetState();
				Rectangle bounds = Bounds.ToRectangle();
				return bounds.Contains(mouseState.X, mouseState.Y);
			}
		}

		private IXNAContainer parent;
		private XNAPoint point;
		private XNASize size;
		private XNASize minimumSize;
		private XNASize preferredSize;
		private XNASize maximumSize;
		private Color foreground;
		private Color background;
		private bool isOpaque;
		private bool hasFocus;
		

		public XNAComponentBase() {
			this.IsValid = false;
			this.Point = new XNAPoint(0, 0);
			this.Size = new XNASize();
			this.MinimumSize = new XNASize();
			this.PreferredSize = new XNASize();
			this.MaximumSize = new XNASize();
			this.Foreground = Color.White;
			this.Background = Color.Black;
			this.IsOpaque = false;
			this.IsDynamicLayout = false;
		}

		public override string ToString() {
			float x = Point.X;
			float y = Point.Y;
			float w = Size.Width;
			float h = Size.Height;
			return "x=" + x + ", y=" + y + ", w=" + w + ", h=" + h;
		}

		public abstract void Update(GameTime time, KeyboardState keyState, MouseState mouseState);

		public abstract void Draw(Canvas canvas);
		
		public virtual void Invalidate() {
			this.IsValid = false;
			IXNAContainer parent = Parent;
			if(parent == null) {
				return;
			}
			parent.Invalidate();
		}

		public virtual void Validate() {
			this.IsValid = true;
		}

		public virtual void Revalidate() {
			Invalidate();
			GetValidRoot()?.LazyValidate(this);
		}

		private void DoRevalidate() {
			Revalidate();
		}

		/// <summary>
		/// このコンテナより上位の最も近い有効なるーとを返します.
		/// </summary>
		/// <returns></returns>
		protected IValidRoot GetValidRoot() {
			IXNAComponent r = this;
			do {
				r = r.Parent;
			} while(!(r is IValidRoot) && r != null);
			return r == null ? null : (IValidRoot)r;
		}
		
		//
		//イベントハンドラ
		//

		/// <summary>
		/// XPointのプロパティが変更されることによって座標が間接的に変更されたとき、レイアウトを更新します.<br>
		/// XPointそのものの参照が変更された場合はプロパティで同様の処理が行われます。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLocationChanged(object sender, EventArgs e) {
			DoRevalidate();
		}

		/// <summary>
		/// XSizeのプロパティが変更されることによって大きさが間接的に変更されたとき、レイアウトを更新します.<br>
		/// XSizeそのものの参照が変更された場合はプロパティで同様の処理が行われます。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSizeChanged(object sender, EventArgs e) {
			DoRevalidate();
		}
	}
}
