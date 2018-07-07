using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI {
	/// <summary>
	/// 制約に従ってコンテナにコンポーネントを並べるマネージャです.<br>
	/// 絶対座標による配置ではなく、比率や幅から動的に配置を行うインターフェイスです。
	/// </summary>
	public interface ILayoutManager {
		/// <summary>
		/// コンポーネントを追加します.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="component"></param>
		/// <param name="c"></param>
		void AddLayout(IXNAContainer parent, IXNAComponent component, ILayoutConstraints c);

		/// <summary>
		/// コンポーネントを削除します.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="component"></param>

		void RemoveLayout(IXNAContainer parent, IXNAComponent component);

		/// <summary>
		/// レイアウトが無効にされたので、キャッシュを破棄します.<br>
		/// レイアウトに関するキャッシュがない場合は何もしません。
		/// </summary>
		/// <param name="container"></param>
		void InvalidateLayout(IXNAContainer container);

		/// <summary>
		/// サイズや位置の変更によってレイアウトを更新します.
		/// </summary>
		/// <param name="container"></param>
		void LayoutContainer(IXNAContainer container);

		/// <summary>
		/// コンテナのサイズを計算します.
		/// </summary>
		/// <param name="container"></param>
		/// <returns></returns>
		XNASize CalculateSize(IXNAContainer container);
	}
}
