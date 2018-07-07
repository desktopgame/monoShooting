using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Text {

	/// <summary>
	/// テキストの追加を監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void InsertHandler(object sender, DocumentEventArgs e);

	/// <summary>
	/// テキストの削除を監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void RemoveHandler(object sender, DocumentEventArgs e);

	/// <summary>
	/// テキストを保持するモデルです.
	/// </summary>
	public interface Document {

		/// <summary>
		/// テキストの追記を監視するリスナーのリストです.
		/// </summary>
		event InsertHandler OnInsertUpdate;

		/// <summary>
		/// テキストの削除を監視するリスナーのリストです.
		/// </summary>
		event RemoveHandler OnRemoveUpdate;

		/// <summary>
		/// このドキュメントの長さを返します.
		/// </summary>
		int Length {
			get;
		}

		/// <summary>
		/// 指定位置にテキストを挿入します.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="text"></param>
		void InsertString(int offset, string text);

		/// <summary>
		/// 指定位置から後ろ方向へテキストを削除します.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		void RemoveString(int offset, int length);

		/// <summary>
		/// 指定位置から指定の長さ分テキストを取得します.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		string GetText(int offset, int length);
	}
}
