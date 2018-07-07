using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Shoot.Core.Activity;
using Shoot.Core.GameObject;
using Shoot.Core.GameObject.Generator;
using Shoot.Core.GameObject.Unit;
using System;
using XNA2D.Core.Activity;
using XNA2D.Core.UI;
using XNA2D.Core.UI.RangeBar;
using XNA2D.Core.Utils;
using XNA2D.Core.UI.Scroll;
using XNAActivity.Core.Utils;

namespace Shoot.Core {
	/// <summary>
	/// プレイヤーや敵ユニットが戦闘を行うフィールドです.
	/// </summary>
	public partial class Field {
		/// <summary>
		/// このフィールドが表示される画面.
		/// </summary>
		public ExGame Owner {
			private set; get;
		}

		/// <summary>
		/// プレイヤー.
		/// </summary>
		public IUnitPlayer Player {
			set; get;
		}

		/// <summary>
		/// 表示されるオブジェクトのリストです.
		/// </summary>
		public FieldModel Model {
			set; get;
		}

		/// <summary>
		/// 防衛ゲージのモデルです.
		/// </summary>
		public DefenseModel DefenceModel {
			set; get;
		}

		/// <summary>
		/// スコアのモデルです.
		/// </summary>
		public ScoreModel ScoreModel {
			set; get;
		}

		/// <summary>
		/// 敵を生成するインターフェイスです.
		/// </summary>
		public IGenerator Generator {
			set; get;
		}

		/// <summary>
		/// フィールドの横幅.
		/// </summary>
		public int Width {
			private set; get;
		}

		/// <summary>
		/// フィールドの縦幅.
		/// </summary>
		public int Height {
			private set; get;
		}
		private FrameTimer healIntervalTimer;
		private ScrollTexture backgroundLayerFront;
		private ScrollTexture backgroundLayerBack;
		private IBoss lastBoss;
		private RangeBar hpBar;
		private RangeBar energyBar;
		private RangeBar bomBar;
		private RangeBar defenceBar;
		private RangeBar bossBar;

		public Field(IUnitPlayer player, FieldModel model, IGenerator gen) {
			this.healIntervalTimer = FrameTimer.ForSecond(5);
			this.Generator = gen;
			this.Player = player;
			this.Model = model;
			this.DefenceModel = new DefaultDefenceModel(100);
			this.ScoreModel = new DefaultScoreModel();
			this.Width = 640;
			this.Height = 480;
			//UIの作成
			this.hpBar = new RangeBar();
			hpBar.Point.X = 32;
			hpBar.Point.Y = 16;
			hpBar.Size = hpBar.PreferredSize;
			hpBar.Foreground = Color.Red;
			hpBar.Background = Color.Blue;
			this.energyBar = new RangeBar();
			energyBar.Point.X = 32;
			energyBar.Point.Y = 48;
			energyBar.Size = energyBar.PreferredSize;
			energyBar.Foreground = Color.Yellow;
			energyBar.Background = Color.Blue;
			this.bomBar = new RangeBar();
			bomBar.Model.Maximum = Player.BomPointMax;
			bomBar.Model.Value = Player.BomPointValue;
			bomBar.Point.X = 32;
			bomBar.Point.Y = 80;
			bomBar.Size = bomBar.PreferredSize;
			bomBar.Foreground = Color.Orange;
			bomBar.Background = Color.Blue;
			this.defenceBar = new RangeBar();
			defenceBar.Model.Maximum = DefenceModel.Maximum;
			defenceBar.Model.Value = defenceBar.Model.Maximum;
			defenceBar.Point.X = 48 + hpBar.Size.Width;
			defenceBar.Point.Y = hpBar.Point.Y;
			defenceBar.Size = defenceBar.PreferredSize;
			defenceBar.Size.Width *= 2;
			defenceBar.Foreground = Color.Green;
			defenceBar.Background = Color.Blue;
			this.bossBar = new RangeBar();
			bossBar.Point.X = defenceBar.Point.X + defenceBar.Size.Width + 16;
			bossBar.Point.Y = hpBar.Point.Y;
			bossBar.Size = bossBar.PreferredSize;
			bossBar.Size.Width *= 2;
			bossBar.Foreground = Color.Red;
			bossBar.Background = Color.Blue;
		}
		
		public Field(IUnitPlayer player, FieldModel model) : this(player, model, new NormalGenerator()) {
		}

		/// <summary>
		/// フィールドを初期化します.
		/// <param name="game"></param>
		/// </summary>
		public void Initialize(ExGame game) {
			this.Owner = game;
		}

		/// <summary>
		/// 必要なコンテンツを読み込みます.
		/// </summary>
		/// <param name="game"></param>
		public void LoadContent(ExGame game) {
			FlyweightContents fc = FlyweightContents.GetInstance();
			fc.GetAll<Texture2D>(game, "Image");
			fc.GetAll<Texture2D>(game, "Image/Unit");
			fc.GetAll<Texture2D>(game, "Image/Animation");
			fc.GetAll<Texture2D>(game, "Image/Item");
			fc.GetAll<SoundEffect>(game, "Sound/Effect");
			fc.GetAll<SpriteFont>(game, "Font");
		//	fc.GetAll<Song>(game, "Sound/Song");
			Texture2D textureBack = fc.Get<Texture2D>("Image/backgroundLayerBack");
			Texture2D textureFront = fc.Get<Texture2D>("Image/backgroundLayerFront");
			this.backgroundLayerBack = new ScrollTexture(Width, Height, new HorizontalScroll(-1), textureBack);
			this.backgroundLayerFront = new ScrollTexture(Width, Height, new HorizontalScroll(-1), textureFront);
		}

		/// <summary>
		/// 不要になったコンテンツを開放します.
		/// </summary>
		/// <param name="game"></param>
		public void UnloadContent(ExGame game) {
		}

		/// <summary>
		/// オブジェクトを更新します.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="gameTime"></param>
		public void Update(ExGame game, GameTime gameTime) {
			//定期的に防衛ゲージを回復する
			if(healIntervalTimer.Update().IsElapsed()) {
				DefenceModel.Value = Math.Min(DefenceModel.Maximum, DefenceModel.Value + 1);
			}
			//定期的にオブジェクトを生成する
			foreach(IGameObject o in Generator.Generate(gameTime, this)) {
				Model.Add(o);
			}
			//全てのオブジェクトを更新.
			for(int i=Model.Count - 1; i>=0; i--) {
				IGameObject o = Model[i];
				o.Update(gameTime, this);
				if(o.IsDespawn) {
					Model.RemoveAt(i);
				}
			}
			Player.Update(gameTime, this);
		}

		/// <summary>
		/// オブジェクトを描画します.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		public void Draw(ExGame game, GameTime gameTime, SpriteBatch batch) {
			//背景を描画
			backgroundLayerBack.Scroll(gameTime);
			backgroundLayerBack.Draw(batch);
			backgroundLayerFront.Scroll(gameTime);
			backgroundLayerFront.Draw(batch);
			//HPとエネルギーとボムを描画
			hpBar.Model.Maximum = Player.HitPointMax;
			hpBar.Model.Value = Math.Max(0, Player.HitPointValue);
			hpBar.Draw(batch);
			energyBar.Model.Maximum = Player.EnergyPointMax;
			energyBar.Model.Value = Math.Max(0, Player.EnergyPointValue);
			energyBar.Draw(batch);
			bomBar.Model.Maximum = Player.BomPointMax;
			bomBar.Model.Value = Math.Max(0, Player.BomPointValue);
			bomBar.Draw(batch);
			//防衛ゲージを描画
			defenceBar.Model.Maximum = DefenceModel.Maximum;
			defenceBar.Model.Value = Math.Max(0, DefenceModel.Value);
			defenceBar.Draw(batch);
			//ボスの体力を描画
			if(lastBoss != null && !lastBoss.IsDespawn) {
				bossBar.Model.Maximum = lastBoss.HitPointMax;
				bossBar.Model.Value = Math.Max(0, lastBoss.HitPointValue);
				bossBar.Draw(batch);
			}
			//全てのオブジェクトを描画
			Model.ForEach(elem => elem.Draw(gameTime, batch, this));
			Player.Draw(gameTime, batch, this);
		}

		/// <summary>
		/// フィールドが可視化されると呼ばれます.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="prefs"></param>
		public void Show(ExGame game, ITransitionInfo prefs) {
			//ポーズ画面から戻ってきただけなら初期化しない
			object isReturnFromPause;
			if(!prefs.TryGetValue(GameActivity.ReturnFromPause, out isReturnFromPause) || !((bool)isReturnFromPause)) {
				object isDemo;
				this.lastBoss = null;
				prefs.TryGetValue(GameActivity.Demo, out isDemo);
				this.Model = new DefaultFieldModel();
				this.DefenceModel = new DefaultDefenceModel(100);
				this.ScoreModel = new DefaultScoreModel();
				this.Player = (isDemo != null && (bool)isDemo) ? (IUnitPlayer)new AIPlayer() : new UserPlayer();
				this.Player.PositionX = 0;
				this.Player.PositionY = Height / 2;
				this.Generator = new NormalGenerator();
			}
			Setup();
		}

		/// <summary>
		/// フィールドが非可視化されると呼ばれます.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="prefs"></param>
		public void Hide(ExGame game, ITransitionInfo prefs) {
			object isGotoPause;
			if(prefs.TryGetValue(GameActivity.GotoPause, out isGotoPause) && (bool)isGotoPause) {
				return;
			}
			Destroy();
		}
	}
}
