using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Utils {
	/// <summary>
	/// ミリ秒単位で時間を計測するクラスです.
	/// 
	/// 非推奨です.
	/// 待機画面からゲーム画面に復帰したときに差分が大きくなるので、正しく時間を計れません。
	/// FrameTimerを代替として利用します。
	/// </summary>
	public class Schedule {
		/// <summary>
		/// 指定時間の経過を監視するリスナーのリストです.
		/// </summary>
		public EventHandler OnElapsed;

		/// <summary>
		/// 待機する経過時間.
		/// </summary>
		public double Interval {
			set; get;
		}

		private double period;
		private double timeOffset;

		public Schedule(double interval) {
			this.period = -1d;
			this.timeOffset = 0d;
			this.Interval = interval;
		}

		/// <summary>
		/// 1秒で初期化されます.
		/// </summary>
		public Schedule() : this(1000d) {
		}

		/// <summary>
		/// 時間の差を計算して待機時間が経過したかどうかを返します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public bool Update(GameTime gameTime) {
			double now = gameTime.TotalGameTime.TotalMilliseconds;
			double diff = period == -1 ? int.MaxValue : now - period;
			this.period = now;
			//必要な時間が経過したら
			if((timeOffset += diff) > Interval) {
				timeOffset = 0;
				OnElapsed?.Invoke(this, EventArgs.Empty);
				return true;
			}
			return false;
		}

		/// <summary>
		/// 最初の呼び出しの場合にもtrueを返します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public bool UpdateFirst(GameTime gameTime) {
			return Update(gameTime) || period == -1d;
		}

		/// <summary>
		/// クリアします.
		/// </summary>
		/// <param name="gameTime"></param>
		public void Clear(GameTime gameTime) {
			this.period = gameTime.TotalGameTime.TotalMilliseconds;
			this.timeOffset = 0;
		}
	}
}
