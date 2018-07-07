using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// ダメージを受けたことを監視するリスナーのリストです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void DamageHandler(object sender, DamageEventArgs e);

	/// <summary>
	/// ダメージを与えたことを通知するイベントです.
	/// </summary>
	public class DamageEventArgs : EventArgs {
		/// <summary>
		/// 加害者.
		/// </summary>
		public DamageSource Source {
			private set; get;
		}

		/// <summary>
		/// 被害者.
		/// </summary>
		public IGameObject Victim {
			private set; get;
		}

		public DamageEventArgs(DamageSource src, IGameObject v) {
			this.Source = src;
			this.Victim = v;
		}
	}
}
