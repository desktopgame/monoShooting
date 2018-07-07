using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Text {
	/// <summary>
	/// カレット位置を表すモデルです.
	/// </summary>
	public interface Caret {
		/// <summary>
		/// カレット位置の変更を監視するリスナーのリストです.
		/// </summary>
		event EventHandler OnCaretUpdate;

		/// <summary>
		/// カレット位置.<br>
		/// 0 ~ Document#Length - 1内である必要があります。
		/// </summary>
		int Position {
			set; get;
		}
	}
}
