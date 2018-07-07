using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAActivity.Core.UI.YesNo {
	/// <summary>
	/// Yes, Noの決定を監視するリスナーです.
	/// </summary>
	/// <param name="e"></param>
	public delegate void YesNoHandler(YesNoEventArgs e);

	/// <summary>
	/// Yes, Noの決定を通知するイベントです.
	/// </summary>
	public class YesNoEventArgs : EventArgs {
		/// <summary>
		/// 決定された選択です.
		/// </summary>
		public YesNoEnum Option {
			private set; get;
		}

		public YesNoEventArgs(YesNoEnum option) : base() {
			this.Option = option;
		}
	}
}
