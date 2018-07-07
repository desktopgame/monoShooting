using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Utils {
	/// <summary>
	/// Dictionary 型の拡張メソッドを管理するクラス
	/// <a href="http://baba-s.hatenablog.com/entry/2014/06/27/203952">【C#】Dictionary型の指定したキーに値が存在しない場合はデフォルト値を返す拡張メソッド </a>
	/// </summary>
	public static class DictionaryExtensions {
		/// <summary>
		/// 指定したキーに関連付けられている値を取得します。
		/// キーが存在しない場合は既定値を返します
		/// </summary>
		public static TValue GetOrDefault<TKey, TValue>(
			this Dictionary<TKey, TValue> self,
			TKey key,
			TValue defaultValue = default(TValue)) {
			TValue value;
//			return self.ContainsKey(key) ? self[key] : defaultValue;
			return self.TryGetValue(key, out value) ? value : defaultValue;
		}
	}
}
