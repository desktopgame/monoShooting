using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.Utils;

namespace XNA2D.Core.UI.Button {
	/// <summary>
	/// 押し込むことで処理を起動するボタンです.
	/// </summary>
	public class Button : XNAComponentBase {
		/// <summary>
		/// 呼び出しはモデルへ転送されます.
		/// </summary>
		public event ActionHandler	OnActionPerformed {
			add { Model.OnActionPerformed += value; }
			remove { Model.OnActionPerformed -= value; }
		}

		/// <summary>
		/// 描画される文字列.
		/// </summary>
		public string Text {
			set {
				this.text = value;
				try {
					Vector2 vec = font.MeasureString(text);
					PreferredSize.Width = vec.X;
					PreferredSize.Height = vec.Y;
				} catch(ArgumentException e) {
					throw new ArgumentException("使用出来ない文字列です。コンテンツを確認してください：" + text, e);
				}
			}
			get { return text; }
		}

		/// <summary>
		/// 描画されるアイコン.
		/// </summary>
		public Texture2D Texture {
			set {
				this.texture = value;
				PreferredSize.Width = value.Width;
				PreferredSize.Height = value.Height;
			}
			get { return texture; }
		}

		/// <summary>
		/// ボタンの状態を管理するモデル.
		/// </summary>
		public ButtonModel Model {
			set; get;
		}

		/// <summary>
		/// ボタンの処理を定義するコントローラ.
		/// </summary>
		public Action<object, ActionEventArgs> Action {
			set; get;
		}

		private SpriteFont font;
		private string text;
		private Texture2D texture;

		public Button(SpriteFont font, string text) : base() {
			this.font = font;
			if(font == null) {
				throw new ArgumentException("フォントがnullです.");
			}
			this.Text = text;
			this.texture = null;
			this.IsOpaque = true;
			this.Model = new DefaultButtonModel();
		}

		public Button(Texture2D texture) : base() {
			if(texture == null) {
				throw new ArgumentException("テクスチャがnullです.");
			}
			this.Texture = texture;
		}

		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
			Rectangle rect = Bounds.ToRectangle();
			//ボタンの押し込みは範囲内のみ
			if(rect.Contains(mouseState.X, mouseState.Y)) {
				Model.IsPressed = mouseState.LeftButton == ButtonState.Pressed;
			//ボタンの離しはどこでもOK
			} else if(mouseState.LeftButton == ButtonState.Released) {
				Model.IsPressed = false;
			}
			//現在のマウス位置がボタン矩形に含まれないなら
			if(!rect.Contains(mouseState.X, mouseState.Y)) {
				Model.IsRollover = false;
			}
			//現在のマウス位置がボタン矩形に含まれるなら
			Model.IsRollover = true;
			if(Model.IsPressed) {
				Model.IsArmed = true;
			} else if(Model.IsArmed) {
				Model.IsArmed = false;
				Action?.Invoke(this, new ActionEventArgs(Model.ActionCommand));
			}
		}

		public override void Draw(Canvas canvas) {
			canvas.Clear(this);
			if(Model.IsPressed) {
				canvas.FillRectangle(Bounds.ToRectangle(), Color.Yellow);
			}
			//文字/画像を描画
			Vector2 vec = Point.ToVector2();
			if(Texture == null) {
				canvas.DrawString(font, Text, vec, Foreground);
			} else {
				canvas.DrawImage(Texture, vec, Color.White);
			}
			//枠を描画
			canvas.DrawRectangle(Bounds.ToRectangle(), Foreground);
		}
	}
}
