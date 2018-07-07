using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAActivity.Core.UI {
	/// <summary>
	/// Xbox, WindowsOS の入力を共通のインターフェイスで扱うクラスです.
	/// </summary>
	public class Handle : IControl {
		/// <summary>
		/// Windowsの←と、XBoxコントローラのDパッドの↑を関連付けます.
		/// </summary>
		public static readonly Handle LEFT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Left),
			(index, gamePad) => gamePad.DPad.Left == ButtonState.Pressed
		);

		/// <summary>
		/// Windowsの←と、XBoxコントローラのDパッドの→を関連付けます.
		/// </summary>
		public static readonly Handle RIGHT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Right),
			(index, gamePad) => gamePad.DPad.Right == ButtonState.Pressed
		);

		/// <summary>
		/// Windowsの↑と、XBoxコントローラのDパッドの↑を関連付けます.
		/// </summary>
		public static readonly Handle UP = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Up),
			(index, gamePad) => gamePad.DPad.Up == ButtonState.Pressed
		);

		/// <summary>
		/// Windowsの↓と、XBoxコントローラのDパッドの↓を関連付けます.
		/// </summary>
		public static readonly Handle DOWN = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Down),
			(index, gamePad) => gamePad.DPad.Down == ButtonState.Pressed
		);

		/// <summary>
		/// WindowsのEnterと、XBoxコントローラのStartボタンを関連付けます.
		/// </summary>
		public static readonly Handle ENTER = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Enter),
			(index, gamePad) => gamePad.Buttons.Start == ButtonState.Pressed
		);

		/// <summary>
		/// WindowsのEscapeと、XBoxコントローラのBackボタンを関連付けます.
		/// </summary>
		public static readonly Handle EXIT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Escape),
			(index, gamePad) => gamePad.Buttons.Back == ButtonState.Pressed
		);

		/// <summary>
		/// このハンドルで定義されるイベント(左へ移動とか)を検出するWindows向けの実装です.
		/// </summary>
		public IWindowsDetector Windows {
			private set; get;
		}

		/// <summary>
		/// このハンドルで定義されるイベント(左へ移動とか)を検出するXBox向けの実装です.
		/// </summary>
		public IXBoxDetector XBox {
			private set; get;
		}
		
		public Handle(IWindowsDetector windows, IXBoxDetector xbox) {
			this.Windows = windows;
			this.XBox = xbox;
		}

		public Handle(WindowsDetectorProvider windows, XBoxDetectorProvider xbox)
			: this(new WindowsDetectorDelegate(windows), new XBoxDetectorDelegate(xbox)) {
		}

		/// <summary>
		/// XBox, Windowsの両方で入力を検出します.
		/// どちらかで検出されたらtrueを返します.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public bool IsDetect(PlayerIndex index) {
			MouseState mouseState = Mouse.GetState();
			KeyboardState keyState = Keyboard.GetState();
			GamePadState gamePadState = GamePad.GetState(index);
			if(!gamePadState.IsConnected) {
				return Windows.IsDetect(index, mouseState, keyState);
			}
			return Windows.IsDetect(index, mouseState, keyState) || XBox.IsDetect(index, gamePadState);
		}

		/// <summary>
		/// 1Pで検出します.
		/// </summary>
		/// <returns></returns>
		public bool IsDetect() {
			return IsDetect(PlayerIndex.One);
		}
	}

	//
	//インターフェイス
	//
	
	/// <summary>
	/// Windows向けの入力検出インターフェイスです.
	/// </summary>
	public interface IWindowsDetector {
		bool IsDetect(PlayerIndex index, MouseState mouseState, KeyboardState keyState);
	}

	/// <summary>
	/// XBox向けの入力検出インターフェイスです.
	/// </summary>
	public interface IXBoxDetector {
		bool IsDetect(PlayerIndex index, GamePadState gamePadState);
	}

	//
	//実装(Win)
	//

	/// <summary>
	/// WindowsDetectorの実装を提供します.
	/// </summary>
	/// <param name="index"></param>
	/// <param name="mouseState"></param>
	/// <param name="keyState"></param>
	/// <returns></returns>
	public delegate bool WindowsDetectorProvider(PlayerIndex index, MouseState mouseState, KeyboardState keyState);

	/// <summary>
	/// WindowsDetectorをラムダで実装できるようにします.
	/// </summary>
	public class WindowsDetectorDelegate : IWindowsDetector {
		private WindowsDetectorProvider prov;

		public WindowsDetectorDelegate(WindowsDetectorProvider prov) {
			this.prov = prov;
		}

		public bool IsDetect(PlayerIndex index, MouseState mouseState, KeyboardState keyState) {
			return prov(index, mouseState, keyState);
		}
	}

	//
	//実装(XBox)
	//

	/// <summary>
	/// XBoxDetectorの実装を提供します.
	/// </summary>
	/// <param name="index"></param>
	/// <param name="gamePadState"></param>
	/// <returns></returns>
	public delegate bool XBoxDetectorProvider(PlayerIndex index, GamePadState gamePadState);

	/// <summary>
	/// XBoxDetectorをラムダで実装できるようにします.
	/// </summary>
	public class XBoxDetectorDelegate : IXBoxDetector {
		private XBoxDetectorProvider prov;

		public XBoxDetectorDelegate(XBoxDetectorProvider prov) {
			this.prov = prov;
		}

		public bool IsDetect(PlayerIndex index, GamePadState gamePadState) {
			return prov(index, gamePadState);
		}
	}
}
