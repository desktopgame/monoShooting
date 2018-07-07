using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// アニメーションの描画実装を提供します.
	/// </summary>
	public interface IAnimationFrame {
		/// <summary>
		/// 次のフレームが存在するかどうかを返します.<br>
		/// このフラグがfalseになると、IAnimationFrame#Endが呼び出され、AnimationMediatorによって関連付けられた次のアニメーションがある場合はそれを開始します。
		/// </summary>
		bool HasNext {
			get;
		}

		/// <summary>
		/// アニメーションが開始すると呼ばれます.
		/// </summary>
		void Begin();

		/// <summary>
		/// フレームを更新します.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="gameTime"></param>
		void Update(int offset, GameTime gameTime);

		/// <summary>
		/// アニメーションの1フレームを描画します.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		void Draw(int offset, GameTime gameTime, SpriteBatch batch);

		/// <summary>
		/// アニメーションが終了すると呼ばれます.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="list">次に開始するアニメーションの名前</param>
		void End(int offset, List<string> list);
	}
}
