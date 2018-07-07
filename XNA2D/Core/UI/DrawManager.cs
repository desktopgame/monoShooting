using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.UI;

namespace XNA2D.Core.UI {
	/// <summary>
	/// スプライトとそれを利用するキャンバスを紐づけて管理するクラス.
	/// </summary>
	public class DrawManager : IDisposable {
		private static DrawManager instance;
		private Dictionary<SpriteBatch, Canvas> dictionary;

		private DrawManager() {
			this.dictionary = new Dictionary<SpriteBatch, Canvas>();
		}

		/// <summary>
		/// 唯一のインスタンスを返します.
		/// </summary>
		/// <returns></returns>
		public static DrawManager GetInstance() {
			if(instance == null) {
				instance = new DrawManager();
			}
			return instance;
		}

		/// <summary>
		/// GetInstance().GetCanvas();
		/// </summary>
		/// <param name="batch"></param>
		/// <returns></returns>
		public static Canvas GetCurrentCanvas(SpriteBatch batch) {
			return GetInstance().GetCanvas(batch);
		}

		/// <summary>
		/// 一つのスプライトに対してキャンバスが複数生成されないようにインスタンスを共有します.
		/// </summary>
		/// <param name="batch"></param>
		/// <returns></returns>
		public Canvas GetCanvas(SpriteBatch batch) {
			if(!dictionary.ContainsKey(batch)) {
				dictionary[batch] = new Canvas(batch);
			}
			return dictionary[batch];
		}

		/// <summary>
		/// 全てのキャンバスを開放します.
		/// </summary>
		public void Dispose() {
			foreach(KeyValuePair<SpriteBatch, Canvas> collec in dictionary) {
				collec.Value.Dispose();
			}
			dictionary.Clear();
		}
	}
}
