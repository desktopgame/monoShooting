using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bean.Core;
using Microsoft.Xna.Framework;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// IUnitの基底クラスです.
	/// </summary>
	public class UnitBase : GameObjectSimple, IUnit {
		public event PropertyChangeHandler<float> OnHitPointMaxChanged;
		public event PropertyChangeHandler<float> OnHitPointValueChanged;
		public event DamageHandler OnDamage;

		public float HitPointMax {
			set {
				float old = this.hitPointMax;
				this.hitPointMax = value;
				OnHitPointMaxChanged?.Invoke(this, new PropertyChangeEventArgs<float>(old, value));
			}
			get { return hitPointMax; }
		}

		public float HitPointValue {
			set {
				float old = this.hitPointValue;
				this.hitPointValue = value;
				OnHitPointValueChanged?.Invoke(this, new PropertyChangeEventArgs<float>(old, value));
			}
			get { return hitPointValue; }
		}

		public float SpeedX {
			set; get;
		}

		public float SpeedY {
			set; get;
		}

		private float hitPointMax;
		private float hitPointValue;

		public UnitBase(string contentPath, float max) : base(contentPath) {
			this.HitPointMax = max;
			this.HitPointValue = max;
		}

		public override void Update(GameTime gameTime, Field field) {
			if(HitPointValue <= 0) {
				this.IsDespawn = true;
			}
			base.Update(gameTime, field);
		}

		public virtual void DamageFrom(DamageSource src) {
			this.HitPointValue -= src.Value;
			OnDamage?.Invoke(this, new DamageEventArgs(src, this));
		}
	}
}
