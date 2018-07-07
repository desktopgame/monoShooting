using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI.Label;

namespace XNA2D.Core.UI.List {
	/// <summary>
	/// IListCellRendererのデフォルト実装です.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DefaultListCellRenderer<T> : Label.Label, ListCellRenderer<T> {
		
		public DefaultListCellRenderer(SpriteFont font) : base(font, "") {
		}


		public IXNAComponent GetListCellComponent(ItemList<T> list, int index, T value, bool hasFocus, bool isSelected) {
			string text = value is string ? (string)((object)value) : value.ToString();
			Text = text;
			Foreground = list.Foreground;
			Background = list.Background;
			return this;
		}
	}
}
