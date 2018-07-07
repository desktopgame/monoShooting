using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace XNA2D.Core.Utils {
	/// <summary>
	/// 直列化可能なキーと値のセットです.
	/// </summary>
	[Serializable]
	public class SerializedDictionary {
		private string filePath;
		private Dictionary<string, object> dictionary;

		public SerializedDictionary(string filePath) {
			this.filePath = filePath;
			this.dictionary = new Dictionary<string, object>();
		}

		/// <summary>
		/// 辞書へ保存された情報をファイルへ書き込みます.
		/// </summary>
		public void Write() {
			Serializer.Serialize(filePath, dictionary);
		}

		/// <summary>
		/// ファイルへ保存された情報を辞書へ書き込みます.<br>
		/// この呼び出し以前に辞書へ保存されたデータはすべてクリアしてから書き込みます。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public void Read() {
			dictionary.Clear();
			Dictionary<string, object> d = Serializer.Deserialize<Dictionary<string, object>>(filePath);
			foreach(string key in d.Keys) {
				this[key] = d[key];
			}
		}

		/// <summary>
		/// 全てのキーを返します.
		/// </summary>
		public Dictionary<string, object>.KeyCollection Keys {
			get { return dictionary.Keys; }
		}

		/// <summary>
		/// キーと値を紐づけます.<br>
		/// 書き込みのとき、値がともにシリアライズ可能である必要があります(たぶん)
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public object this [string key] {
			set {
			//	FIXME:値にSerializableアノテーション?がついているか検査
			//	Type t = value.GetType();
			//	object[] attributes = t.GetCustomAttributes(false);
			//	foreach(object attr in attributes) {
			//	}
				this.dictionary[key] = value;
			}
			get {
				return dictionary[key];
			}
		}
	}
}
