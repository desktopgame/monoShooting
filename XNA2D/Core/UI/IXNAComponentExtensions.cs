using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI;

namespace XNA2D.Core.UI {
	/// <summary>
	/// IXNAComponent 型の拡張メソッドを管理するクラス
	/// </summary>
	public static class IXNAComponentExtensions {
		/// <summary>
		/// キャンバスに変換して描画を行います.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="batch"></param>
		public static void Draw(this IXNAComponent self, SpriteBatch batch) {
			self.Draw(DrawManager.GetInstance().GetCanvas(batch));
		}

		/// <summary>
		/// 入力状態を取得して処理を行います.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="time"></param>
		public static void Update(this IXNAComponent self, GameTime time) {
			self.Update(time, Keyboard.GetState(), Mouse.GetState());
		}
	}
}
