using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Button {
	/// <summary>
	/// ButtonModelのデフォルト実装です.
	/// </summary>
	public class DefaultButtonModel : ButtonModel {
		public event EventHandler OnUpdateRendering;
		public event ActionHandler OnActionPerformed;

		public string ActionCommand {
			set; get;
		}

		public bool IsEnabled {
			set {
				this.isEnabled = value;
				OnUpdateRendering?.Invoke(this, EventArgs.Empty);
			}
			get { return isEnabled; }
		}

		public bool IsPressed {
			set {
				this.isPressed = value;
				OnUpdateRendering?.Invoke(this, EventArgs.Empty);
			}
			get { return isPressed; }
		}

		public bool IsRollover {
			set {
				this.isRollover = value;
				OnUpdateRendering?.Invoke(this, EventArgs.Empty);
			}
			get { return isRollover; }
		}

		public bool IsArmed {
			set {
				this.isArmed = value;
				OnUpdateRendering?.Invoke(this, EventArgs.Empty);
				if(!value) OnActionPerformed?.Invoke(this, new ActionEventArgs(ActionCommand));
			}
			get { return isArmed; }
		}

		private bool isEnabled;
		private bool isPressed;
		private bool isRollover;
		private bool isArmed;
	}
}
