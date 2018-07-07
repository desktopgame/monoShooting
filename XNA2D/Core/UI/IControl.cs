using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAActivity.Core.UI {
	/// <summary>
	/// ユーザからの入力を検出可能な何らかのデバイスです.
	/// </summary>
	public interface IControl {
		/// <summary>
		/// ユーザからの入力が検出されたならtrueを返します.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		bool IsDetect(PlayerIndex index);
	}
}
