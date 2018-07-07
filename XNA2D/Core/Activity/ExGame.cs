using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace XNA2D.Core.Activity {
	/// <summary>
	/// 画面遷移をサポートするGameの拡張です.
	/// </summary>
	public class ExGame : Game {
		/// <summary>
		/// スプライトの表示.
		/// </summary>
		public GraphicsDeviceManager GraphicsDeviceManager {
			private set; get;
		}

		/// <summary>
		/// 処理をゲームスレッドで行うためのキュー.
		/// </summary>
		public EventQueue EventQueue {
			set; get;
		}

		/// <summary>
		/// 画面遷移を行ってから200ミリ秒の間だけtrueになるフラグです.
		/// この間は更新を行いません(キーイベントで画面遷移する実装のときに一度のキー入力でいくつか画面が飛ぶ場合があるので、このフラグを確認してキーイベントを無視する)
		/// </summary>
		public bool IsEnabledUpdate {
			private set; get;
		}

		/// <summary>
		/// 画面遷移を制御するマップ.
		/// </summary>
		public ActivityMap ActivityMap {
			private set; get;
		}

		/// <summary>
		/// このゲームを描画するスプライトです.
		/// #LoadContentが呼ばれるまではnullです。
		/// </summary>
		public SpriteBatch SpriteBatch {
			private set; get;
		}

		private Timer timer;


		public ExGame() : base() {
			this.EventQueue = new EventQueue();
			this.GraphicsDeviceManager = new GraphicsDeviceManager(this);
			this.Content.RootDirectory = "Content";
			this.timer = new Timer();
			this.IsEnabledUpdate = false;
			timer.Elapsed += new ElapsedEventHandler(timerCallBack);
			timer.Interval = 200;
		}
		
		/// <summary>
		/// このゲームで表示されるアクティビティを登録します.
		/// </summary>
		/// <param name="activityMap"></param>
		protected internal virtual void PreInitialize(ActivityMap activityMap) {
			this.ActivityMap = activityMap;
		}
		
		/// <summary>
		/// ウィンドウを中央へ移動します.
		/// </summary>
		/// <param name="isApplyChanges"></param>
		public void WindowToCenter(bool isApplyChanges=true) {
			//スクリーンの大きさ
			int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
			int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
			System.Windows.Forms.Form f = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(Window.Handle);
			//画面中央へ移動
			int x = ((screenWidth / 2) - (f.Width / 2));
			int y = ((screenHeight / 2) - (f.Height / 2));
			f.Location = new System.Drawing.Point(x, y);
			if(isApplyChanges) {
				GraphicsDeviceManager.ApplyChanges();
			}
		}
		
		//
		//コールバックメソッド
		//

		/// <summary>
		/// 現在表示されているアクティビティへ委譲します.
		/// </summary>
		protected override void Initialize() {
			ActivityMap.Initialize(this);
			base.Initialize();
		}

		/// <summary>
		/// 現在表示されているアクティビティへ委譲します.
		/// </summary>
		protected override void LoadContent() {
			this.SpriteBatch = new SpriteBatch(GraphicsDevice);
			ActivityMap.LoadContent(this);
		}

		/// <summary>
		/// 現在表示されているアクティビティへ委譲します.
		/// </summary>
		protected override void UnloadContent() {
			ActivityMap.UnloadContent(this);
		}

		/// <summary>
		/// 現在表示されているアクティビティへ委譲します.
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Update(GameTime gameTime) {
			if(IsEnabledUpdate) {
				UpdateDisabled(gameTime);
				return;
			}
			if(!EventQueue.IsEmpty) {
				EventQueue.PollEvent()();
			}
			ActivityMap.Update(this, gameTime);
			base.Update(gameTime);
		}

		/// <summary>
		/// 画面遷移を行ってから200ミリ秒間に発生した更新を受け取るコールバックメソッドです.
		/// </summary>
		/// <param name="time"></param>
		protected virtual void UpdateDisabled(GameTime time) {
		}

		/// <summary>
		/// 現在表示されているアクティビティへ委譲します.
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Draw(GameTime gameTime) {
			ActivityMap.Draw(this, gameTime, SpriteBatch);
			base.Draw(gameTime);
		}

		//
		//画面遷移
		//

		/// <summary>
		/// 直前の画面に戻ります.
		/// </summary>
		/// <param name="info"></param>
		public void Return(ITransitionInfo info) {
			if(info == null) info = NullTransitionInfo.INSTANCE;
			ActivityMap.Return(this, info);
			this.IsEnabledUpdate = true;
			timer.Start();
		}

		/// <summary>
		/// 直前の画面に戻ります.
		/// NullTransitionInfoを渡してオーバーロードメソッドを呼び出します.
		/// </summary>
		public void Return() {
			Return(null);
		}

		/// <summary>
		/// 指定の名前に紐づけられた画面へ遷移します.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="info"></param>
		public void Goto(string name, ITransitionInfo info) {
			if(info == null) info = NullTransitionInfo.INSTANCE;
			ActivityMap.Goto(this, name, info);
			//しばらくの間キー入力を受け付けない
			this.IsEnabledUpdate = true;
			timer.Start();
		}

		/// <summary>
		/// NullTransitioninfoを渡してオーバーロードメソッドを呼び出します.
		/// </summary>
		/// <param name="name"></param>
		public void Goto(string name) {
			Goto(name, NullTransitionInfo.INSTANCE);
		}

		//
		//イベントハンドラ
		//

		private void timerCallBack(object args, ElapsedEventArgs e) {
			this.IsEnabledUpdate = false;
			timer.Stop();
		}
	}
}
