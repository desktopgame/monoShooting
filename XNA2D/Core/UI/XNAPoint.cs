using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI {
	/// <summary>
	/// X座標とY座標を表します.<br>
	/// イベント通知機構を実装します。
	/// </summary>
	public class XNAPoint {
		/// <summary>
		/// 位置の変更を監視するリスナーのリストです.
		/// </summary>
		public event EventHandler OnLocationChanged;
		public float X {
			set {
				this.x = value;
				OnLocationChanged?.Invoke(this, EventArgs.Empty);
			}
			get { return x; }
		}

		public float Y {
			set {
				this.y = value;
				OnLocationChanged?.Invoke(this, EventArgs.Empty);
			}
			get { return y; }
		}

		private float x;
		private float y;

		public XNAPoint(float x, float y) {
			this.X = x;
			this.Y = y;
		}

		public XNAPoint(XNAPoint xp) : this(xp.X, xp.Y) {
		}

		/// <summary>
		/// 0, 0で初期化します.
		/// </summary>
		public XNAPoint() : this(0, 0) {
		}

		/// <summary>
		/// 座標を用いて同一かどうかを判別します.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj) {
			if(!(obj is XNAPoint)) {
				return false;
			}
			XNAPoint other = (XNAPoint)obj;
			return X == other.X && Y == other.Y;
		}

		/// <summary>
		/// キャストして返します.
		/// </summary>
		/// <returns></returns>
		public int GetX() {
			return (int)X;
		}

		/// <summary>
		/// キャストして返します.
		/// </summary>
		/// <returns></returns>
		public int GetY() {
			return (int)Y;
		}

		/// <summary>
		/// XNAのベクタへ変換します.
		/// </summary>
		/// <returns></returns>
		public Vector2 ToVector2() {
			return new Vector2(X, Y);
		}
	}
}
