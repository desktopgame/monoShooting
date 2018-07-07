using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNA2D.Core.Utils;
//using SSharp;
using System.IO;
//using SSharp.Event;
//using SSharp.Byte;
using System.Diagnostics;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// AIです.
	/// </summary>
	public class AIPlayer : PlayerBase {
		private Schedule scheduleOfTask;
		//private JITCompilationUnit jitCompliationUnit;
		//private InjectSupport injectSupport;

		public AIPlayer() : base() {
			string scriptPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + @"ai.ss";
			string source = File.ReadAllText(scriptPath);
			this.scheduleOfTask = new Schedule(10);
		//	this.jitCompliationUnit = JITCompilationUnit.Compile(source);
		//	this.injectSupport = new InjectSupport();
		//	jitCompliationUnit.DebugMode = true;
		//	jitCompliationUnit.DumpTree();
		//	injectSupport.Bind(jitCompliationUnit.Enviroment);
		}

		public override void Update(GameTime gameTime, Field field) {
			if(!scheduleOfTask.Update(gameTime)) {
				base.Update(gameTime, field);
				return;
			}
			//FIXME:重い
			/*
			injectSupport["player"] = this;
			injectSupport["field"] = field;
			jitCompliationUnit.Run();
			base.Update(gameTime, field);
			//*/
			//*
			UpdateImpl(gameTime, field);
			base.Update(gameTime, field);
			//*/
		}

		private void UpdateImpl(GameTime gameTime, Field field) {
			float y = 0;
			float height = Height;
			List<int> countList = new List<int>();
			this.AccelerationY = 0f;
			//画面の上から下まで走査して敵の数を調べる
			while(y <= field.Height) {
				//その高さにいる敵を取得
				IUnit[] units = GetUnitsForY(field, y);
				countList.Add(units.Length);
				//自分も同じ高さにいるなら発射
				//ただしその高さに自分の弾が3つ以上あるなら何もしない
				if(units.Length > 0 && Contains(y, this) && GetBulletsForY(field, y).Length < 3) {
					Fire(gameTime, field);
				}
				y += Height;
			}
			//最も敵の少ない場所へ
			int index = IndexOfLowest(countList, 0);
			float toY = index * Height;
			if(!(Contains(toY, this))) {
				float diff = PositionY - toY;
				if(diff > 0) {
					this.AccelerationY = -SpeedY;
				} else if(diff < 0) {
					this.AccelerationY = SpeedY;
				}
			}
		}

		/// <summary>
		/// 指定のユニットに弾丸が含まれるかどうかを返します.
		/// </summary>
		/// <param name="unit"></param>
		/// <returns></returns>
		private bool ContainsBullet(IUnit[] unit) {
			bool r = false;
			Array.ForEach(unit, elem => {
				if(!(elem is Bullet.Bullet)) {
					return;
				}
				Bullet.Bullet bullet = (Bullet.Bullet)elem;
				if(!(bullet.GameObject is IUnitPlayer)) {
					r = true;
					return;
				}
			});
			return r;
		}

		/// <summary>
		/// 最も値の高い位置を返します.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="startIndex"></param>
		/// <returns></returns>
		private int IndexOfHighest(List<int> list, int startIndex) {
			int index = -1;
			int w = 0;
			for(int i = startIndex; i < list.Count; i++) {
				if(w < list[i]) {
					w = list[i];
					index = i;
				}
			}
			return index;
		}

		/// <summary>
		/// 最も値の低い位置を返します.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="startIndex"></param>
		/// <returns></returns>
		private int IndexOfLowest(List<int> list, int startIndex) {
			int index = -1;
			int w = int.MaxValue;
			for(int i = startIndex; i < list.Count; i++) {
				if(w > list[i]) {
					w = list[i];
					index = i;
				}
			}
			return index;
		}

		/// <summary>
		/// 自分で発射した弾丸を取得.
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		private Bullet.Bullet[] GetBullets(Field field) {
			List<Bullet.Bullet> ret = new List<Bullet.Bullet>();
			field.Model.ForEach(elem => {
				if(!(elem is Bullet.Bullet)) {
					return;
				}
				Bullet.Bullet bullet = (Bullet.Bullet)elem;
				if(bullet.GameObject is IUnitPlayer) {
					ret.Add(bullet);
				}
			});
			return ret.ToArray();
		}

		/// <summary>
		/// 自分で発射した弾丸のうち指定の高さに属するものすべてを返します.
		/// </summary>
		/// <param name="field"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private Bullet.Bullet[] GetBulletsForY(Field field, float y) {
			List<Bullet.Bullet> ret = new List<Bullet.Bullet>();
			Array.ForEach(GetBullets(field), elem => {
				if(elem.GameObject is IUnitPlayer) {
					ret.Add(elem);
				}
			});
			return ret.ToArray();
		}

		/// <summary>
		/// フィールドに存在するオブジェクトのうち指定の高さに属するオブジェクト全てを返します.
		/// </summary>
		/// <param name="field"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private IUnit[] GetUnitsForY(Field field, float y) {
			List<IUnit> ret = new List<IUnit>();
			field.Model.ForEach(elem => {
				float elemY = elem.PositionY;
				if(!Contains(y, elem) || field.Player.Equals(elem)) {
					return;
				}
				ret.Add((IUnit)elem);
			});
			return ret.ToArray();
		}

		/// <summary>
		/// オブジェクトが指定の高さに属するかどうかを返します.
		/// </summary>
		/// <param name="y"></param>
		/// <param name="o"></param>
		/// <returns></returns>
		private bool Contains(float y, IGameObject o) {
			if(!(o is IUnit)) {
				return false;
			}
			if(o.PositionX < PositionX) {
				return false;
			}
			return
			(o.PositionY >= y && o.PositionY <= y + Height);
		}
	}
}
