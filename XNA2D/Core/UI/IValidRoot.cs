using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI {
	/// <summary>
	/// 検証の開始位置です.
	/// </summary>
	public interface IValidRoot : IXNAContainer {
		/// <summary>
		/// 対象のコンポーネントを次のイベントで検証します.
		/// </summary>
		/// <param name="c"></param>
		void LazyValidate(IXNAComponent c);
	}
}
