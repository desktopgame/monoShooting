using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Button {
	/// <summary>
	/// ボタンの押し込みを監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ActionHandler(object sender, ActionEventArgs e);

	/// <summary>
	/// ボタンが押し込まれたことを通知するイベントです.
	/// </summary>
	public class ActionEventArgs : EventArgs {
		/// <summary>
		/// このアクションの種類を表す識別子です.
		/// </summary>
		public string ActionCommand {
			private set; get;
		}

		public ActionEventArgs(string actionCommand) {
			this.ActionCommand = actionCommand;
		}
	}
}
