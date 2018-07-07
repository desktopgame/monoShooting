using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2D.Core.UI.Talk {
	/// <summary>
	/// 会話文をボックスで表示するUIです.
	/// </summary>
	public class Talk : XNAComponentBase {
		/// <summary>
		/// このUIによって表示される会話文の進捗を保持するモデルです.
		/// </summary>
		public TalkModel Model {
			set {
				if(model != null) {
					model.OnTalkUpdate -= OnTalkUpdate;
				}
				this.model = value;
				model.OnTalkUpdate += OnTalkUpdate;
				UpdatePreferredSize();
			} 
			get { return model; }
		}
		
		/// <summary>
		/// 会話文を表示するためのフォント.
		/// </summary>
		public SpriteFont Font {
			set; get;
		}

		/// <summary>
		/// 一文字ずつ表示するときの間隔.
		/// </summary>
		public float CharacterUpdateInterval {
			set {
				this.characterProgressInterval = value;
				this.scheduleOfPrintMessage = new Schedule(characterProgressInterval);
			}
			get { return characterProgressInterval; }
		}

		/// <summary>
		/// ボックスの横幅.
		/// 殆どの場合画面いっぱいに広げるために使用されます。
		/// 
		/// ただし、収容する文字の横幅より小さい場合は使用されません。
		/// </summary>
		public float FixedWidth {
			set; get;
		}

		/// <summary>
		/// ボックスの縦幅.
		/// 殆どの場合画面いっぱいに広げるために使用されます。
		/// 
		/// ただし、収容する文字の縦幅より小さい場合は使用されません。
		/// </summary>
		public float FixedHeight {
			set; get;
		}

		/// <summary>
		/// 収容する文字の大きさに関係なく、常に一定の大きさのボックスを表示する場合はtrue.
		/// デフォルトはfalseです。
		/// </summary>
		public bool IsFixedMode {
			set; get;
		}

		private TalkModel model;
		private Detector keyDetector;
		private Schedule scheduleOfPrintMessage;
		private float characterProgressInterval;

		public Talk(SpriteFont font, TalkModel model) {
			if(font == null) {
				throw new ArgumentException("フォントがnullです");
			}
			this.Font = font;
			this.Model = model;
			this.CharacterUpdateInterval = 500f;
			this.keyDetector = Detector.GetInstance();
		}

		public Talk(SpriteFont font, string text) : this(font, new DefaultTalkModel(text)) {
		}

		public override void Draw(Canvas canvas) {
			string text = Model.Text.Substring(0, Model.Offset);
			Vector2 bodyPosition = new Vector2(Point.X, Point.Y);
			Rectangle border = new Rectangle(Point.GetX(), Point.GetY(), Size.GetWidth(), Size.GetHeight());
			canvas.DrawString(Font, text, bodyPosition, Color.White);
			canvas.DrawRectangle(border, Color.Red);
		}

		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
			KeyboardState s = Keyboard.GetState();
			//500ﾐﾘ秒ごとに文字を進める
			if(scheduleOfPrintMessage.Update(time) && Model.Offset < Model.Text.Length) {
				Model.Offset++;
			}
			//エンターが押された
			if(!keyDetector.IsDetect(Keys.Enter)) {
				return;
			}
			//まだ全ての文章が表示されていなかったら全部表示
			if(Model.Offset < Model.Text.Length) {
				Model.Offset = Model.Text.Length;
				return;
			}
		}

		//
		//イベントハンドラ
		//

		private void OnTalkUpdate(object sender, TalkModelEventArgs e) {
			UpdatePreferredSize();
		}

		private void UpdatePreferredSize() {
			XNASize prefSize = new XNASize();
			Vector2 strVec = Font.MeasureString(Model.Text);
			prefSize.Width = (IsFixedMode && FixedWidth >= strVec.X) ? FixedWidth : strVec.X;
			prefSize.Height = (IsFixedMode && FixedHeight >= strVec.Y) ? FixedHeight : strVec.Y;
			this.PreferredSize = prefSize;
		}
	}
}
