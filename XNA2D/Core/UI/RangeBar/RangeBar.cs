using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.Utils;

namespace XNA2D.Core.UI.RangeBar {
	/// <summary>
	/// 最小値,現在値,最大値で表される範囲を表現するバーです.
	/// </summary>
	public class RangeBar : XNAComponentBase {
		/// <summary>
		/// このバーで表示される範囲.
		/// </summary>
		public RangeModel Model {
			set {
				this.model = value;
				PreferredSize = new XNASize(model.Maximum, 16);
			}
			get { return model; }
		}

		private RangeModel model;

		public RangeBar(RangeModel model) : base() {
			this.Model = model;
		}

		/// <summary>
		/// DefualtRangeModel()で初期化します.
		/// </summary>
		public RangeBar() : this(new DefaultRangeModel()) {
		}

		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
		}

		public override void Draw(Canvas canvas) {
			int width = (int)((Model.Value / Model.Maximum) * Size.Width);
			Rectangle borderRect = Bounds.ToRectangle();
			Rectangle contentsRect = new Rectangle(borderRect.X, borderRect.Y, width, borderRect.Height);
			DrawRange(canvas, contentsRect);
			DrawBorder(canvas, borderRect);
		}

		/// <summary>
		/// 進捗や%を表す内側を描画します.
		/// </summary>
		/// <param name="canvas"></param>
		/// <param name="contentRect"></param>
		protected virtual void DrawRange(Canvas canvas, Rectangle contentRect) {
			canvas.FillRectangle(contentRect, Foreground);
		}

		/// <summary>
		/// 棒の枠線部分を描画します.
		/// </summary>
		/// <param name="canvas"></param>
		/// <param name="borderRect"></param>
		protected virtual void DrawBorder(Canvas canvas, Rectangle borderRect) {
			canvas.DrawRectangle(borderRect, Background);
		}
	}
}
