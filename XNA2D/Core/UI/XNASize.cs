using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI {
	/// <summary>
	/// 横幅と縦幅を表します.<br>
	/// イベント通知機構を実装します。
	/// </summary>
	public class XNASize {
		/// <summary>
		/// サイズの変更を監視するリスナーのリストです.
		/// </summary>
		public event EventHandler OnSizeChanged;
		
		public float Width {
			set {
				this.width = value;
				OnSizeChanged?.Invoke(this, EventArgs.Empty);
			} 
			get { return width; }
		}

		public float Height {
			set {
				this.height = value;
				OnSizeChanged?.Invoke(this, EventArgs.Empty);
			}
			get { return height; }
		}

		private float width;
		private float height;

		public XNASize(float width, float height) {
			this.Width = width;
			this.Height = height;
		}

		public XNASize(XNASize xs) : this(xs.Width, xs.Height) {
		}

		/// <summary>
		/// 全ての大きさを足します.
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static XNASize Sum(params XNASize[] size) {
			XNASize ret = new XNASize();
			Array.ForEach(size, elem => {
				ret.Width += elem.Width;
				ret.Height += elem.Height;
			});
			return ret;
		}
		
		/// <summary>
		/// 大きさを用いて同一かどうかを判別します.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj) {
			if(!(obj is XNASize)) {
				return false;
			}
			XNASize other = (XNASize)obj;
			return Width == other.Width && Height == other.Height;
		}

		/// <summary>
		/// 0, 0で初期化します.
		/// </summary>
		public XNASize() : this(0, 0) {
		}

		/// <summary>
		/// キャストして返します.
		/// </summary>
		/// <returns></returns>
		public int GetWidth() {
			return (int)Width;
		}

		/// <summary>
		/// キャストして返します.
		/// </summary>
		/// <returns></returns>
		public int GetHeight() {
			return (int)Height;
		}
	}
}
