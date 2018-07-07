using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNAActivity.Core.UI;

namespace XNA2D.Core.UI {
	/// <summary>
	/// キー入力を制御するコントローラです.<br>
	/// 押しっぱなしによるキーイベントの連続発生を抑えます.
	/// </summary>
	public class Detector {
		private static Detector instance;
		private Dictionary<Keys, IControl> store;
		private Dictionary<IControl, bool> isPressed;


		private Detector() {
			this.store = new Dictionary<Keys, IControl>();
			this.isPressed = new Dictionary<IControl, bool>();
		}

		/// <summary>
		/// 唯一のインスタンスを返します.
		/// </summary>
		/// <returns></returns>
		public static Detector GetInstance() {
			if(instance == null) {
				instance = new Detector();
			}
			return instance;
		}

		//
		//XBox向けの実装
		//

		/// <summary>
		/// Windows, XBoxからの入力を検出します.
		/// 直前にトリガーされていたらtrueを返しません.
		/// 一度トリガーをキャンセルしてから再トリガーすることによってtrueを返します.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="control"></param>
		/// <returns></returns>
		public bool IsDetect(PlayerIndex index, IControl control) {
			//まだ押されてない
			//if(!isPressed.ContainsKey(control)) {
			//	isPressed[control] = true;
			//	return s.IsKeyDown(control);
			//}
			bool pressed = isPressed[control];
			//押されてる
			if(control.IsDetect(index) && pressed) {
				return false;
			} else if(control.IsDetect(index) && !pressed) {
				isPressed[control] = true;
				return true;
			}
			isPressed[control] = false;
			return false;
		}

		/// <summary>
		/// 1Pとして入力を検出します.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		public bool IsDetect(IControl control) {
			if(!isPressed.ContainsKey(control)) {
				isPressed[control] = true;
				return control.IsDetect(PlayerIndex.One);
			}
			return IsDetect(PlayerIndex.One, control);
		}

		//
		//Windows向けの実装
		//

		/// <summary>
		/// 指定のキーが直前に入力されていたらtrueを返しません.
		/// 一度キーを離してから押し込むことによってtrueを返します.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		private bool IsDetect(PlayerIndex index, Keys key) {
			if(!store.ContainsKey(key)) {
				IControl control = new KeyControl(key);
				store[key] = control;
				isPressed[control] = control.IsDetect(index);
				return control.IsDetect(index);
			}
			return IsDetect(index, store[key]);
		}

		/// <summary>
		/// 1Pとして入力を検出します.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool IsDetect(Keys key) {
			return IsDetect(PlayerIndex.One, key);
		}
	}

	/// <summary>
	/// キーボードの入力を検出する実装です.
	/// </summary>
	internal class KeyControl : IControl {
		private Keys key;
		public KeyControl(Keys key) {
			this.key = key;
		}

		public bool IsDetect(PlayerIndex index) {
			return Keyboard.GetState().IsKeyDown(key);
		}
	}
}
