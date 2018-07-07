using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// IAnimationFrameのファクトリクラスです.<br>
	/// AnimationMediatorで同じ種類のアニメーションを並列して実行するために使用されます。
	/// </summary>
	public interface IAnimationFrameFactory {
		/// <summary>
		/// IAnimationFrameを生成します.<br>
		/// 必要に応じて座標や色を渡すことが出来ます
		/// </summary>
		/// <param name="args">生成に必要なパラメータ 実装によっては無視されます。</param>
		/// <returns></returns>
		IAnimationFrame Create(params object[] args);
	}
}
