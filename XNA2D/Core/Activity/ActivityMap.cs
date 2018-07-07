using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2D.Core.Activity {
	/// <summary>
	/// 画面遷移を監視するリスナーです.
	/// </summary>
	/// <param name="oldActivity"></param>
	/// <param name="newActivity"></param>
	public delegate void ActivityChangeHandler(IActivity oldActivity, IActivity newActivity);

	//FIXME:FIFO?リストかキューで管理するとしたら画面自体が自分の前の画面と次の画面を知っていることになる?
	/// <summary>
	/// 文字と画面を紐づけて画面遷移を行うマップです.
	/// </summary>
	public class ActivityMap {
		public event ActivityChangeHandler OnActivityChanged;

		/// <summary>
		/// 名前と画面を紐づけるプロパティです.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public IActivity this[string name] {
			set {
				dictionary[name] = value;
			}
			get {
				return dictionary[name];
			}
		}

		/// <summary>
		/// キー一覧.
		/// </summary>
		public Dictionary<string, IActivity>.KeyCollection Keys {
			get { return dictionary.Keys; }
		}
		
		/// <summary>
		/// アクティビティの描画メソッドを呼ぶ前に自動でbegin;するならtrue.
		/// </summary>
		public bool IsAutoBegin {
			set; get;
		}

		/// <summary>
		/// アクティビティの描画メソッドを呼ぶ前に自動でend;するならtrue.
		/// </summary>
		public bool IsAutoEnd {
			set; get;
		}

		private Dictionary<string, IActivity> dictionary;
		private Stack<IActivity> history;
		private IActivity current;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="defaultActivity"></param> デフォルトで表示されるページ
		public ActivityMap(string name, IActivity defaultActivity) {
			this.dictionary = new Dictionary<string, IActivity>();
			this.history = new Stack<IActivity>();
			this.dictionary[name] = defaultActivity;
			this.current = defaultActivity;
			this.IsAutoBegin = true;
			this.IsAutoEnd = true;
			history.Push(defaultActivity);
		}
		
		/// <summary>
		/// 登録された全ての画面を初期化します.
		/// </summary>
		/// <param name="game"></param>
		public void Initialize(ExGame game) {
			foreach(KeyValuePair<string, IActivity> pair in dictionary) {
				pair.Value.Initialize(game);
			}
		}

		/// <summary>
		/// 登録された全ての画面でコンテンツを読み込みます.
		/// </summary>
		/// <param name="game"></param>
		public void LoadContent(ExGame game) {
			foreach(KeyValuePair<string, IActivity> pair in dictionary) {
				pair.Value.LoadContent(game);
			}
		}

		/// <summary>
		/// 登録された全ての画面でコンテンツを開放します.
		/// </summary>
		/// <param name="game"></param>
		public void UnloadContent(ExGame game) {
			foreach(KeyValuePair<string, IActivity> pair in dictionary) {
				pair.Value.UnloadContent(game);
			}
		}

		/// <summary>
		/// 画面を更新します.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="time"></param>
		public void Update(ExGame game, GameTime time) {
			current.Update(game, time);
		}

		/// <summary>
		/// 画面を描画します.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="time"></param>
		/// <param name="spriteBatch"></param>
		public void Draw(ExGame game, GameTime time, SpriteBatch spriteBatch) {
			current.Clear(game);
			if(IsAutoBegin) spriteBatch.Begin();
				current.Draw(game, time, spriteBatch);
			if(IsAutoEnd) spriteBatch.End();
		}

		//
		//画面遷移
		//

		/// <summary>
		/// 直前の画面に戻ります.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="info"></param>
		public void Return(ExGame game, ITransitionInfo info) {
			Goto(game, history.Pop(), info, false);
		}

		/// <summary>
		/// 指定の名前に紐づけられた画面に遷移します.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="name"></param>
		/// <param name="info"></param>
		public void Goto(ExGame game, string name, ITransitionInfo info) {
			Goto(game, name, info, true);
		}

		/// <summary>
		/// 指定の画面に遷移します.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="activity"></param>
		/// <param name="info"></param>
		private void Goto(ExGame game, IActivity activity, ITransitionInfo info) {
			throw new NotImplementedException();
			//Goto(game, activity, info, true);
		}

		private void Goto(ExGame game, IActivity activity, ITransitionInfo info, bool enq) {
			OnActivityChanged?.Invoke(current, activity);
			IActivity old = this.current;
			old?.Hide(game, info);

			this.current = activity;
			current.Show(game, info);

			if(enq) {
				history.Push(old);
			}
		}

		private void Goto(ExGame game, string name, ITransitionInfo info, bool enq) {
			//IActivity old = this.current;
			//this.current = dictionary[name];
			//OnActivityChanged?.Invoke(old, current);
			//old?.Hide(game, info);
			//current.Show(game, info);
			//if(enq) {
			//	history.Push(old);
			//}
			Goto(game, dictionary[name], info, enq);
		}
	}
}
