using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace XNA2D.Core.Utils {

	/// <summary>
	/// コンテンツを使い回すクラスです.
	/// </summary>
	public class FlyweightContents {
		private static FlyweightContents instance;

		/// <summary>
		/// コンテンツのロードを監視するリスナーのリストです.
		/// </summary>
		public event ContentHandler OnLoaded;

		/// <summary>
		/// コンテンツのアンロードを監視するリスナーのリストです.
		/// </summary>
		public event ContentHandler OnUnloaded;
		

		private Dictionary<string, object> dictionary;

		private FlyweightContents() {
			this.dictionary = new Dictionary<string, object>();
		}

		/// <summary>
		/// 唯一のインスタンスを返します.
		/// </summary>
		/// <param name="game"></param>
		/// <returns></returns>
		public static FlyweightContents GetInstance() {
			if(instance == null) {
				instance = new FlyweightContents();
			}
			return instance;
		}
		
		/// <summary>
		/// 指定の名前のコンテンツが既に読み込まれていたらアクションを即座に実行します.<br>
		/// まだ読み込まれていなかったら読み込まれたタイミングで実行されます。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <param name="a"></param>
		public void Invoke<T>(string name, Action<T> a) {
			if(dictionary.ContainsKey(name)) {
				a((T)dictionary[name]);
				return;
			}
			OnLoaded += (sender, e) => {
				if(!e.Path.Equals(name)) {
					return;
				}
				a((T)e.Content);
			};
		}

		/// <summary>
		/// 指定の名前のコンテンツを返します.<br>
		/// まだ読み込まれていない場合は読み込みます。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public T Get<T>(ContentManager contentManager, string name) {
			object value;
			if(dictionary.TryGetValue(name, out value)) {
				return (T)value;
			}
			value = contentManager.Load<T>(name);
			dictionary[name] = value;
			OnLoaded?.Invoke(this, new ContentEventArgs(name, value, ContentEventArgs.EventType.Load));
			return (T)dictionary[name];
		}

		/// <summary>
		/// Game#Contentを参照します.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="game"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public T Get<T>(Game game, string name) {
			return Get<T>(game.Content, name);
		}

		/// <summary>
		/// 既に読み込まれているはずのコンテンツを返します.<br>
		/// Flyweight#Getとの違いは読み込まれていなかった場合に読み込みを行わない点です。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public T Get<T>(string name) {
			return (T)dictionary[name];
		}

		/// <summary>
		/// 指定のフォルダのファイルを全て特定の型で読み込みます.<br>
		/// <a href="http://stackoverflow.com/questions/12914002/how-to-load-all-files-in-a-folder-with-xna">How to load all files in a folder with XNA?</a>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="game"></param>
		/// <param name="contentFolder"></param>
		/// <exception cref="DirectoryNotFoundException"></exception>
		public void GetAll<T>(Microsoft.Xna.Framework.Game game, string contentFolder) {
			DirectoryInfo dir = new DirectoryInfo(game.Content.RootDirectory + "/" + contentFolder);
			if(!dir.Exists) {
				throw new DirectoryNotFoundException();
			}
//			string[] a = new string[] { "Colors", "" };
			FileInfo[] files = dir.GetFiles("*.*");
			foreach(FileInfo file in files) {
				string key = Path.GetFileNameWithoutExtension(file.Name);
				Get<T>(game, contentFolder + "/" + key);
			}
		}

		/// <summary>
		/// 指定の名前のコンテンツを破棄します.
		/// </summary>
		/// <param name="name"></param>
		public void Remove(string name) {
			object value = dictionary[name];
			dictionary.Remove(name);
			OnUnloaded?.Invoke(this, new ContentEventArgs(name, value, ContentEventArgs.EventType.Unload));
		}

		/// <summary>
		/// 全てのコンテンツを破棄します.
		/// </summary>
		public void Clear() {
			if(dictionary.Count == 0) {
				return;
			}
			foreach(string key in dictionary.Keys) {
				Remove(key);
				break;
			}
			Clear();
		}
	}
}
