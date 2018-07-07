using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shoot.Core.GameObject;
using XNA2D.Core.Utils;

namespace Shoot.Core {
	/// <summary>
	/// IFieldModelのデフォルト実装です.<br>
	/// Listへの委譲で実装されます。
	/// </summary>
	public class DefaultFieldModel : FieldModel {
		public event FieldModelChanged OnFieldModelChanged;

		public IGameObject this[int index] {
			get { return list[index]; }
		}

		public int Count {
			get { return list.Count; }
		}

		private List<IGameObject> list;

		public DefaultFieldModel() {
			this.list = new List<IGameObject>();
		}

		public void Add(IGameObject o) {
			list.Add(o);
			OnFieldModelChanged?.Invoke(this, new FieldModelEventArgs(o, FieldModelEventArgs.EventType.Add));
		}

		public void RemoveAt(int index) {
			IGameObject o = list[index];
			list.RemoveAt(index);
			OnFieldModelChanged?.Invoke(this, new FieldModelEventArgs(o, FieldModelEventArgs.EventType.Remove));
		}

		public void ForEach(Action<IGameObject> a) {
			ForEach((index, elem) => {
				a(elem);
				return false;
			});
		}

		public void ForEach(ListAction<IGameObject> a) {
			for(int i=0; i<list.Count; i++) {
				if(a(i, list[i])) {
					break;
				}
			}
		}
	}
}
