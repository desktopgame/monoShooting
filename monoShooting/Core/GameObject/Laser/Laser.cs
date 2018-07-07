using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.UI;
using Shoot.Core.GameObject.Unit;

namespace Shoot.Core.GameObject.Laser {
	/// <summary>
	/// 画面の端っこまで照射されるレーザー.
	/// </summary>
	public class Laser : GameObjectBase {
		/// <summary>
		/// 照射中であるか.
		/// </summary>
		public bool IsIrradiation {
			private set; get;
		}

		/// <summary>
		/// レーザーを発射しているエンティティ.
		/// </summary>
		public IGameObject GameObject {
			private set; get;
		}

		private bool begin;
		private Color color;
		private Canvas canvas;
		private int height = 20;


		public Laser(IGameObject gameObject, Color color) {
			this.GameObject = gameObject;
			this.color = color;
			this.begin = false;
			this.IsIrradiation = false;
		}

		public override void Update(GameTime gameTime, Field field) {
			//照射中は常に発射カウント
			field.ScoreModel.FireCount++;
			//あたり判定を検証
			Tuple<bool, IUnit[]> result = GameObjectUtils.Validate(this, GameObject, field, 5, true);
			if(result.Item1 && GameObject is IUnitPlayer) {
				field.ScoreModel.HitCount += result.Item2.Length;
			}
			base.Update(gameTime, field);
		}

		/// <summary>
		/// 画面の端っこまで描画.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		/// <param name="field"></param>
		public override void Draw(GameTime gameTime, SpriteBatch batch, Field field) {
			if(canvas == null) {
				this.canvas = new Canvas(batch);
			}
			if(!IsIrradiation) {
				this.IsDespawn = true;
				return;
			}
			if(GameObject is IUnitPlayer) {
				field.ScoreModel.FireCount++;
			}
			int x = (int)GameObject.PositionX;
			int y = (int)GameObject.PositionY;
			int w = (int)GameObject.Width;
			int h = (int)GameObject.Height;
			Rectangle rect = new Rectangle(x + w, y + (h / 2), field.Width - (x + w), height);
			this.PositionX = x + w;
			this.PositionY = y + (h / 2);
			this.Width = rect.Width;
			this.Height = rect.Height;
			canvas.FillRectangle(rect, color);
		}

		/// <summary>
		/// 照射を開始します.
		/// <exception cref="InvalidCastException">既にBeginが呼ばれているとき</exception>
		/// </summary>
		public void Begin() {
			if(begin) {
				throw new InvalidOperationException();
			}
			this.IsIrradiation = true;
			this.begin = true;
		}

		/// <summary>
		/// 照射を終了します.
		/// <exception cref="InvalidCastException">まだBeginが呼ばれていないとき/既にEndが呼ばれているとき</exception>
		/// </summary>
		public void End() {
			if(!begin) {
				throw new InvalidOperationException();
			}
			this.IsIrradiation = false;
		}
	}
}
