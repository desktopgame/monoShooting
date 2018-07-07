using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.Activity;
using Microsoft.Xna.Framework.Input;
using Shoot.Core.GameObject.Unit;
using XNA2D.Core.Utils;
using XNA2D.Core.UI;

namespace Shoot.Core.Activity {
	/// <summary>
	/// ゲーム画面.
	/// </summary>
	public class GameActivity : IActivity {
		/// <summary>
		/// タイトル画面からゲーム画面へ移行するときにプレイヤーが操作しないことを明示するために利用されるITransitionInfoのキーです.
		/// </summary>
		public static readonly string Demo = "Demo";	

		/// <summary>
		/// ポーズ画面からゲーム画面に戻るときにモデルを初期化する必要がないことを明示するために利用されるITransitionInfoのキーです.
		/// </summary>
		public static readonly string ReturnFromPause = "ReturnFromPause";

		/// <summary>
		/// ゲーム画面からポーズ画面へ移行するときにモデルを破棄する必要がないことを明示するために利用されるITransitionInfoのキーです.
		/// </summary>
		public static readonly string GotoPause = "GotoPause";

		/// <summary>
		/// ゲーム画面からポーズ画面へ移行するときに背景にゲーム画面を描画するためにこのオブジェクトのインスタンスを渡すのに利用されるITransitionInfoのキーです.
		/// </summary>
		public static readonly string Background = "Background";

		/// <summary>
		/// ゲーム画面からゲームオーバー画面へ移行するときにスコアを渡すのに利用されるITransitionInfoのキーです.
		/// </summary>
		public static readonly string Score = "Score";

		/// <summary>
		/// このアクティビティによって描画されるフィールド.
		/// </summary>
		public Field Field {
			private set; get;
		}

		private int oldWidth;
		private int oldHeight;
		private bool isDemo;
		private Schedule scheduleOfBlinkRate;
		private float alpha;
		private float vector;

		public GameActivity(Field field) {
			this.Field = field;
			this.scheduleOfBlinkRate = new Schedule(500f);
			this.alpha = 1f;
			this.vector = -0.01f;
		}
		
		public GameActivity() : this(new Field(new UserPlayer(), new DefaultFieldModel())) {
		}

		public void Initialize(ExGame game) {
			Field.Initialize(game);
		}

		public void LoadContent(ExGame game) {
			Field.LoadContent(game);
		}
		
		public void UnloadContent(ExGame game) {
			Field.UnloadContent(game);
		}

		public void Update(ExGame game, GameTime time) {
			KeyboardState s = Keyboard.GetState();
			if(s.IsKeyDown(Keys.Escape) && !isDemo) {
				DefaultTransitionInfo info = new DefaultTransitionInfo();
				info[Background] = this;
				info[GotoPause] = true;
				game.Goto("Pause", info);
				return;
			}
			if(isDemo && s.IsKeyDown(Keys.Enter)) {
				game.Goto("Title");
				this.isDemo = false;
			}
			Field.Update(game, time);
		}

		public void Clear(ExGame game) {
			game.GraphicsDevice.Clear(Color.Black);
		}

		public void Draw(ExGame game, GameTime time, SpriteBatch batch) {
			Field.Draw(game, time, batch);
			if(!isDemo) {
				return;
			}
			//半透明の矩形を描画
			Rectangle area = new Rectangle(0, 0, 200, 200);
			int gameWndW = game.GraphicsDeviceManager.PreferredBackBufferWidth;
			int gameWndH = game.GraphicsDeviceManager.PreferredBackBufferHeight;
			area.X = (gameWndW - area.Width) / 2;
			area.Y = (gameWndH - area.Height) / 2;
			DrawManager.GetCurrentCanvas(batch).FillRectangle(area, Color.Orange * alpha);
			//文字を描画
			string message = "デモ中です。\nEnter -> タイトルへ";
			SpriteFont font = FlyweightContents.GetInstance().Get<SpriteFont>("Font/MS_12");
			Vector2 fontSize = font.MeasureString(message);
			Vector2 drawPos = new Vector2((gameWndW - fontSize.X) / 2, (gameWndH - fontSize.Y) / 2);
			batch.DrawString(font, message, drawPos, Color.Black * alpha);
			//透明度を往復させる
			if((alpha >= 1.0f && vector == 0.01f) ||
			   (alpha <= 0.0f && vector == -0.01f)) {
				vector *= -1;
			}
			alpha += vector;
		}

		public void Show(ExGame game, ITransitionInfo prefs) {
			Field.Show(game, prefs);
			this.isDemo = prefs.ContainsKey(Demo) ? (bool)prefs[Demo] : false;
			this.oldWidth = game.GraphicsDeviceManager.PreferredBackBufferWidth;
			this.oldHeight = game.GraphicsDeviceManager.PreferredBackBufferHeight;
			game.GraphicsDeviceManager.PreferredBackBufferWidth = Field.Width;
			game.GraphicsDeviceManager.PreferredBackBufferHeight = Field.Height;
			game.GraphicsDeviceManager.ApplyChanges();
		}

		public void Hide(ExGame game, ITransitionInfo prefs) {
			Field.Hide(game, prefs);
			game.GraphicsDeviceManager.PreferredBackBufferWidth = oldWidth;
			game.GraphicsDeviceManager.PreferredBackBufferHeight = oldHeight;
			//game.GotoCenterOfScreen(game.GraphicsDeviceManager);
		}
	}
}
