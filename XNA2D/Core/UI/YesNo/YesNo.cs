using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.UI;
using Microsoft.Xna.Framework.Graphics;

namespace XNAActivity.Core.UI.YesNo {
	//NOTE:XBoxコントローラへの対応済み
	/// <summary>
	/// Yes, Noを選択出来るコンポーネントです.
	/// </summary>
	public class YesNo : XNAComponentBase {
		/// <summary>
		/// Yes, Noの決定を監視するリスナーのリストです.
		/// </summary>
		public event YesNoHandler OnEnter;

		/// <summary>
		/// YesとNoの間の余白.
		/// </summary>
		public float Margin {
			set {
				this.margin = value;
				Vector2 yesSize = font.MeasureString("Yes");
				Vector2 noSize = font.MeasureString("No");
				this.PreferredSize = new XNASize(yesSize.X + Margin + noSize.X, yesSize.Y);
			}
			get { return margin; }
		}

		/// <summary>
		/// 現在選択されている設定です.
		/// </summary>
		public YesNoEnum Option {
			set; get;
		}

		private SpriteFont font;
		private float margin;

		public YesNo(SpriteFont font) : base() {
			this.font = font;
			this.Margin = 50;
			this.Option = YesNoEnum.Yes;
		}

		public override void Draw(Canvas canvas) {
			//Yesを描画
			Vector2 yesPos = Point.ToVector2();
			canvas.DrawString(font, "Yes", yesPos, Foreground);
			//Noを描画
			Vector2 yesSize = font.MeasureString("Yes");
			Vector2 noPos = new Vector2(yesPos.X + yesSize.X + Margin, yesPos.Y);
			canvas.DrawString(font, "No", noPos, Foreground);
			//枠を描画
			Rectangle rect;
			Vector2 pos;
			Vector2 size;
			if(Option == YesNoEnum.Yes) {
				pos = yesPos;
				size = font.MeasureString("Yes");
			} else {
				pos = noPos;
				size = font.MeasureString("No");
			}
			rect.X = (int)pos.X;
			rect.Y = (int)pos.Y;
			rect.Width = (int)size.X;
			rect.Height = (int)size.Y;
			canvas.DrawRectangle(rect, Foreground);
		}

		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
			Detector detector = Detector.GetInstance();
			if(detector.IsDetect(Handle.LEFT) || 
			   detector.IsDetect(Handle.RIGHT)) {
				this.Option = Option == YesNoEnum.Yes ? YesNoEnum.No : YesNoEnum.Yes;
			}
			if(detector.IsDetect(Handle.ENTER)) {
				OnEnter?.Invoke(new YesNoEventArgs(Option));
			}
		}
	}
}
