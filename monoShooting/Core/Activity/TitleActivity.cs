using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Activity;
using XNA2D.Core.UI;
using XNA2D.Core.UI.Label;
using XNA2D.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.UI.List;
using Microsoft.Xna.Framework.Audio;
using XNA2D.Core.UI.RangeBar;

namespace Shoot.Core.Activity {
	/// <summary>
	/// タイトル画面.
	/// </summary>
	public class TitleActivity : Window {
		private string[] activities;
		private ItemList<string> itemList;
		private RangeBar rangeBar;
		private Schedule scheduleOfProgress;

		public TitleActivity() : base(new BorderLayout()) {
			this.activities = new string[]{ "Game", "Controller"};
			this.scheduleOfProgress = new Schedule(200f);
		}

		public override void Initialize(ExGame game) {
			game.Window.Title = "Shoot";
			game.GraphicsDeviceManager.PreferredBackBufferWidth = 640;
			game.GraphicsDeviceManager.PreferredBackBufferHeight = 480;
			game.GraphicsDeviceManager.ApplyChanges();
			ContentPane.Size.Width = 640;
			ContentPane.Size.Height = 480;
			ContentPane.Validate();
			base.Initialize(game);
		}

		public override void LoadContent(ExGame game) {
			//UIの構築
			FlyweightContents fc = FlyweightContents.GetInstance();
			SpriteFont font = fc.Get<SpriteFont>(game, "Font/MS_26");
			Label iconLabel = new Label(fc.Get<Texture2D>(game, "Image/title"));
			this.itemList = new ItemList<string>(new string[] { "Start Game", "Key Binding"}, 3);
			this.rangeBar = new RangeBar(new DefaultRangeModel(0, 0, 100));
			Panel itemWithRange = new Panel(new BorderLayout());
			itemList.Renderer = new DefaultListCellRenderer<string>(font);
			itemList.HasFocus = true;
			itemList.SelectionModel.OnStateChanged += OnStateChanged; 
			rangeBar.Foreground = Color.Yellow;
			rangeBar.Background = Color.Red;
			itemWithRange.Add(itemList, BorderLayout.North);
			itemWithRange.Add(rangeBar, BorderLayout.South);
			itemWithRange.Validate();
			ContentPane.Add(iconLabel, BorderLayout.North);
			ContentPane.Add(itemWithRange, BorderLayout.Center);
			ContentPane.Validate();
		}

		private void OnStateChanged(object sender, EventArgs e) {
			FlyweightContents.GetInstance().Get<SoundEffect>("Sound/Effect/select").Play();
			//項目が移動した=プレイヤーが操作したのでデモの待ち時間をリセット
			rangeBar.Model.Value = 0;
		}

		public override void Update(ExGame game, GameTime time) {
			KeyboardState s = Keyboard.GetState();
			//プログレスバーを進める
			if(scheduleOfProgress.Update(time)) {
				int len = s.IsKeyDown(Keys.Space) ? 10 : 2;
				RangeModel model = rangeBar.Model;
				model.Value = Math.Min(model.Maximum, Math.Max(0, model.Value + len));
			}
			//プログレスバーが満タンになったら
			if(rangeBar.Model.Value == rangeBar.Model.Maximum) {
				DefaultTransitionInfo info = new DefaultTransitionInfo();
				info[GameActivity.Demo] = true;
				game.Goto("Game", info);
				rangeBar.Model.Value = 0;
				return;
			}
			//エンターでゲーム画面へ
			if(s.IsKeyDown(Keys.Enter)) {
				game.Goto(activities[itemList.SelectionModel.SelectedIndex]);
				rangeBar.Model.Value = 0;
				return;
			} else if(s.IsKeyDown(Keys.Escape)) {
				return;
			}
			base.Update(game, time);
		}
	}
}
