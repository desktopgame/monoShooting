using Bean.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot.Core.GameObject {
	/// <summary>
	/// フィールドに存在するなんらかのオブジェクトです.
	/// </summary>
	public interface IGameObject {
		/// <summary>
		/// X座標の変更を監視するリスナーのリストです.
		/// </summary>
		event PropertyChangeHandler<float> OnPositionXChanged;

		/// <summary>
		/// Y座標の変更を監視するリスナーのリストです.
		/// </summary>
		event PropertyChangeHandler<float> OnPositionYChanged;

		/// <summary>
		/// X座標.
		/// </summary>
		float PositionX {
			set; get;
		}
		
		/// <summary>
		/// Y座標.
		/// </summary>
		float PositionY {
			set; get;
		}

		/// <summary>
		/// X方向の加速度.<br>
		/// キー入力や床の摩擦力によって変動することがあります。
		/// </summary>
		float AccelerationX {
			set; get;
		}

		/// <summary>
		/// Y方向の加速度.<br>
		/// キー入力や落下によって変動することがあります。
		/// </summary>
		float AccelerationY {
			set; get;
		}

		/// <summary>
		/// 横幅.
		/// </summary>
		float Width {
			get;
		}

		/// <summary>
		/// 縦幅.
		/// </summary>
		float Height {
			get;
		}

		/// <summary>
		/// このオブジェクトが画面から除去されるべきならtrue.
		/// </summary>
		bool IsDespawn {
			get;
		}
		
		/// <summary>
		/// このオブジェクトを更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="field"></param>
		void Update(GameTime gameTime, Field field);

		/// <summary>
		/// このオブジェクトを描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		/// <param name="field"></param>
		void Draw(GameTime gameTime, SpriteBatch batch, Field field);

		/// <summary>
		/// あたり判定を検証します.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		bool IsCollision(IGameObject other);
	}
}
