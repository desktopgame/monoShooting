using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace XNA2D.Core.UI {
	
	/// <summary>
	/// 文字列で制約を表す実装です.
	/// </summary>
	public class StringConstraints : ILayoutConstraints {
		/// <summary>
		/// 判別するための文字列.
		/// </summary>
		public string Value {
			private set; get;
		}
		public StringConstraints(string value) {
			this.Value = value;
		}

		public override int GetHashCode() {
			return Value.GetHashCode();
		}

		public override bool Equals(object obj) {
			if(!(obj is StringConstraints)) {
				return false;
			}
			StringConstraints sc = (StringConstraints)obj;
			return sc.Value.Equals(Value);
		}
	}
	
	/// <summary>
	/// 方角でレイアウトするクラスです.
	/// </summary>
	public class BorderLayout : ILayoutManager {
		public static readonly ILayoutConstraints North = new StringConstraints("NORTH");
		public static readonly ILayoutConstraints South = new StringConstraints("SOUTH");
		public static readonly ILayoutConstraints Center = new StringConstraints("CENTER");
		public static readonly ILayoutConstraints East = new StringConstraints("EAST");
		public static readonly ILayoutConstraints West = new StringConstraints("WEST");
		private static readonly List<ILayoutConstraints> Directions = new List<ILayoutConstraints>{ North , South, Center, East, West};

		private Dictionary<ILayoutConstraints, IXNAComponent> components;

		private IXNAComponent this [ILayoutConstraints key] {
			get { return components.GetOrDefault(key); }
		}

		public BorderLayout() {
			this.components = new Dictionary<ILayoutConstraints, IXNAComponent>();
		}

		public void AddLayout(IXNAContainer parent, IXNAComponent component, ILayoutConstraints c) {
			if(!Directions.Contains(c)) {
				throw new ArgumentException();
			}
			components[c] = component;
		}

		public void RemoveLayout(IXNAContainer parent, IXNAComponent component) {
			foreach(ILayoutConstraints lc in components.Keys) {
				if(components[lc].Equals(component)) {
					components.Remove(lc);
					break;
				}
			}
		}

		public void InvalidateLayout(IXNAContainer container) {
		}

		public void LayoutContainer(IXNAContainer container) {
			foreach(ILayoutConstraints lc in components.Keys) {
				LayoutContainer(container, components[lc], lc);
			}
		}

		protected virtual void LayoutContainer(IXNAContainer container, IXNAComponent component, ILayoutConstraints lc) {
			component.Size = component.PreferredSize;
			if(lc.Equals(North)) {
				LayoutContainerNorth(container, component);
			} else if(lc.Equals(South)) {
				LayoutContainerSouth(container, component);
			} else if(lc.Equals(Center)) {
				LayoutContainerCenter(container, component);
			} else if(lc.Equals(West)) {
				LayoutContainerWest(container, component);
			} else if(lc.Equals(East)) {
				LayoutContainerEast(container, component);
			}
		}

		protected virtual void LayoutContainerNorth(IXNAContainer container, IXNAComponent component) {
			component.Point.X = container.Point.X + ((container.Size.Width - component.Size.Width) / 2);
			component.Point.Y = container.Point.Y;
		}

		protected virtual void LayoutContainerSouth(IXNAContainer container, IXNAComponent component) {
			IXNAComponent north = this[North];
			IXNAComponent middle = Get(West, Center, East);
			float baseY = (container.Point.Y + container.Size.Height) - component.Size.Height;
			float calcY = (GetY(north) + GetHeight(north)) + GetHeight(middle);
			component.Point.X = container.Point.X + ((container.Size.Width - component.Size.Width) / 2);
			component.Point.Y = Math.Max(baseY, calcY);
		}

		protected virtual void LayoutContainerCenter(IXNAContainer container, IXNAComponent component) {
			IXNAComponent north = this[North];
			IXNAComponent left = this[West];
			IXNAComponent right = this[East];
			float rightX = container.Point.X + container.Size.Width;
			float bottomY = container.Point.Y + container.Size.Height;
			float baseX = container.Point.X + ((container.Size.Width - component.Size.Width) / 2);
			float baseY = container.Point.Y + ((container.Size.Height - component.Size.Height) / 2);
			float calcX = GetX(left) + GetWidth(left);
			float calcY = (GetX(north) + GetHeight(north));
			component.Point.X = Math.Max(baseX, calcX);
			component.Point.Y = Math.Max(baseY, calcY);
		}

		protected virtual void LayoutContainerWest(IXNAContainer container, IXNAComponent component) {
			IXNAComponent north = this[North];
			float baseY = container.Point.Y + ((container.Size.Height - component.Size.Height) / 2);
			float calcY = (GetY(north) + GetHeight(north));
			component.Point.X = container.Point.X;
			component.Point.Y = Math.Max(baseY, calcY);
		}

		protected virtual void LayoutContainerEast(IXNAContainer container, IXNAComponent component) {
			IXNAComponent north = this[North];
			IXNAComponent left = Get(Center, West);
			float baseX = (container.Point.X + container.Size.Width) - component.Size.Width;
			float baseY = container.Point.Y + ((container.Size.Height - component.Size.Height) / 2);
			float calcX = (GetX(left) + GetWidth(left));
			float calcY = (GetY(north) + GetHeight(north));
			component.Point.X = Math.Max(baseX, calcY);
			component.Point.Y = Math.Max(baseY, calcY);
		}

		public XNASize CalculateSize(IXNAContainer container) {
			XNASize top = GetSize(North);
			XNASize left = GetSize(West);
			XNASize center = GetSize(Center);
			XNASize right = GetSize(East);
			XNASize bottom = GetSize(South);
			XNASize middle = new XNASize(left.Width + center.Width + right.Width, 0);
			XNASize ret = new XNASize();
			ret.Height += top.Height;
			ret.Height += MaxHeight(left, center, right);
			ret.Height += bottom.Height;
			ret.Width += MaxWidth(top, middle, bottom);
			return ret;
		}

		private float GetX(IXNAComponent c) {
			return c == null ? 0 : c.Point.X;
		}

		private float GetY(IXNAComponent c) {
			return c == null ? 0 : c.Point.Y;
		}

		private float GetWidth(IXNAComponent c) {
			return c == null ? 0 : c.Size.Width;
		}

		private float GetHeight(IXNAComponent c) {
			return c == null ? 0 : c.Size.Height;
		}

		private XNASize GetSize(ILayoutConstraints con) {
			IXNAComponent c;
			return components.TryGetValue(con, out c) ? c.Size : new XNASize(0, 0);
		}

		private IXNAComponent Get(params ILayoutConstraints[] cons) {
			IXNAComponent ret = null;
			Array.ForEach(cons, value => {
				IXNAComponent c = this[value];
				if(c == null || ret != null) {
					return;
				}
				ret = c;
			});
			return ret;
		}

		private float MaxWidth(params XNASize[] size) {
			float w = 0;
			Array.ForEach(size, elem => {
				if(w < elem.Width) {
					w = elem.Width;
				}
			});
			return w;
		}

		private float MaxHeight(params XNASize[] size) {
			float h = 0;
			Array.ForEach(size, elem => {
				if(h < elem.Height) {
					h = elem.Height;
				}
			});
			return h;
		}
	}
}
