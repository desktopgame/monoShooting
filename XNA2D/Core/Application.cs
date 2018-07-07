using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA2D.Core.Activity;

namespace XNA2D.Core {
	/// <summary>
	/// Mainメソッドを実装するクラスで継承してください.
	/// </summary>
	public abstract class Application {
		/// <summary>
		/// ゲームを起動します.<br>
		/// <pre>
		/// public static void Main(string[] args) {
		///		Program program = new Program();
		///		program.Launch(args);
		/// }
		/// </pre>
		/// </summary>
		/// <param name="args"></param>
		protected void Launch(string[] args) {
			ExGame game = CreateExGame();
			ActivityMap map = CreateActivityMap(game);
			try {
				game.PreInitialize(map);
				game.Run();
			} finally {
				game.Dispose();
			}
		}

		/// <summary>
		/// ゲームクラスを生成します.
		/// </summary>
		/// <returns></returns>
		protected virtual ExGame CreateExGame() {
			return new ExGame();
		}

		/// <summary>
		/// 画面遷移の構成を定義するマップを返します.
		/// </summary>
		/// <param name="game"></param>
		/// <returns></returns>
		protected abstract ActivityMap CreateActivityMap(ExGame game);
    }
}
