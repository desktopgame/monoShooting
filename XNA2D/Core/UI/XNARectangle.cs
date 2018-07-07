using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI {
	/// <summary>
	/// 位置と大きさで矩形範囲を表すクラス.<br>
	/// イベント通知機構を実装します。
	/// </summary>
	public class XNARectangle {
		/// <summary>
		/// 大きさの変更を監視するリスナーのリストです.
		/// </summary>
		public event EventHandler OnSizeChanged {
			add { Size.OnSizeChanged += value; }
			remove { Size.OnSizeChanged -= value; }
		}

		/// <summary>
		/// 位置の変更を監視するリスナーのリストです.
		/// </summary>
		public event EventHandler OnLocationChanged {
			add { Point.OnLocationChanged += value; }
			remove { Point.OnLocationChanged -= value; }
		}

		/// <summary>
		/// 座標.
		/// </summary>
		public XNAPoint Point {
			set; get;
		}

		/// <summary>
		/// 大きさ.
		/// </summary>
		public XNASize Size {
			set; get;
		}

		public XNARectangle(XNAPoint point, XNASize size) {
			this.Point = point;
			this.Size = size;
		}

		public XNARectangle(float x, float y, float width, float height) : this(new XNAPoint(x, y), new XNASize(width, height)) {
		}

		public XNARectangle(XNARectangle rect) : this(rect.Point, rect.Size) {
		}

		public XNARectangle() : this(new XNAPoint(), new XNASize()) {
		}

		/// <summary>
		/// 位置&大きさが等しければtrueを返します.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj) {
			if(!(obj is XNARectangle)) {
				return false;
			}
			XNARectangle other = (XNARectangle)obj;
			return (other.Point.Equals(Point) && other.Size.Equals(Size));
		}

		/// <summary>
		/// キャストして返します.
		/// </summary>
		/// <returns></returns>
		public int GetX() {
			return Point.GetX();
		}

		/// <summary>
		/// キャストして返します.
		/// </summary>
		/// <returns></returns>
		public int GetY() {
			return Point.GetY();
		}

		/// <summary>
		/// キャストして返します.
		/// </summary>
		/// <returns></returns>
		public int GetWidth() {
			return Size.GetWidth();
		}

		/// <summary>
		/// キャストして返します.
		/// </summary>
		/// <returns></returns>
		public int GetHeight() {
			return Size.GetHeight();
		}

		/// <summary>
		/// XNAのRectangleに変換します.
		/// </summary>
		/// <returns></returns>
		public Rectangle ToRectangle() {
			return new Rectangle(Point.GetX(), Point.GetY(), Size.GetWidth(), Size.GetHeight());
		}
	}
}
