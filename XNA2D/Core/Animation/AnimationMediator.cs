using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Animation {
	/// <summary>
	/// 名前とアニメーションを紐づけて管理するクラスです.
	/// </summary>
	public class AnimationMediator {
		/// <summary>
		/// アニメーションの変更を監視するリスナーのリストです.
		/// </summary>
		public event AnimationHandler OnAnimationChanged;
		
		private Dictionary<string, IAnimationFrameFactory> factories;
		private Dictionary<string, List<AnimationInstance>> instances;

		public AnimationMediator() {
			this.factories = new Dictionary<string, IAnimationFrameFactory>();
			this.instances = new Dictionary<string, List<AnimationInstance>>();
		}

		//
		//アニメーションの登録/削除
		//

		/// <summary>
		/// 指定のIDとアニメーションを紐づけます.<br>
		/// アニメーションそのものではなくそのファクトリを紐づけるのは、同じ種類のアニメーションを並列して再生したい場合に対応するためです。
		/// </summary>
		/// <param name="name"></param>
		/// <param name="f"></param>
		public void AddAnimation(string name, IAnimationFrameFactory f) {
			this.factories[name] = f;
		}

		/// <summary>
		/// 指定のIDとそれに紐づけられたアニメーションを削除します.
		/// </summary>
		/// <param name="name"></param>
		public void RemoveAnimation(string name) {
			factories.Remove(name);
		}

		//FIXME:AbstractFactory
		/// <summary>
		/// アニメーションインスタンスを生成します.
		/// </summary>
		/// <param name="frame"></param>
		/// <returns></returns>
		protected virtual AnimationInstance CreateAnimationInstance(IAnimationFrame frame) {
			return new AnimationInstance(this, frame);
		}

		//
		//アニメーションの開始/終了
		//

		/// <summary>
		/// パラメータからアニメーションを生成し、リストに追加します.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public AnimationInstance Begin(string name, params object[] args) {
			IAnimationFrame frame = factories[name].Create(args);
			AnimationInstance instance = CreateAnimationInstance(frame);
			if(!instances.ContainsKey(name)) {
				instances[name] = new List<AnimationInstance>();
			}
			instance.Begin();
			instances[name].Add(instance);
			OnAnimationChanged?.Invoke(this, new AnimationEventArgs(name, AnimationEventArgs.Type.Begin));
			return instance;
		}

		/// <summary>
		/// パラメータの必要ないアニメーションを生成し、リストに追加します.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public AnimationInstance Begin(string name) {
			return Begin(name, null);
		}

		/// <summary>
		/// リストに追加されたアニメーションを更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime) {
			List<string> buf = new List<string>();
			foreach(KeyValuePair<string, List<AnimationInstance>> pair in instances) {
				pair.Value.ForEach(elem => {
					elem.Update(gameTime);
					OnAnimationChanged?.Invoke(this, new AnimationEventArgs(pair.Key, AnimationEventArgs.Type.Update));
				});
				for(int i=pair.Value.Count-1; i>=0; i--) {
					if(!pair.Value[i].Frame.HasNext) {
						pair.Value[i].End(buf);
						pair.Value.RemoveAt(i);
						OnAnimationChanged?.Invoke(this, new AnimationEventArgs(pair.Key, AnimationEventArgs.Type.End));
					}
				}
			}
			buf.ForEach(elem => Begin(elem));
		}

		/// <summary>
		/// リストに追加されたアニメーションを描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		public void Draw(GameTime gameTime, SpriteBatch batch) {
			foreach(KeyValuePair<string, List<AnimationInstance>> pair in instances) {
				pair.Value.ForEach(elem => {
					elem.Draw(gameTime, batch);
				});
			}
		}

		/// <summary>
		/// リストに追加されたアニメーションのうち指定の名前と紐づけられたアニメーションの終了処理(End)を実行します.
		/// </summary>
		/// <param name="name"></param>
		public void End(string name) {
			if(!instances.ContainsKey(name)) {
				return;
			}
			List<AnimationInstance> instancesList = instances[name];
			List<string> buf = new List<string>();
			instancesList.ForEach(elem => {
				elem.End(buf);
				OnAnimationChanged?.Invoke(this, new AnimationEventArgs(name, AnimationEventArgs.Type.End));
			});
			buf.ForEach(elem => Begin(elem));
			instancesList.Clear();
		}

		/// <summary>
		/// リストに追加された全てのアニメーションを終了します.
		/// </summary>
		public void End() {
			foreach(KeyValuePair<string, List<AnimationInstance>> pair in instances) {
				End(pair.Key);
			}
		}

		//
		//プロパティ
		//
		
		private bool IsPlaying(string name, Func<AnimationInstance, bool> condition) {
			if(!instances.ContainsKey(name)) {
				return false;
			}
			List<AnimationInstance> li = instances[name];
			for(int i = 0; i < li.Count; i++) {
				AnimationInstance elem = li[i];
				if(condition(elem)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 指定の名前に紐づけられたアニメーションのうち再生中のものがあるかどうか.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="pause">ポーズ中でもまだフレームに続きがあるなら再生中であると判断するならtrue</param>
		/// <returns></returns>
		public bool IsPlaying(string name, bool isPause=true) {
			return IsPlaying(name, (elem) => {
				return (isPause ? elem.Frame.HasNext : elem.IsPlaying);
			});
		}

		/// <summary>
		/// なんらかのアニメーションが再生中であるかどうか.
		/// </summary>
		/// <param name="pause">ポーズ中でもまだフレームに続きがあるなら再生中であると判断するならtrue</param>
		/// <returns></returns>
		public bool IsPlaying(bool isPause=true) {
			foreach(KeyValuePair<string, List<AnimationInstance>> pair in instances) {
				if(IsPlaying(pair.Key, isPause)) {
					return true;
				}
			}
			return false;
		}
	}
}
