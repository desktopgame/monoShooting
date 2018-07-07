using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Activity {
	/// <summary>
	/// 画面遷移を行う際に前の画面から次の画面へ渡すことができる読み取り専用のキーと値のセットです.
	/// </summary>
	public interface ITransitionInfo {
		/// <summary>
		/// キーと値を紐づけるプロパティです.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		object this [object key] {
			get;
		}

		/// <summary>
		/// 指定のキーに紐づけられた値をvalueへ格納します.<br>
		/// キーが存在しなかった場合はfalseを返します。
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		bool TryGetValue(object key, out object value);

		/// <summary>
		/// 指定のキーが含まれるどうかを返します.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool ContainsKey(object key);

		/// <summary>
		/// 指定の値が含まれるかどうかを返します.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		bool ContainsValue(object value);
	}
}
