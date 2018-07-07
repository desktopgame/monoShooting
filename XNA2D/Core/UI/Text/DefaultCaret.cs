using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Text {
	/// <summary>
	/// Caretのデフォルト実装です.
	/// </summary>
	public class DefaultCaret : Caret {
		public event EventHandler OnCaretUpdate;

		public int Position {
			set {
				this.position = value;
				OnCaretUpdate?.Invoke(this, EventArgs.Empty);
			}
			get { return position; }
		}

		private int position;

		public DefaultCaret(int position) {
			this.Position = position;
		}

		public DefaultCaret() : this(0) {
		}
	}
}
