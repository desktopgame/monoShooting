using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Talk {
	/// <summary>
	/// TalkModelのデフォルト実装です.
	/// </summary>
	public class DefaultTalkModel : MutableTalkModel {
		public event TalkModelHandler OnTalkUpdate;
		
		/// <summary>
		/// テキストが変更されると表示位置は0に設定されます。
		/// </summary>
		public string Text {
			set { 
				this.text = value;
				this.Offset = 0; 
			}
			get { return text; }
		}

		public int Offset {
			set { 
				this.offset = value;
				OnTalkUpdate?.Invoke(this, new TalkModelEventArgs(Text, Text.Substring(0, Offset)));
			}
			get { return offset; }
		}

		private string text;
		private int offset;


		public DefaultTalkModel(string text) {
			if(text.Length == 0 || text == null) {
				text = " ";
				Console.WriteLine("DefaultTalkModel#new[引数の長さが0のため、長さ1の空白に修正されました]");
			}
			this.Text = text;
		}

		public DefaultTalkModel() : this(" ") {
		}
	}
}
