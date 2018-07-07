using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Button {
	/// <summary>
	/// ボタンの押し込み状態やロールオーバーを保持するモデルです.<br>
	/// ビューはこれを参照してレンダリングを行います。
	/// </summary>
	public interface ButtonModel {
		/// <summary>
		/// レンダリングに関するプロパティの変更を監視するリスナーのリストです.
		/// </summary>
		event EventHandler OnUpdateRendering;

		/// <summary>
		/// アクションの実行を監視するリスナーのリストです.
		/// </summary>
		event ActionHandler OnActionPerformed;

		/// <summary>
		/// ボタンの押し込み状態.
		/// </summary>
		bool IsPressed {
			set; get;
		}

		/// <summary>
		/// ボタンに対するマウスの状態.
		/// </summary>
		bool IsRollover {
			set; get;
		}

		/// <summary>
		/// ボタンの有効/無効状態.
		/// </summary>
		bool IsEnabled {
			set; get;
		}

		/// <summary>
		/// ボタンの作動準備状態.
		/// </summary>
		bool IsArmed {
			set; get;
		}

		/// <summary>
		/// ボタンの識別子.
		/// </summary>
		string ActionCommand {
			set; get;
		}
	}
}
