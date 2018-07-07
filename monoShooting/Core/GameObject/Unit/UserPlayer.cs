using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.Utils;
using Shoot.Core.GameObject.Bullet;

namespace Shoot.Core.GameObject.Unit {
	/// <summary>
	/// プレイヤーが操作可能なユニットです.
	/// </summary>
	public class UserPlayer : PlayerBase {

		public UserPlayer() : base() {
		}
		
		public override void Update(GameTime gameTime, Field field) {
			UpdatePosition(gameTime, field);
			UpdateEnergy(gameTime, field);
			UpdateLaser(gameTime, field);
			UpdateBom(field);
			base.Update(gameTime, field);
		}

		private void UpdatePosition(GameTime gameTime, Field field) {
			KeyboardState s = Keyboard.GetState();
			this.AccelerationX = 0;
			this.AccelerationY = 0;
			if(s.IsKeyDown(Keys.Left)) {
				this.AccelerationX = -SpeedX;
			}
			if(s.IsKeyDown(Keys.Right)) {
				this.AccelerationX = SpeedX;
			}
			if(s.IsKeyDown(Keys.Up)) {
				this.AccelerationY = -SpeedY;
			}
			if(s.IsKeyDown(Keys.Down)) {
				this.AccelerationY = SpeedY;
			}
		}

		private void UpdateEnergy(GameTime time, Field field) {
			//Zキーが押されていないか、Xキーが押されているか(レーザー照射)、エネルギー不足
			KeyboardState s = Keyboard.GetState();
			if(!s.IsKeyDown(Keys.Z) || s.IsKeyDown(Keys.X)) {
				return;
			}
			Fire(time, field);
		}

		private void UpdateLaser(GameTime time, Field field) {
			//Xキーが押されていないか、Zキーが押されているか(バレット発射)、エネルギー不足
			KeyboardState s = Keyboard.GetState();
			if(!s.IsKeyDown(Keys.X) || s.IsKeyDown(Keys.Z)) {
				Cancel();
				return;
			}
			Irradiation(time, field);
		}

		private void UpdateBom(Field field) {
			KeyboardState s = Keyboard.GetState();
			if(!s.IsKeyDown(Keys.B)) {
				return;
			}
			Bomb(field);
		}
	}
}
