using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI {
	/// <summary>
	/// 他のコンポーネントを包含出来るコンテナです.
	/// </summary>
	public interface IXNAContainer : IXNAComponent {
		/// <summary>
		/// このコンポーネントの配置法則.
		/// </summary>
		ILayoutManager LayoutManager {
			set; get;
		}

		/// <summary>
		/// このコンポーネントの直下のコンポーネントの数を返します.
		/// </summary>
		int Count {
			get;
		}
		
		/// <summary>
		/// 指定位置のコンポーネントを返します.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		IXNAComponent this [int index] {
			get;
		}

		/// <summary>
		/// 制約に従ってコンポーネントを配置します.
		/// </summary>
		/// <param name="component"></param>
		void Add(IXNAComponent component, ILayoutConstraints c);

		/// <summary>
		/// 制約のないレイアウトでのみ使用可能です.<br>
		/// nullで同名メソッドを呼び出します。
		/// </summary>
		/// <param name="component"></param>
		void Add(IXNAComponent component);

		/// <summary>
		/// 指定のコンポーネントを削除します.
		/// </summary>
		/// <param name="component"></param>
		void Remove(IXNAComponent component);

		/// <summary>
		/// 指定位置のコンポーネントを削除します.
		/// </summary>
		/// <param name="index"></param>
		void RemoveAt(int index);

		/// <summary>
		/// 全てのコンポーネントを削除します.
		/// </summary>
		void RemoveAll();
	}
}
