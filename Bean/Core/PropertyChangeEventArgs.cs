using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean.Core {

	/// <summary>
	/// プロパティの変更を監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void PropertyChangeHandler<T>(object sender, PropertyChangeEventArgs<T> e);

	/// <summary>
	/// プロパティの変更を通知するイベントです.
	/// </summary>
	public class PropertyChangeEventArgs<T> : EventArgs {
		/// <summary>
		/// 変更前の値.
		/// </summary>
		public T OldValue {
			private set; get;
		}

		/// <summary>
		/// 変更後の値.
		/// </summary>
		public T NewValue {
			private set; get;
		}

		public PropertyChangeEventArgs(T oldValue, T newValue) {
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}
	}
}
