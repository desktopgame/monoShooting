using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Utils {
	/// <summary>
	/// FPSを計測するためのクラスです.
	/// <a href="http://www7.plala.or.jp/kfb/program/framerate.html">フレームレートの計測</a>
	/// </summary>
	public class FPSManager {
		/// <summary>
		/// 一秒間に画面が更新される回数です.
		/// </summary>
		public float Value {
			set; get;
		}

		/// <summary>
		/// 描画に適した形式の文字列を返します.
		/// </summary>
		public string Text {
			get { return string.Format("FPS: {0:F2}", Value); }
		}

		private double period;
		private int frameCount;

		public FPSManager() {
			this.Value = 0f;
			this.period = 0d;
			this.frameCount = 0;
		}

		/// <summary>
		/// Game#Drawで呼び出します.
		/// </summary>
		/// <param name="gameTime"></param>
		public void Tick(GameTime gameTime) {
			this.frameCount++;
			double now = gameTime.TotalGameTime.TotalMilliseconds;
			if(now - period >= 1000) {
				this.Value = (frameCount * 1000) / (float)(now - period);
				this.period = now;
				this.frameCount = 0;
			}
		}
	}
}
