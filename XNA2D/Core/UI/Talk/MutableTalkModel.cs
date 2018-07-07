using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.Talk {
	/// <summary>
	/// 可変な実装です.
	/// </summary>
	public interface MutableTalkModel : TalkModel {
		new string Text {
			set; get;
		}
	}
}
