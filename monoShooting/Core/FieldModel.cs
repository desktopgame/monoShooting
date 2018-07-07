using Bean.Core;
using Shoot.Core.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace Shoot.Core {
	/// <summary>
	/// フィールドに存在するゲームオブジェクトをリスト形式で管理するモデル.
	/// </summary>
	public interface FieldModel {
		/// <summary>
		/// このモデルの要素の増減を監視するリスナーのリストです.
		/// </summary>
		event FieldModelChanged OnFieldModelChanged;

		/// <summary>
		/// 指定位置のゲームオブジェクトを返します.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		IGameObject this[int index] {
			get;
		}

		/// <summary>
		/// 要素数を返します.
		/// </summary>
		int Count {
			get;
		}

		/// <summary>
		/// 指定のオブジェクトを追加します.
		/// </summary>
		/// <param name="o"></param>
		void Add(IGameObject o);

		/// <summary>
		/// 指定位置のオブジェクトを削除します.
		/// </summary>
		/// <param name="index"></param>
		void RemoveAt(int index);

		/// <summary>
		/// 全ての要素を訪問します.
		/// </summary>
		/// <param name="a"></param>
		void ForEach(Action<IGameObject> a);

		/// <summary>
		/// 全ての要素を訪問します.
		/// </summary>
		/// <param name="a"></param>
		void ForEach(ListAction<IGameObject> a);
	}
}
