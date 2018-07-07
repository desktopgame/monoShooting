using Shoot.Core.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core {
	/// <summary>
	/// フィールドのオブジェクトの増減を監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void FieldModelChanged(object sender, FieldModelEventArgs e);

	/// <summary>
	/// フィールドに存在するオブジェクトの増減を通知するイベントです.
	/// </summary>
	public class FieldModelEventArgs : EventArgs {
		/// <summary>
		/// イベントの種類を表す列挙です.
		/// </summary>
		public enum EventType {
			Add, Remove
		}

		/// <summary>
		/// 追加/削除されたオブジェクト.
		/// </summary>
		public IGameObject GameObject {
			private set;  get;
		}

		public EventType Type {
			private set; get;
		}

		public FieldModelEventArgs(IGameObject gameObject, EventType type) {
			this.GameObject = gameObject;
			this.Type = type;
		}
	}
}
