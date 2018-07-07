using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core {
	/// <summary>
	/// ScoreModelのデフォルト実装です.
	/// </summary>
	public class DefaultScoreModel : ScoreModel {
		public int FireCount {
			set; get;
		}

		public int HitCount {
			set; get;
		}

		public int HitProbability {
			get {
				if(HitCount == 0) {
					return 0;
				}
				float h = HitCount;
				float f = FireCount;
				float r = (h / f) * 100;
				return (int)Math.Ceiling(r);
			}
		}

		public int KillCount {
			set; get;
		}

		public ScoreRank CalculateRank() {
			int p = 0;
			p += HitProbability;
			p += KillCount;
			return CalculateRank(p);
		}

		private ScoreRank CalculateRank(int point) {
			if(point < 20) {
				return ScoreRank.D;
			} else if(point < 40) {
				return ScoreRank.C;
			} else if(point < 70) {
				return ScoreRank.B;
			} else if(point < 90) {
				return ScoreRank.A;
			} else if(point < 100) {
				return ScoreRank.S;
			} else {
				return ScoreRank.SS;
			}
		}
	}
}
