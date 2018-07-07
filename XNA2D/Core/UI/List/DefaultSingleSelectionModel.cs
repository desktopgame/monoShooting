using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.List {
	/// <summary>
	/// SingleSelectionModelのデフォルト実装です.
	/// </summary>
	public class DefaultSingleSelectionModel : SingleSelectionModel {
		public event EventHandler OnStateChanged;

		public int SelectedIndex {
			set {
				this.selectedIndex = value;
				OnStateChanged?.Invoke(this, EventArgs.Empty);
			}
			get { return selectedIndex; }
		}

		private int selectedIndex;


		public DefaultSingleSelectionModel(int index) {
			this.SelectedIndex = index;
		}

		public DefaultSingleSelectionModel() : this(-1) {
		}
	}
}
