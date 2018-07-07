using Microsoft.Xna.Framework;
using Shoot.Core.GameObject.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNAActivity.Core.Utils;

namespace Shoot.Core.GameObject.Generator {
	/// <summary>
	/// 難易度ノーマル.
	/// </summary>
	public class NormalGenerator : IGenerator {
		private int count;
		private IBoss lastBoss;
		private FrameTimer timer;

		private static readonly int LevelUp = 10;

		public NormalGenerator() {
			this.timer = FrameTimer.ForSecond(3);
		}

		public IGameObject[] Generate(GameTime gameTime, Field field) {
			//指定の時間が経過していない
			if(!timer.Update().IsElapsed()) {
				return new IGameObject[] { };
			}
			//ボスを倒すたびにレベルがあがる
			int level = (count / LevelUp) + 1;
			//前に出現したボスがまだ倒れていないなら雑魚はださない
			if(lastBoss != null && !lastBoss.IsDespawn) {
				return new IGameObject[] { };
			}
			this.count++;
			//10の倍数でないなら雑魚
			if(count % LevelUp != 0) {
				List<IGameObject> list = new List<IGameObject>();
				float x = field.Width - 32;
				float y = 0;
				for(int i = 0; i < 5; i++) {
					IUnitEnemy unit = CreateEnemy(level, i);
					unit.PositionX = x;
					unit.PositionY = y;
					y += 32;
					list.Add(unit);
				}
				return list.ToArray();
			}
			//10の倍数ならボス
			IBoss boss = CreateBoss(level);
			boss.PositionX = field.Width - boss.Width;
			boss.PositionY = field.Height / 2;
			this.lastBoss = boss;
			return new IGameObject[] { boss };
		}

		/// <summary>
		/// レベルに応じた雑魚をだす.
		/// </summary>
		/// <param name="level"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		private IUnitEnemy CreateEnemy(int level, int index) {
			if(level == 1) {
				return new RandomEnemy();
			} else {
				return new RandomEnemy2();
			}
		}

		/// <summary>
		/// レベルに応じたボスを出す
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		private IBoss CreateBoss(int level) {
			if(level == 1) {
				return new Proto();
			} else {
				return new ProtoSP();
			}
		}
	}
}
