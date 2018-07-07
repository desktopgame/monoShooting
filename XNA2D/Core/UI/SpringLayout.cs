using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI;

namespace XNA2D.Core.UI {

	public class SpringConstraints : ILayoutConstraints {
	}

	/// <summary>
	/// ほかのコンポーネントとの間隔でレイアウトを行う実装です.
	/// </summary>
	public class SpringLayout : ILayoutManager {
		public void AddLayout(IXNAContainer parent, IXNAComponent component, ILayoutConstraints c) {
		}

		public void RemoveLayout(IXNAContainer parent, IXNAComponent component) {
		}
		
		public void InvalidateLayout(IXNAContainer container) {
		}

		public void LayoutContainer(IXNAContainer container) {
		}

		public XNASize CalculateSize(IXNAContainer container) {
			return null;
		}
	}
}
