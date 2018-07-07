using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Utils {
	/// <summary>
	/// 途中で中断可能なアクションです.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="index"></param>
	/// <param name="value"></param>
	/// <returns>中断するならtrue</returns>
	public delegate bool ListAction<T>(int index, T value);

	/// <summary>
	/// 途中で中断可能なアクションです.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="row"></param>
	/// <param name="column"></param>
	/// <param name="value"></param>
	/// <returns>中断するならtrue</returns>
	public delegate bool TableAction<T>(int row, int column, T value);

	/// <summary>
	/// 途中で中断可能なアクションです.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="parent"></param>
	/// <param name="depth"></param>
	/// <param name="index"></param>
	/// <param name="value"></param>
	/// <returns>中断するならtrue</returns>
	public delegate bool TreeAction<T>(T parent, int depth, int index, T value);
}
