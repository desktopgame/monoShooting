using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Activity {
	/// <summary>
	/// イベントを順番に処理するためのキューです.
	/// 
	/// MT環境でのset/getの衝突を避けるために用意されたクラスです。
	/// ExGameでは毎フレーム一つだけアクションを取り出して実行します。
	/// </summary>
	public class EventQueue {
		private Queue<Action> q;

		/// <summary>
		/// キューが空であるかどうかを返します.
		/// </summary>
		public bool IsEmpty {
			get { return q.Count == 0; }
		}

		public EventQueue() {
			this.q = new Queue<Action>();
		}

		/// <summary>
		/// 処理を予約します.
		/// </summary>
		/// <param name="e"></param>
		public void PostEvent(Action e) {
			q.Enqueue(e);
		}

		/// <summary>
		/// 最初に予約されたアクションをキューから除去して返します.
		/// </summary>
		/// <returns></returns>
		public Action PollEvent() {
			return q.Dequeue();
		}
	}
}
