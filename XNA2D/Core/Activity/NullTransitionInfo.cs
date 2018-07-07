using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Activity {
	/// <summary>
	/// キーや値を一切保持しない実装です.
	/// </summary>
	public class NullTransitionInfo : ITransitionInfo {
		/// <summary>
		/// 唯一のインスタンスです.
		/// </summary>
		public static readonly ITransitionInfo INSTANCE = new NullTransitionInfo();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <exception cref="NotImplementedException">常にスローします.</exception>
		/// <returns></returns>
		public object this[object key] {
			get {
				throw new NotImplementedException();
			}
		}

		private NullTransitionInfo() {
		}

		public bool ContainsKey(object key) {
			return false;
		}

		public bool ContainsValue(object value) {
			return false;
		}

		public bool TryGetValue(object key, out object value) {
			value = null;
			return false;
		}
	}
}
