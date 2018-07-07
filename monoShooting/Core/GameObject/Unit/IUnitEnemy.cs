using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// 画面左端へ到達することで防衛ゲージを減らせるユニットです.
	/// </summary>
	public interface IUnitEnemy : IUnit {
		/// <summary>
		/// 画面左端への到達を監視するリスナーのリストです.
		/// </summary>
		event EventHandler OnGoaled;

		/// <summary>
		/// ドロップアイテムを生成します.
		/// </summary>
		/// <returns>ドロップしない場合はnull</returns>
		Item.Item CreateDropItem();
	}
}
