using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core {
	/// <summary>
	/// スコアデータを管理するモデル.
	/// </summary>
	public interface ScoreModel {
		/// <summary>
		/// 発射数.
		/// </summary>
		int FireCount {
			set; get;
		}

		/// <summary>
		/// 命中数.
		/// </summary>
		int HitCount {
			set; get;
		}

		/// <summary>
		/// 撃破数.
		/// </summary>
		int KillCount {
			set; get;
		}

		/// <summary>
		/// 命中率.
		/// </summary>
		int HitProbability {
			get;
		}

		/// <summary>
		/// 全てのデータを評価して総合評価を計算します.
		/// </summary>
		/// <returns></returns>
		ScoreRank CalculateRank();
	}

	/// <summary>
	/// 総合評価.
	/// </summary>
	public enum ScoreRank {
		SS, S, A, B, C, D
	}
}
