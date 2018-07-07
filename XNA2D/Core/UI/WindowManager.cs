using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Activity;

namespace XNAActivity.Core.UI {
	/// <summary>
	/// ウィンドウサイズの保存/復元をサポートするクラスです.
	/// </summary>
	public class WindowManager {
		/// <summary>
		/// 現在の横幅.
		/// </summary>
		public int Width {
			private set; get;
		}

		/// <summary>
		/// 現在の縦幅.
		/// </summary>
		public int Height {
			private set; get;
		}

		private static WindowManager instance;

		/// <summary>
		/// 唯一のインスタンスを返します.
		/// </summary>
		/// <returns></returns>
		public static WindowManager GetInstance() {
			if(instance == null) {
				instance = new WindowManager();
			}
			return instance;
		}

		/// <summary>
		/// サイズを保存します.
		/// </summary>
		/// <param name="g"></param>
		public void Store(ExGame g) {
			this.Width = g.GraphicsDeviceManager.PreferredBackBufferWidth;
			this.Height = g.GraphicsDeviceManager.PreferredBackBufferHeight;
		}

		/// <summary>
		/// サイズを保存してから現在のサイズを変更します.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void Commit(ExGame g, int width, int height) {
			Store(g);
			g.GraphicsDeviceManager.PreferredBackBufferWidth = width;
			g.GraphicsDeviceManager.PreferredBackBufferHeight = height;
			g.WindowToCenter();
		}

		/// <summary>
		/// 保存済みのサイズを復元します.
		/// </summary>
		/// <param name="g"></param>
		public void Rollback(ExGame g) {
			if(Width == 0 || Height == 0) {
				return;
			}
			int cWidth = g.GraphicsDeviceManager.PreferredBackBufferWidth;
			int cHeight = g.GraphicsDeviceManager.PreferredBackBufferHeight;
			if(cWidth == Width && cHeight == Height) {
				g.WindowToCenter();
				return;
			}
			g.GraphicsDeviceManager.PreferredBackBufferWidth = Width;
			g.GraphicsDeviceManager.PreferredBackBufferHeight = Height;
			g.WindowToCenter();
		}
	}
}
