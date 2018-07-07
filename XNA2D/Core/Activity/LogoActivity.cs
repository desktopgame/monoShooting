using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA2D.Core.Utils;

namespace XNA2D.Core.Activity {
	/// <summary>
	/// ロゴを表示してから次の画面へ遷移する実装です.
	/// </summary>
	public class LogoActivity : IActivity {

		/// <summary>
		/// ロゴ一つあたりの表示時間.
		/// </summary>
		public float Interval {
			set {
				this.interval = value;
				if(scheduleOfLogoChange == null) {
					this.scheduleOfLogoChange = new Schedule(interval);
				} else {
					this.scheduleOfLogoChange.Interval = interval;
				}
			}
			get { return interval; }
		}

		/// <summary>
		/// 表示されるロゴの一覧です.
		/// </summary>
		public LogoCollection Collection {
			private set; get;
		}

		private float interval;
		private string next;
		private int offset;
		private Schedule scheduleOfLogoChange;
		

		public LogoActivity(string next, float interval) {
			this.Interval = interval;
			this.next = next;
			this.offset = 0;
			this.Collection = new LogoCollection();
		}

		public LogoActivity(string next) : this(next, 1000) {
		}
		

		public void Initialize(ExGame game) {
		}

		public void LoadContent(ExGame game) {
			Collection.ForEach(elem => elem.LoadContent(game));
		}

		public void UnloadContent(ExGame game) {
			Collection.ForEach(elem => elem.UnloadContent(game));
		}

		public void Update(ExGame game, GameTime time) {
			if(offset >= Collection.Count) {
				game.Goto(next);
				return;
			}
			if(!Collection[offset].HasNext) {
				this.offset++;
			}
		}

		public void Clear(ExGame game) {
			game.GraphicsDevice.Clear(Color.Black);
		}

		public void Draw(ExGame game, GameTime time, SpriteBatch batch) {
			if(offset >= Collection.Count) {
				return;
			}
			Collection[offset].Draw(time, batch);
		}

		public void Show(ExGame game, ITransitionInfo info) {
		}

		public void Hide(ExGame game, ITransitionInfo info) {
		}
	}

	/// <summary>
	/// ロゴをリスト形式で格納するクラスです.
	/// </summary>
	public class LogoCollection {
		private List<ILogo> impl;

		/// <summary>
		/// 指定位置のロゴ.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public ILogo this[int index] {
			set { impl[index] = value; }
			get { return impl[index]; }
		}

		/// <summary>
		/// ロゴの数を返します.
		/// </summary>
		public int Count {
			get { return impl.Count; }
		}

		public LogoCollection() {
			this.impl = new List<ILogo>();
		}
	
		/// <summary>
		/// ロゴを末尾に追加します.
		/// </summary>
		/// <param name="logo"></param>
		public void Add(ILogo logo) {
			impl.Add(logo);
		}	

		/// <summary>
		/// 指定位置にロゴを挿入します.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="logo"></param>
		public void Insert(int index, ILogo logo) {
			impl.Insert(index, logo);
		}

		/// <summary>
		/// 指定のロゴを削除します.
		/// </summary>
		/// <param name="logo"></param>
		public void Remove(ILogo logo) {
			impl.Remove(logo);
		}

		/// <summary>
		/// 指定位置のロゴを削除します.
		/// </summary>
		/// <param name="index"></param>
		public void RemoveAt(int index) {
			impl.RemoveAt(index);
		}

		/// <summary>
		/// 条件を満たすロゴを削除します.
		/// </summary>
		/// <param name="condition"></param>
		public void RemoveAll(Predicate<ILogo> condition) {
			impl.RemoveAll(condition);
		}

		/// <summary>
		/// 全ての要素を訪問します.
		/// </summary>
		/// <param name="visitAction"></param>
		public void ForEach(Action<ILogo> visitAction) {
			impl.ForEach(visitAction);
		}
	}

	/// <summary>
	/// 一定時間表示されるロゴの描画を提供します.
	/// </summary>
	public interface ILogo {
		/// <summary>
		/// もう描画すべきフレームが無ければfalse1を返します.
		/// </summary>
		bool HasNext {
			get;
		}

		/// <summary>
		/// ロゴの表示に必要なリソースを読み込みます.
		/// </summary>
		/// <param name="game"></param>
		void LoadContent(Game game);

		/// <summary>
		/// ロゴの表示に必要なリソースを開放します.
		/// </summary>
		/// <param name="game"></param>
		void UnloadContent(Game game);

		/// <summary>
		/// ロゴを描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		void Draw(GameTime gameTime, SpriteBatch batch);
	}

	/// <summary>
	/// フェードアウトするロゴの基底クラス.
	/// </summary>
	public abstract class FadeLogo : ILogo {
		private float alpha;

		public FadeLogo() {
			this.alpha = 1f;
		}

		public bool HasNext {
			get { return alpha > 0f; }
		}

		public void Draw(GameTime gameTime, SpriteBatch batch) {
			Draw(gameTime, batch, alpha -= 0.01f);
		}

		public abstract void LoadContent(Game game);
		public abstract void UnloadContent(Game game);

		/// <summary>
		/// 描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		/// <param name="alpha"></param>
		protected abstract void Draw(GameTime gameTime, SpriteBatch batch, float alpha);
	}

	/// <summary>
	/// 画像をウィンドウ中心に表示する実装です.
	/// </summary>
	public class TextureLogo : FadeLogo {
		private GraphicsDeviceManager graphicsDeviceManager;
		private Texture2D texture;
		private string path;
		private Color color;


		public TextureLogo(GraphicsDeviceManager graphicsDeviceManager, string path, Color color) : base() {
			this.graphicsDeviceManager = graphicsDeviceManager;
			this.path = path;
			this.color = color;
		}

		public TextureLogo(GraphicsDeviceManager graphicsDeviceManager, string path) : this(graphicsDeviceManager, path, Color.White) {
		}

		protected override void Draw(GameTime gameTime, SpriteBatch batch, float alpha) {
			DrawUtils.DrawImage(graphicsDeviceManager, batch, texture, Color.White * alpha);
		}

		public override void LoadContent(Game game) {
			this.texture = game.Content.Load<Texture2D>(path);
		}

		public override void UnloadContent(Game game) {
		}
	}

	/// <summary>
	/// 文字をウィンドウ中心に表示する実装です.
	/// </summary>
	public class TextLogo : FadeLogo {
		private GraphicsDeviceManager graphicsDeviceManager;
		private string path;
		private string text;
		private Color color;
		private SpriteFont font;

		public TextLogo(GraphicsDeviceManager graphicsDeviceManager, string path, string text, Color color) : base() {
			this.graphicsDeviceManager = graphicsDeviceManager;
			this.path = path;
			this.text = text;
			this.color = color;
		}

		public TextLogo(GraphicsDeviceManager graphicsDeviceManager, string path, string text) : this(graphicsDeviceManager, path, text, Color.Black) {
		}

		public override void LoadContent(Game game) {
			this.font = game.Content.Load<SpriteFont>(path);
		}

		public override void UnloadContent(Game game) {
		}

		protected override void Draw(GameTime gameTime, SpriteBatch batch, float alpha) {
			DrawUtils.DrawString(graphicsDeviceManager, batch, font, text, color * alpha);
		}
	}

	/// <summary>
	/// 文字と矩形をウィンドウ中心に表示する実装です.
	/// </summary>
	public class SimpleLogo : FadeLogo {
		private GraphicsDeviceManager graphicsDeviceManager;
		private string path;
		private string text;
		private Color foreground;
		private Color background;
		private SpriteFont font;
		private float rectWidth;
		private float rectHeight;

		public SimpleLogo(GraphicsDeviceManager graphicsDeviceManager, string path, string text, Color foreground, Color background, float rectWidth = -1f, float rectHeight = -1f) : base() {
			this.graphicsDeviceManager = graphicsDeviceManager;
			this.path = path;
			this.text = text;
			this.foreground = foreground;
			this.background = background;
			this.rectWidth = rectWidth;
			this.rectHeight = rectHeight;
		}

		protected override void Draw(GameTime gameTime, SpriteBatch batch, float alpha) {
			Vector2 size = font.MeasureString(text);
			int rectWidthR = (int)Math.Max(size.X, rectWidth);
			int rectHeightR = (int)Math.Max(size.Y, rectHeight);
			Rectangle rect = new Rectangle(0, 0, rectWidthR, rectHeightR);
			DrawUtils.FillRect(graphicsDeviceManager, batch, rect, background * alpha);
			DrawUtils.DrawString(graphicsDeviceManager, batch, font, text, foreground * alpha);
		}

		public override void LoadContent(Game game) {
			this.font = game.Content.Load<SpriteFont>(path);
		}

		public override void UnloadContent(Game game) {
		}
	}
}
