using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Talk {
	/// <summary>
	/// 会話文の内容や現在表示されている位置に関するモデルです.
	/// </summary>
	public interface TalkModel {
		/// <summary>
		/// 会話の進捗を監視するリスナーのリストです.
		/// </summary>
		event TalkModelHandler OnTalkUpdate;

		/// <summary>
		/// 表示される文章.
		/// </summary>
		string Text {
			get;
		}

		/// <summary>
		/// 現在表示されているテキストの位置.
		/// </summary>
		int Offset {
			set;  get;
		}
	}
}
