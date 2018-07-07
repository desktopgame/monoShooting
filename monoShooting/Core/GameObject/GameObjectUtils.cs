using Shoot.Core.GameObject.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core.GameObject {
	/// <summary>
	/// IGameObjectに関するユーティリティクラス.
	/// </summary>
	public static class GameObjectUtils {
		/// <summary>
		/// 全てのエンティティとのあたり判定を検証します.<br>
		/// プレイヤーが発射したならプレイヤー以外全て、<br>
		/// 敵が発射したならプレイヤーとの衝突を検証します。
		/// </summary>
		/// <param name="attacker">敵と衝突することでダメージを与えることが出来るオブジェクト</param>
		/// <param name="damageSource">それを生成したオブジェクト</param>
		/// <param name="field"></param>
		/// <param name="damage"></param>
		/// <param name="isMultiHit">複数相手にヒットするならtrue</param>
		/// <returns>ヒットしたか/ヒットした被害者</returns>
		public static Tuple<bool, IUnit[]> Validate(IGameObject attacker, IGameObject damageSource, Field field, float damage, bool isMultiHit) {
			List<IUnit> victim = new List<IUnit>();
			bool isPlayer = damageSource is IUnitPlayer;
			bool ret = false;
			IUnit player = field.Player;
			//発射したのがプレイヤーなら敵との接触を検証する
			//敵の発射した弾がプレイヤーにヒットすることはない
			if(isPlayer) {
				field.Model.ForEach((index, elem) => {
					if(!(elem is IUnit) || elem.Equals(attacker) || !elem.IsCollision(attacker)) {
						return false;
					}
					IUnit unit = (IUnit)elem;
					unit.DamageFrom(new DamageSource(damageSource, damage));
					ret = true;
					victim.Add(unit);
					return !isMultiHit;
				});
			//発射したのがプレイヤー以外ならプレイヤーとの接触を検証する
			//敵の発射した弾が別の敵にヒットすることはない
			} else if(player.IsCollision(attacker)) {
				player.DamageFrom(new DamageSource(damageSource, damage));
				ret = true;
				victim.Add(player);
			}
			return new Tuple<bool, IUnit[]>(ret, victim.ToArray());
		}
	}
}
