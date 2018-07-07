using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAActivity.Core.Utils {
	/// <summary>
	/// フレーム単位で時間を計測します.
	/// </summary>
	public sealed class FrameTimer {
		public int Limit {
			set {
				this.limit = value;
				this.current = 0;
			} 
			get { return limit; }
		}

		private int current;
		private int limit;
		private static readonly int FRAME = 1000 / 60;

		public FrameTimer(int limit) {
			this.current = 0;
			this.Limit = limit;
		}

		/// <summary>
		/// ミリ秒単位をフレーム単位に修正して返します.
		/// </summary>
		/// <param name="mill"></param>
		/// <returns></returns>
		public static int MillToFrame(int mill) {
			return mill / FRAME;
		}

		/// <summary>
		/// 秒単位をフレーム単位に修正して返します.
		/// </summary>
		/// <param name="sec"></param>
		/// <returns></returns>
		public static int SecondToFrame(int sec) {
			int mill = sec * 1000;
			return mill / FRAME;
		}

		/// <summary>
		/// 指定のﾐﾘ秒単位の時間をフレーム単位に修正して生成します.
		/// </summary>
		/// <param name="mill"></param>
		/// <returns></returns>
		public static FrameTimer ForMill(int mill) {
			return new FrameTimer(MillToFrame(mill));
		}

		/// <summary>
		/// 指定の秒単位の時間をフレーム単位に修正して生成します.
		/// </summary>
		/// <param name="sec"></param>
		/// <returns></returns>
		public static FrameTimer ForSecond(int sec) {
			return new FrameTimer(SecondToFrame(sec));
		}

		/// <summary>
		/// タイマーをリセットします.
		/// </summary>
		public void Clear() {
			this.current = 0;
		}

		/// <summary>
		/// フレームを更新します。
		/// </summary>
		public FrameTimer Update() {
			if(current++ > Limit) {
				this.current = 0;
			}
			return this;
		}

		/// <summary>
		/// 指定回数フレームが経過したならtrueを返します
		/// </summary>
		/// <returns></returns>
		public bool IsElapsed() {
			return current >= Limit;
		}
	}
}
