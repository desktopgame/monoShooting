using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Talk {
	/// <summary>
	/// 会話の進捗を監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void TalkModelHandler(object sender, TalkModelEventArgs e);

	/// <summary>
	/// 会話の進捗を通知するイベントです.
	/// </summary>
	public class TalkModelEventArgs : EventArgs {
		/// <summary>
		/// 今後表示されるテキストも含めたテキストです.
		/// </summary>
		public string AllText {
			get;
		}

		/// <summary>
		/// 現在表示されているテキストです.
		/// </summary>
		public string SubText {
			get;
		}

		/// <summary>
		/// 全ての文章を表示したならtrueを返します.
		/// </summary>
		public bool IsComplete {
			get { return AllText.Equals(SubText); }
		}

		public TalkModelEventArgs(string all, string sub) {
			this.AllText = all;
			this.SubText = sub;
		}
	}
}
