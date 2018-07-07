using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI;
using XNA2D.Core.UI.Label;

namespace XNA2D.Core.UI.Table {
	/// <summary>
	/// TableCellRenderer,HeaderCellRendererのデフォルト実装です.
	/// </summary>
	public class DefaultTableCellRenderer : Label.Label, TableCellRenderer, HeaderCellRenderer {

		public DefaultTableCellRenderer(SpriteFont font) : base(font, "") {
		}

		public IXNAComponent GetTableCellComponent(Table table, int row, int column, object value, bool hasFocus, bool isSelected) {
			string s = value is string ? (string)value : value.ToString();
			Text = s;
			Foreground = table.Foreground;
			Background = table.Background;
			return this;
		}

		public IXNAComponent GetHeaderCellComponent(Table table, Header header, int index, object value) {
			//ヘッダ
			if(value == null || index == -1) {
				value = "    ";
			}
			string s = value is string ? (string)value : value.ToString();
			Text = s;
			Foreground = table.Foreground;
			Background = table.Background;
			return this;
		}
	}
}
