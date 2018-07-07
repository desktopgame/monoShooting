using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.UI;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2D.Core.UI.Text {
	/// <summary>
	/// 1行分のテキストを扱うことが出来る実装です.
	/// </summary>
	public class TextField : TextComponent {
		public override SpriteFont Font {
			set {
				base.Font = value;
				PreferredSize.Height = Font.MeasureString("M").Y;
			}
		}

		private static string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		
		public TextField(SpriteFont font, Document doc) : base(font, doc) {
			ResizeRows(1);
			Document.OnInsertUpdate += OnDocumentUpdate;
			Document.OnRemoveUpdate += OnDocumentUpdate;
		}

		public TextField(SpriteFont font) : this(font, new PlainDocument()) {
		}

		/// <summary>
		/// このコンポーネントは1行のみの表示にしか対応していません.
		/// </summary>
		/// <param name="rows"></param>
		/// <exception cref="ArgumentException">rows != 1</exception>
		public override void ResizeRows(int rows) {
			if(rows != 1) {
				throw new ArgumentException();
			}
			base.ResizeRows(rows);
		}

		/// <summary>
		/// マウスクリックでフォーカスオン、キー入力でドキュメントへ書き込み.
		/// </summary>
		/// <param name="time"></param>
		/// <param name="keyState"></param>
		/// <param name="mouseState"></param>
		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
			Rectangle rect = Bounds.ToRectangle();
			float mx = mouseState.X;
			float my = mouseState.Y;
			Vector2 mc = new Vector2(mx, my);
			bool contains = rect.Contains((int)mx, (int)my);
			bool clickLeft = mouseState.LeftButton == ButtonState.Pressed;
			//左クリック
			if(contains && clickLeft) {
				int offset = ViewToModel(mc);
				Caret.Position = Math.Min(Document.Length, Math.Max(0, offset));
				HasFocus = true;
			} else if(!contains && clickLeft) {
				HasFocus = false;
			}
			//フォーカスが当たっていないと入力できない
			if(!HasFocus) {
				return;
			}
			//キー入力(IME未対応)
			Keys[] keys = keyState.GetPressedKeys();
			if(keys != null && keys.Length > 0) {
				InputKey(keys);
			}
		}

		/// <summary>
		/// 引数のキー配列をドキュメントへ書き込みます.
		/// </summary>
		/// <param name="keys"></param>
		private void InputKey(Keys[] keys) {
			StringBuilder sb = new StringBuilder();
			foreach(Keys key in keys) {
				string s = key.ToString();
				//カレット位置を変更
				if(key.Equals(Keys.Left)) {
					Caret.Position = Math.Min(Document.Length, Math.Max(0, Caret.Position - 1));
				} else if(key.Equals(Keys.Right)) {
					Caret.Position = Math.Min(Document.Length, Math.Max(0, Caret.Position + 1));
				}
				//とりあえずa~zA~zしか書き込めない
				if(key.Equals(Keys.Enter) || !Alphabet.Contains(s)) {
					continue;
				}
				sb.Append(s);
			}
			Document.InsertString(Caret.Position, sb.ToString());
		}

		/// <summary>
		/// ドキュメントに格納された文字列を描画します.
		/// </summary>
		/// <param name="canvas"></param>
		public override void Draw(Canvas canvas) {
			//背景を塗りつぶす
			canvas.FillRectangle(Bounds.ToRectangle(), Background);
			//枠を描画
			canvas.DrawRectangle(Bounds.ToRectangle(), Foreground);
			//テキストを描画
			float x = Point.X;
			float y = Point.Y;
			DrawString(canvas, x, y);
			//フォーカスが当たっているならカーソルも描画
			if(!HasFocus) {
				return;
			}
			x = Point.X + (Font.MeasureString(Document.GetText(0, Caret.Position)).X);
			canvas.DrawLine(new Vector2(x, y), new Vector2(x, y + Size.Height), CaretColor);
		}

		/// <summary>
		/// 指定位置から文字列の描画を開始します.
		/// </summary>
		/// <param name="canvas"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		private void DrawString(Canvas canvas, float x, float y) {
			string prev = Document.GetText(0, Caret.Position);
			string next = Document.GetText(Caret.Position, Document.Length - Caret.Position);
			string all = Document.GetText(0, Document.Length);
			float componentWidth = Size.Width;
			float stringWidth = Font.MeasureString(all).X;
			Vector2 v = new Vector2(x, y);
			//描画する文字列がコンポーネントより大きいならスクロール
			if(stringWidth > componentWidth) {
				string range;
				int overWidth = (int)(componentWidth / Font.MeasureString("M").X);
				if(Caret.Position + overWidth > Document.Length) {
					int a = Document.Length - (Caret.Position + overWidth);
					range = Document.GetText(Caret.Position, Math.Max(0, a));
				} else {
					range = Document.GetText(Caret.Position, overWidth);
				}
				canvas.DrawString(Font, range, v, Foreground);
			} else {
				canvas.DrawString(Font, all, v, Foreground);
			}
		}

		/// <summary>
		/// 物理座標を論理座標に変換します.
		/// </summary>
		/// <param name="vec"></param>
		/// <returns></returns>
		private int ViewToModel(Vector2 vec) {
			float x = Font.MeasureString("M").X;
			return (int)Math.Round((vec.X - Point.X) / x);
		}

		//
		//イベントハンドラ
		//

		/// <summary>
		/// ドキュメントのテキストが変更されると呼ばれます.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnDocumentUpdate(object sender, DocumentEventArgs e) {
			string s = Document.GetText(0, Document.Length);
			MaximumSize.Width = Font.MeasureString(s).X;
		}
	}
}
