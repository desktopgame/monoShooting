using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Activity {
	/// <summary>
	/// ITransitionInfoのデフォルト実装です.
	/// </summary>
	public class DefaultTransitionInfo : ITransitionInfo {
		/// <summary>
		/// キーと値を紐づけるプロパティです.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public object this [object key] {
			set {
				dictionary[key] = value;
			}
			get {
				return dictionary[key];
			}
		}

		private Dictionary<object, object> dictionary;

		public DefaultTransitionInfo() {
			this.dictionary = new Dictionary<object, object>();
		}

		public DefaultTransitionInfo SetValue(object key, object value) {
			dictionary[key] = value;
			return this;
		}
		
		public bool TryGetValue(object key, out object value) {
			return dictionary.TryGetValue(key, out value);
		}

		public bool ContainsKey(object key) {
			return dictionary.ContainsKey(key);
		}
		
		public bool ContainsValue(object value) {
			return dictionary.ContainsValue(value);
		}
	}
}
