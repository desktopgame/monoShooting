using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Animation {

	/// <summary>
	/// アニメーションの更新を監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void AnimationHandler(object sender, AnimationEventArgs e);

	/// <summary>
	/// アニメーションの再生/終了/更新を通知するイベントです.
	/// </summary>
	public class AnimationEventArgs : EventArgs {
		/// <summary>
		/// イベントの種類
		/// </summary>
		public enum Type {
			Begin, End, Update,
		}
		/// <summary>
		/// 現在のアニメーション.
		/// </summary>
		public string Name {
			private set; get;
		}

		/// <summary>
		/// イベントの種類.
		/// </summary>
		public Type EventType {
			private set; get;
		}

		public AnimationEventArgs(string name, Type eventType) {
			this.Name = name;
			this.EventType = eventType;
		}
	}
}
