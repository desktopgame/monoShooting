using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace XNA2D.Core.Utils {
	/// <summary>
	/// 直列化に関するユーティリティクラスです.
	/// </summary>
	public static class Serializer {
		/// <summary>
		/// 指定のオブジェクトを直列化します.
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="o"></param>
		public static void Serialize(string filePath, object o) {
			using(Stream s = File.OpenWrite(filePath)) {
				BinaryFormatter f = new BinaryFormatter();
				f.Serialize(s, o);
			}
		}

		/// <summary>
		/// 指定のオブジェクトを復元します.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static T Deserialize<T>(string filePath) {
			using(Stream s = File.OpenRead(filePath)) {
				BinaryFormatter f = new BinaryFormatter();
				return (T)f.Deserialize(s);
			}
		}
	}
}
