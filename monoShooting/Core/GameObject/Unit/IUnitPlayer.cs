using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// プレイヤー.
	/// </summary>
	public interface IUnitPlayer : IUnit {
		/// <summary>
		/// エネルギー最大値.
		/// </summary>
		float EnergyPointMax {
			set; get;
		}

		/// <summary>
		/// エネルギー現在値.
		/// </summary>
		float EnergyPointValue {
			set; get;
		}

		/// <summary>
		/// ボムポイント最大値.
		/// </summary>
		float BomPointMax {
			set; get;
		}

		/// <summary>
		/// ボムポイント現在値.
		/// </summary>
		float BomPointValue {
			set; get;
		}
	}
}
