using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.RangeBar {
	/// <summary>
	/// RangeModelのデフォルト実装です.
	/// </summary>
	public class DefaultRangeModel : RangeModel {
		public float Maximum {
			get { return max; }
			set {
				this.max = value;
				if(Minimum > max) {
					Minimum = max;
				}
			}
		}

		public float Minimum {
			get { return min; }

			set {
				this.min = value;
				if(Maximum < min) {
					Maximum = min;
				}
			}
		}

		public float Value {
			get { return val; }

			set {
				if(value < min || value > max) {
					throw new ArgumentException("範囲外です：最小値=" + Minimum + ", 最大値=" + Maximum + ", 現在値=" + Value);
				}
				this.val = value;
			}
		}

		private float min;
		private float val;
		private float max;

		public DefaultRangeModel(float min, float val, float max) {
			this.Minimum = min;
			this.Maximum = max;
			this.Value = val;
		}

		public DefaultRangeModel() : this(0, 0, 100) {
		}
	}
}
