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
	/// テキストコンポーネントの基底クラスです.
	/// </summary>
	public abstract class TextComponent : XNAComponentBase {
		/// <summary>
		/// カレット.
		/// </summary>
		public Caret Caret {	
			set; get;
		}

		/// <summary>
		/// ドキュメント.
		/// </summary>
		public virtual Document Document {
			set; get;
		}

		/// <summary>
		/// フォント.
		/// </summary>
		public virtual SpriteFont Font {
			set; get;
		}

		/// <summary>
		/// カレット色.
		/// </summary>
		public Color CaretColor {
			set; get;
		}
		
		public TextComponent(SpriteFont font, Document doc) {
			this.Font = font;
			this.Caret = new DefaultCaret();
			this.Document = doc;
			this.CaretColor = Foreground;
		}

		public TextComponent(SpriteFont font) : this(font, new PlainDocument()) {
		}

		/// <summary>
		/// 表示行数を設定します.
		/// </summary>
		/// <param name="rows"></param>
		public virtual void ResizeRows(int rows) {
			PreferredSize.Height = Font.MeasureString("M").Y * rows;
		}

		/// <summary>
		/// 表示列数を設定します.
		/// </summary>
		/// <param name="column"></param>
		public virtual void ResizeColumn(int column) {
			PreferredSize.Width = Font.MeasureString("M").Y * column;
		}
	}
}
