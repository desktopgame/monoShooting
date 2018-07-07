using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean.Core {
	/// <summary>
	/// プロパティの変更通知をサポートするオブジェクトで委譲先として利用可能なクラスです.
	/// </summary>
	public sealed class PropertyChangeSupport<T> {
		/// <summary>
		/// プロパティの変更を監視するリスナーのリストです.
		/// </summary>
		public event PropertyChangeHandler<T> OnPropertyChanged;

		/// <summary>
		/// プロパティを保持するオブジェクトです.
		/// </summary>
		public object Source {
			private set; get;
		}

		public PropertyChangeSupport(object source) {
			this.Source = source;
		}

		/// <summary>
		/// プロパティの変更を通知します.
		/// <pre>
		/// public class HogeBean {
		///		public int IntProperty {
		///			set {
		///				int old = IntProperty;
		///				this.intProperty = value;
		///				FirePropertyChange(old, value);
		///			}
		///			get{ return intProperty; }
		///		}
		///	}
		/// </pre>
		/// </summary>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		public void FirePropertyChange(T oldValue, T newValue) {
			OnPropertyChanged?.Invoke(Source, new PropertyChangeEventArgs<T>(oldValue, newValue));
		}
	}
}
