using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Table {
	/// <summary>
	/// 文字列で要素を表すヘッダです.
	/// </summary>
	public class DefaultHeader : Header {
		public object this[int index] {
			get { return elements[index];  }
		}

		public int Count {
			get { return elements.Length; }
		}

		private object[] elements;

		public DefaultHeader(params object[] elements) {
			this.elements = elements;
		}
	}
}
