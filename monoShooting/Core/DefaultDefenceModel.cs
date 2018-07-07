using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bean.Core;

namespace Shoot.Core {
	/// <summary>
	/// DefenceModelのデフォルト実装です.
	/// </summary>
	public class DefaultDefenceModel : DefenseModel {
		public event PropertyChangeHandler<float> OnDefenceMaximumChanged;
		public event PropertyChangeHandler<float> OnDefenceValueChanged;

		public float Maximum {
			private set {
				float old = this.maximum;
				this.maximum = value;
				OnDefenceMaximumChanged?.Invoke(this, new PropertyChangeEventArgs<float>(old, value));
			}
			get { return maximum; }
		}

		public float Value {
			set {
				value = Math.Min(Maximum, Math.Max(0, value));
				float old = this.value;
				this.value = value;
				OnDefenceValueChanged?.Invoke(this, new PropertyChangeEventArgs<float>(old, value));
			}
			get { return value; }
		}

		private float maximum;
		private float value;


		public DefaultDefenceModel(float max) {
			this.Maximum = max;
			this.Value = max;
		}
	}
}
