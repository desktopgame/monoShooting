using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Utils {

	/// <summary>
	/// コンテンツのロード/アンロードを監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ContentHandler(object sender, ContentEventArgs e);

	/// <summary>
	/// コンテンツのロード/アンロードを通知するイベントです.
	/// </summary>
	public class ContentEventArgs : EventArgs {
		/// <summary>
		/// ロードとアンロードを表す列挙です.
		/// </summary>
		public enum EventType {
			Load, Unload
		}

		/// <summary>
		/// 読み込まれたコンテンツのパス.
		/// </summary>
		public string Path {
			private set; get;
		}

		/// <summary>
		/// ロード/アンロードされたコンテンツ.
		/// </summary>
		public object Content {
			private set; get;
		}

		/// <summary>
		/// イベントの種類.
		/// </summary>
		public EventType Type {
			private set; get;
		}

		public ContentEventArgs(string path, object content, EventType type) {
			this.Path = path;
			this.Content = content;
			this.Type = type;
		}
	}
}
