using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.Activity;
using XNA2D.Core.Animation;

namespace XNA2D.Core.Activity {
	/// <summary>
	/// IActivityの空実装クラスです.
	/// </summary>
	public class ActivityAdapter : IActivity {
		private int oldWidth;
		private int oldHeight;
		/// <summary>
		/// この画面の描画されるゲーム.
		/// </summary>
		public ExGame Owner {
			private set; get;
		}
		
		public ActivityAdapter() {
			this.oldWidth = -1;
			this.oldHeight = -1;
		}

		/// <summary>
		/// サブクラスは必ずどこかでbase.Initialize;してください.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="batch"></param>
		public virtual void Initialize(ExGame game) {
			this.Owner = game;
		}

		public virtual void LoadContent(ExGame game) {
		}

		public virtual void UnloadContent(ExGame game) {
		}

		public virtual void Update(ExGame game, GameTime time) {
		}

		public virtual void Draw(ExGame game, GameTime time, SpriteBatch batch) {
		}

		public virtual void Clear(ExGame game) {
		}

		/// <summary>
		/// 保存していおいた画面サイズに戻します.<br>
		/// サブクラスは最後にbase.Hide;するか、実装で画面サイズを管理してください。
		/// </summary>
		/// <param name="game"></param>
		/// <param name="info"></param>
		public virtual void Hide(ExGame game, ITransitionInfo info) {
			if(oldWidth < 0 || oldHeight < 0) {
				return;
			}
			game.GraphicsDeviceManager.PreferredBackBufferWidth = oldWidth;
			game.GraphicsDeviceManager.PreferredBackBufferHeight = oldHeight;
		}
		
		/// <summary>
		/// 現在の画面サイズを保存して、この画面が非表示になるときに復元できるように値を保存します.<br>
		/// サブクラスは最後にbase.Show;するか、実装で画面サイズを管理してください。
		/// </summary>
		/// <param name="game"></param>
		/// <param name="info"></param>
		public virtual void Show(ExGame game, ITransitionInfo info) {
			this.oldWidth = game.GraphicsDeviceManager.PreferredBackBufferWidth;
			this.oldHeight = game.GraphicsDeviceManager.PreferredBackBufferHeight;
		}
	}
}
