using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Text {
	/// <summary>
	/// Documentの基底クラスです.
	/// </summary>
	public class AbstractDocument : Document {
		public int Length {
			get { return sb.Length; }
		}

		public event InsertHandler OnInsertUpdate;
		public event RemoveHandler OnRemoveUpdate;


		private StringBuilder sb;


		public AbstractDocument() {
			this.sb = new StringBuilder();
		}

		public string GetText(int offset, int length) {
			return sb.ToString().Substring(offset, length);
		}

		public void InsertString(int offset, string text) {
			sb.Insert(offset, text);
			OnInsertUpdate?.Invoke(this, new DocumentEventArgs(offset, text.Length));
		}

		public void RemoveString(int offset, int length) {
			sb.Remove(offset, length);
			OnRemoveUpdate?.Invoke(this, new DocumentEventArgs(offset, length));
		}
	}
}
