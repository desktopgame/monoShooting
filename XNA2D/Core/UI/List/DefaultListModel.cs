using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI.List {
	/// <summary>
	/// ListModelのデフォルト実装です.
	/// </summary>
	public class DefaultListModel<T> : ListModel<T> {
		public event ListDataHandler OnListDataChanged;

		public T this[int index] {
			get { return list[index]; }
		}

		public int Count {
			get { return list.Count; }
		}

		private List<T> list;

		
		public DefaultListModel(params T[] elements) {
			if(elements != null) {
				this.list = new List<T>(elements);
			} else {
				this.list = new List<T>();
			}
		}

		public DefaultListModel() : this(null) {
		}

		/// <summary>
		/// 要素を末尾へ追加します.
		/// </summary>
		/// <param name="item"></param>
		public void AddItem(T item) {
			list.Add(item);
			OnListDataChanged?.Invoke(this, new ListDataEventArgs(list.Count - 1, list.Count - 1, ListDataEventArgs.EventType.Add));
		}

		/// <summary>
		/// 指定位置へ要素を挿入します.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		public void InsertItem(int index, T item) {
			list.Insert(index, item);
			OnListDataChanged?.Invoke(this, new ListDataEventArgs(index, index, ListDataEventArgs.EventType.Add));
		}

		/// <summary>
		/// 指定位置の要素を削除します.
		/// </summary>
		/// <param name="index"></param>
		public void RemoveItem(int index) {
			list.RemoveAt(index);
			OnListDataChanged?.Invoke(this, new ListDataEventArgs(index, index, ListDataEventArgs.EventType.Add));
		}
	}
}
