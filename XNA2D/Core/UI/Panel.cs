using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.Activity;
using Microsoft.Xna.Framework.Input;

namespace XNA2D.Core.UI {
	/// <summary>
	/// コンテナとして機能するパネルです.
	/// </summary>
	public class Panel : XNAContainerBase {

		/// <summary>
		/// 背景画像.<br>
		/// 0,0地点に描画されるので、パネルのサイズと等しい画像である必要があります。
		/// </summary>
		public Texture2D Image {
			set; get;
		}


		public Panel(ILayoutManager layoutManager) : base(layoutManager) {
		}

		public Panel() : this(null) {
		}

		public override void Draw(Canvas canvas) {
			if(Image != null) {
				canvas.DrawImage(Image, Vector2.Zero, Color.White);
			}
			base.Draw(canvas);
			//デバッグ用
			//Rectangle rect = Bounds.ToRectangle();
			//rect.Width--;
			//rect.Height--;
			//canvas.DrawRectangle(rect, Color.Red);
		}
	}
}
