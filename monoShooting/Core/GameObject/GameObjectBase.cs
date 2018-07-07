using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bean.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.Utils;

namespace Shoot.Core.GameObject {
	/// <summary>
	/// IGameObjectの基底クラスです.<br>
	/// データの保持とオブザーバーへの通知機構をサポートします。
	/// </summary>
	public abstract class GameObjectBase : IGameObject {
		public event PropertyChangeHandler<float> OnPositionXChanged;
		public event PropertyChangeHandler<float> OnPositionYChanged;

		public float PositionX {
			set {
				float old = positionX;
				this.positionX = value;
				OnPositionXChanged?.Invoke(this, new PropertyChangeEventArgs<float>(old, value));
			}
			get { return positionX; }
		}

		public float PositionY {
			set {

				float old = positionY;
				this.positionY = value;
				OnPositionYChanged?.Invoke(this, new PropertyChangeEventArgs<float>(old, value));
			}
			get { return positionY; }
		}

		public float Width {
			protected set; get;
		}

		public float Height {
			protected set; get;
		}

		public float AccelerationX {
			set; get;
		}

		public float AccelerationY {
			set; get;
		}

		public bool IsDespawn {
			set; get;
		}

		private float positionX;
		private float positionY;


		public GameObjectBase() {
		}

		/// <summary>
		/// 加速度に応じて座標を更新します.<br>
		/// サブクラスは必ず最後にbase.Update;してください。
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="field"></param>
		public virtual void Update(GameTime gameTime, Field field) {
			float newX = PositionX + AccelerationX;
			float newY = PositionY + AccelerationY;
			this.PositionX = Math.Min(field.Width - Width, Math.Max(0, newX));
			this.PositionY = Math.Min(field.Height - Height, Math.Max(0, newY));
		}
		
		public virtual bool IsCollision(IGameObject other) {
			Rectangle selfRect = new Rectangle((int)PositionX, (int)PositionY, (int)Width, (int)Height);
			Rectangle otherRect = new Rectangle((int)other.PositionX, (int)other.PositionY, (int)other.Width, (int)other.Height);
			return selfRect.Intersects(otherRect);
		}

		public abstract void Draw(GameTime gameTime, SpriteBatch batch, Field field);
	}
}
