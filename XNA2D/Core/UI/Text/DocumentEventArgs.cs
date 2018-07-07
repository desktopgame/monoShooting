using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Text {
	/// <summary>
	/// ドキュメントの変更を通知するイベントです.
	/// </summary>
	public class DocumentEventArgs {
		/// <summary>
		/// 変更の位置.
		/// </summary>
		public int Offset {
			get;
		}

		/// <summary>
		/// 変更の長さ.
		/// </summary>
		public int Length {
			get;
		}

		public DocumentEventArgs(int offset, int length) {
			this.Offset = offset;
			this.Length = length;
		}
	}
}
