using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.Activity;
using Microsoft.Xna.Framework.Input;
using XNAActivity.Core.UI;

namespace XNA2D.Core.UI {
	/// <summary>
	/// トップレベルのコンテナとして機能します.
	/// </summary>
	public class Window : ActivityAdapter , IDisposable {
		/// <summary>
		/// コンポーネントを配置するためのコンテナです.
		/// </summary>
		public Panel ContentPane {
			set; get;
		}

		private Canvas canvas;

		public Window(ILayoutManager layoutManager) {
			this.ContentPane = new RootPane(layoutManager);
			ContentPane.IsOpaque = true;
		}

		public Window() : this(null) {
		}

		//
		//IActivityの実装
		//

		/// <summary>
		/// サブクラスは必ずどこかでbase.Initialize;してください.
		/// </summary>
		/// <param name="game"></param>
		public override void Initialize(ExGame game) {
			GraphicsDeviceManager dev = game.GraphicsDeviceManager;
			Update(dev);
			game.GraphicsDeviceManager.PreparingDeviceSettings += OnWindowResize;
			base.Initialize(game);
		}

		/// <summary>
		/// 空実装です.
		/// </summary>
		/// <param name="game"></param>
		public override void LoadContent(ExGame game) {
		}

		/// <summary>
		/// 空実装です.
		/// </summary>
		/// <param name="game"></param>
		public override void UnloadContent(ExGame game) {
		}

		/// <summary>
		/// ContentPaneへ委譲します.<br>
		/// 全ての入力処理はContentPane及びそのサブコンポーネントによって処理されます。
		/// </summary>
		/// <param name="game"></param>
		/// <param name="time"></param>
		public override void Update(ExGame game, GameTime time) {
			ContentPane.Update(time, Keyboard.GetState(), Mouse.GetState());
		}

		/// <summary>
		/// ContentPaneへ委譲します.<br>
		/// 全ての描画処理はContentPane及びそのサブコンポーネントによって処理されます。
		/// </summary>
		/// <param name="game"></param>
		/// <param name="time"></param>
		/// <param name="batch"></param>
		public override void Draw(ExGame game, GameTime time, SpriteBatch batch) {
			if(canvas == null) {
				this.canvas = new Canvas(batch);
			}
			ContentPane.Draw(canvas);
		}

		/// <summary>
		/// ContentPaneの背景色で塗りつぶします.
		/// </summary>
		/// <param name="game"></param>
		public override void Clear(ExGame game) {
			if(ContentPane.IsOpaque) {
				game.GraphicsDevice.Clear(ContentPane.Background);
			}
		}

		/// <summary>
		/// レイアウトを検証します.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="info"></param>
		public override void Show(ExGame game, ITransitionInfo info) {
			ContentPane.Validate();
			game.GraphicsDeviceManager.PreparingDeviceSettings -= OnWindowResize;
			WindowManager.GetInstance().Store(game);
		}

		/// <summary>
		/// レイアウトを無効にします.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="info"></param>
		public override void Hide(ExGame game, ITransitionInfo info) {
			ContentPane.Invalidate();
			WindowManager.GetInstance().Rollback(game);
		}

		//
		//イベントハンドラ
		//

		/// <summary>
		/// ウィンドウサイズの変更をパネルへ適用.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnWindowResize(object sender, PreparingDeviceSettingsEventArgs e) {
			Update((GraphicsDeviceManager)sender);
		}

		private void Update(GraphicsDeviceManager dev) {
			ContentPane.Size.Width = dev.PreferredBackBufferWidth;
			ContentPane.Size.Height = dev.PreferredBackBufferHeight;
			ContentPane.Validate();
		}

		/// <summary>
		/// 全てのコンポーネントの描画に利用されるキャンバスを破棄します.
		/// </summary>
		public void Dispose() {
			canvas.Dispose();
		}
	}

	
	/// <summary>
	/// ウィンドウの委譲先として機能するコンテナです.
	/// </summary>
	class RootPane : Panel, IValidRoot {
		private Queue<IXNAComponent> lazyValidateScheduledComponentQueue;

		public RootPane(ILayoutManager layoutManager) : base(layoutManager) {
			this.lazyValidateScheduledComponentQueue = new Queue<IXNAComponent>();
		}

		public RootPane() : this(null) {
		}
		
		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
			while(lazyValidateScheduledComponentQueue.Count != 0) {
				lazyValidateScheduledComponentQueue.Dequeue().Validate();
			}
			base.Update(time, keyState, mouseState);
		}
		
		//
		//IValidRootの実装
		//

		public void LazyValidate(IXNAComponent c) {
			if(lazyValidateScheduledComponentQueue.Contains(c)) {
				return;
			}
			lazyValidateScheduledComponentQueue.Enqueue(c);
		}
	}
}
