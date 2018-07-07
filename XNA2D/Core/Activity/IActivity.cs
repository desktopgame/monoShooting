using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Activity {
	/// <summary>
	/// 画面です.<br>
	/// 殆どのメソッドはExGameから委譲する形で呼び出されます。
	/// </summary>
	public interface IActivity {

		/// <summary>
		/// 初期化を行います.
		/// </summary>
		/// <param name="game"></param>
		void Initialize(ExGame game);

		/// <summary>
		/// 必要なリソースを確保します.
		/// </summary>
		/// <param name="game"></param>
		void LoadContent(ExGame game);

		/// <summary>
		/// 不要になったリソースを開放します.
		/// </summary>
		/// <param name="game"></param>
		void UnloadContent(ExGame game);

		/// <summary>
		/// フレームが経過するたびに呼ばれます.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="time"></param>
		void Update(ExGame game, GameTime time);

		/// <summary>
		/// 描画を行う直前に呼ばれます.<br>
		/// 画面を塗りつぶしてクリアするのに利用できます。<br>
		/// <pre>game.Graphics.Clear(Color.White);</pre>
		/// </summary>
		/// <param name="game"></param>
		void Clear(ExGame game);

		/// <summary>
		/// 描画を行うタイミングで呼ばれます.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="time"></param>
		/// <param name="batch"></param>
		void Draw(ExGame game, GameTime time, SpriteBatch batch);

		/// <summary>
		/// この画面が表示されて最初に呼ばれる処理です.<br>
		/// ゲームが開始した直後、他の画面から遷移した直後に呼ばれます.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="info">この画面に遷移するときに前の画面から渡されたパラメータ</param>
		void Show(ExGame game, ITransitionInfo info);

		/// <summary>
		/// この画面から別の画面に遷移するとき呼ばれる処理です.<br>
		/// <br>
		/// この画面を表示するためにグラフィックに対して行った変更を復元する場所でもあります。<br>
		/// 例えば、自身が全てのオブジェクトを表示するために大きめのウィンドウサイズを確保する場合、<br>
		/// <br>
		/// IActivity#Showで画面サイズをバッファしておき、当メソッドでその値に復元する必要があります。
		/// </summary>
		/// <param name="game"></param>
		/// <param name="info">この画面から遷移するときに次の画面に渡すパラメータ</param>
		void Hide(ExGame game, ITransitionInfo info);
	}
}
