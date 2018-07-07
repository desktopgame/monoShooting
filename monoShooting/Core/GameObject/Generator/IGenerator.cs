using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core.GameObject.Generator {
	/// <summary>
	/// 定期的に敵を生成するインターフェイスです.
	/// </summary>
	public interface IGenerator {
		/// <summary>
		/// アイテムやら敵オブジェクトやらを生成します.<br>
		/// 何も生成しない場合は長さ0の配列を返します。
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="field"></param>
		/// <returns></returns>
		IGameObject[] Generate(GameTime gameTime, Field field);
	}
}
