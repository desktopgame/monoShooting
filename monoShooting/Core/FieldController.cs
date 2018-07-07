using Microsoft.Xna.Framework.Audio;
using Shoot.Core.Activity;
using Shoot.Core.GameObject;
using Shoot.Core.GameObject.Animation;
using Shoot.Core.GameObject.Bullet;
using Shoot.Core.GameObject.Item;
using Shoot.Core.GameObject.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using XNA2D.Core.Activity;
using XNA2D.Core.Utils;

namespace Shoot.Core {
	/// <summary>
	/// フィールドのモデルを監視するコントローラです.
	/// </summary>
	partial class Field {
		/// <summary>
		/// モデルとコントローラを紐づけます.
		/// </summary>
		protected void Setup() {
			Player.OnHitPointValueChanged += OnHitPointValueChanged;
			Model.OnFieldModelChanged += OnFieldModelChanged;
			Thread.GetDomain().UnhandledException += OnUnhanldedException;
		}
		
		/// <summary>
		/// モデルとコントローラの紐づけを解除します.
		/// </summary>
		protected void Destroy() {
			this.lastBoss = null;
			Player.OnHitPointValueChanged -= OnHitPointValueChanged;
			Model.ForEach(elem => StopListen(elem));
			Model.OnFieldModelChanged -= OnFieldModelChanged;
			Thread.GetDomain().UnhandledException -= OnUnhanldedException;
		}

		//
		//イベントハンドラ
		//

		/// <summary>
		/// フィールドでオブジェクト増減すると呼ばれます.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFieldModelChanged(object sender, FieldModelEventArgs e) {
			IGameObject o = e.GameObject;
			FlyweightContents fc = FlyweightContents.GetInstance();
			//オブジェクトの追加
			if(e.Type.Equals(FieldModelEventArgs.EventType.Add)) {
				//ボスが現れた
				if(o is IBoss) {
					this.lastBoss = (IBoss)o;
				}
				StartListen(o);
			//オブジェクトの削除
			} else {
				StopListen(o);
			}
		}

		private void StartListen(IGameObject o) {
			//ユニットなら
			if(o is IUnit || o is Bullet) {
				((IUnit)o).OnHitPointValueChanged += OnHitPointValueChanged;
				((IUnit)o).OnDamage += OnDamage;
			}
			//敵ユニットなら
			if(o is IUnitEnemy) {
				((IUnitEnemy)o).OnGoaled += OnGoaled;
			}
		}
		
		private void StopListen(IGameObject o) {
			//ユニットなら
			if(o is IUnit || o is Bullet) {
				((IUnit)o).OnHitPointValueChanged -= OnHitPointValueChanged;
				((IUnit)o).OnDamage -= OnDamage;
			}
			//敵ユニットなら
			if(o is IUnitEnemy) {
				((IUnitEnemy)o).OnGoaled -= OnGoaled;
			}
		}

		/// <summary>
		/// ユニットの体力が増減すると呼ばれます.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnHitPointValueChanged(object sender, Bean.Core.PropertyChangeEventArgs<float> e) {
			FlyweightContents fc = FlyweightContents.GetInstance();
			//ダメージ
			if(e.OldValue > e.NewValue) {
				OnDownHPOfEntity(sender, e);
			//回復
			} else if(e.OldValue < e.NewValue) {
				fc.Get<SoundEffect>("Sound/Effect/heal").Play();
			}
		}

		/// <summary>
		/// エンティティのHPが減少すると呼ばれます.<br>
		/// 音を鳴らしたりします。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnDownHPOfEntity(object sender, Bean.Core.PropertyChangeEventArgs<float> e) {
			FlyweightContents fc = FlyweightContents.GetInstance();
			//音を鳴らす
			SoundEffect se = e.NewValue <= 0 ? fc.Get<SoundEffect>("Sound/Effect/death") : fc.Get<SoundEffect>("Sound/Effect/damage");
			se.Play();
			//死亡
			if(e.NewValue <= 0) {
				OnDieEntity(sender);
			}
		}

		/// <summary>
		/// エンティティのHPが0になると呼ばれます。<br>
		/// 爆発エフェクトを配置し、HPが0になったエンティティがプレイヤーならゲームオーバー画面へ遷移します。
		/// </summary>
		/// <param name="sender"></param>
		private void OnDieEntity(object sender) {
			//爆発エフェクトをおく
			Model.Add(new ExplodeAnimation((IGameObject)sender));
			//死亡したのがプレイヤーなら
			if(sender is IUnitPlayer) {
				GameOver();
			//死亡したのが敵なら
			} else if(sender is IUnitEnemy) {
				Item item = ((IUnitEnemy)sender).CreateDropItem();
				//運が悪いとドロップしないこともある
				if(item == null) {
					return;
				}
				item.PositionX = ((IGameObject)sender).PositionX;
				item.PositionY = ((IGameObject)sender).PositionY;
				Model.Add(item);
			}
		}

		/// <summary>
		/// 敵ユニットが画面左端へ到達すると呼ばれます.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnGoaled(object sender, EventArgs e) {
			DefenceModel.Value -= 3;
			if(DefenceModel.Value <= 0) {
				GameOver();
			}
		}

		/// <summary>
		/// ユニットがダメージを受けると呼ばれます.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnDamage(object sender, DamageEventArgs e) {
			if(!(e.Source.Attacker is IUnitPlayer) || !(e.Victim is IUnit)) {
				return;
			}
			Player.BomPointValue = Math.Min(Player.BomPointMax, Player.BomPointValue + 5);
		}

		/// <summary>
		/// 処理されなかった例外を処理します.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnUnhanldedException(object sender, UnhandledExceptionEventArgs e) {
			Exception ex = (Exception)e.ExceptionObject;
			MessageBox.Show(
				"エラーが発生しました。\n" +
				"Message=" + ex.Message +
				"StackTrace=" + ex.StackTrace
			);
			((ExGame)Owner).Goto("Title");
		}

		/// <summary>
		/// 音を鳴らして画面遷移.
		/// </summary>
		private void GameOver() {
			DefaultTransitionInfo info = new DefaultTransitionInfo();
			info[GameActivity.Score] = ScoreModel;
			FlyweightContents.GetInstance().Get<SoundEffect>("Sound/Effect/gameover").Play();
			Owner.EventQueue.PostEvent(() => {
				Owner.Goto("GameOver", info);
			});
		}
	}
}
