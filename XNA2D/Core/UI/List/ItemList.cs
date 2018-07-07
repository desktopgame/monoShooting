using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.Utils;
using XNA2D.Core.UI.List;
using XNAActivity.Core.UI;

namespace XNA2D.Core.UI.List {
	//NOTE:XBoxコントローラへの対応済み
	/// <summary>
	/// 項目を縦に並べるコンポーネントです.
	/// </summary>
	public class ItemList<T> : XNAComponentBase {
		/// <summary>
		/// アイテムの決定を監視するリスナーのリストです.
		/// </summary>
		public event ItemEnterHandler OnItemEnter;

		/// <summary>
		/// 表示される項目.
		/// </summary>
		public ListModel<T> Model {
			set {
				if(model != null) {
					model.OnListDataChanged -= OnListDataChanged;
				}
				this.model = value;
				model.OnListDataChanged += OnListDataChanged;
				Update();
			}
			get { return model; }
		}

		/// <summary>
		/// 選択されている項目.
		/// </summary>
		public SingleSelectionModel SelectionModel {
			set {
				if(selectionModel != null) {
					selectionModel.OnStateChanged -= OnStateChanged;
				}
				this.selectionModel = value;
				selectionModel.OnStateChanged += OnStateChanged;
			}
			get { return selectionModel; }
		}

		/// <summary>
		/// 表示される項目の数.
		/// </summary>
		public int VisibleRowCount {
			set {
				this.visibleRowCount = value;
				Update();
			}
			get { return visibleRowCount; }
		}

		/// <summary>
		/// 項目を描画するレンダラ.
		/// </summary>
		public ListCellRenderer<T> Renderer {
			set {
				this.renderer = value;
				Update();
			}
			get { return renderer; }
		}

		private ListModel<T> model;
		private SingleSelectionModel selectionModel;
		private ListCellRenderer<T> renderer;
		private int visibleRowCount;
		private Detector detector;

		public ItemList(ListModel<T> model, int visibleRowCount) : base() {
			this.Model = model;
			this.SelectionModel = new DefaultSingleSelectionModel(0);
			this.VisibleRowCount = visibleRowCount;
			this.detector = Detector.GetInstance();
		}

		public ItemList(T[] elements, int visibleRowCount) : this(new DefaultListModel<T>(elements), visibleRowCount) {
		}

		public ItemList(int visibleRowCount) : this(new DefaultListModel<T>(), visibleRowCount) {
		}

		/// <summary>
		/// キー入力を受け取って選択項目を移動.
		/// </summary>
		/// <param name="time"></param>
		/// <param name="keyState"></param>
		/// <param name="mouseState"></param>
		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
			int index = SelectionModel.SelectedIndex;
			//項目の移動
			if(!HasFocus) {
				return;
			}
			if(detector.IsDetect(Handle.UP)) {
				SelectionModel.SelectedIndex = index == 0 ? Model.Count - 1 : index - 1;
			} else if(detector.IsDetect(Handle.DOWN)) {
				SelectionModel.SelectedIndex = index == Model.Count - 1 ? 0 : index + 1;
			}
			//項目の決定
			if(detector.IsDetect(Handle.ENTER) && index != -1) {
				OnItemEnter?.Invoke(this, new ItemEnterEventArgs(index, Model[index]));
				return;
			}
		}

		/// <summary>
		/// レンダラに全てのアイテムを描画してもらう.
		/// </summary>
		/// <param name="canvas"></param>
		public override void Draw(Canvas canvas) {
			canvas.Clear(this);
			ListCellRenderer<T> renderer = Renderer;
			Tuple<int, int> range = GetVisibleRange();
			float y = Point.Y;
			for(int i=range.Item1; i<range.Item2; i++) {
				IXNAComponent cell = renderer.GetListCellComponent(this, i, model[i], HasFocus, SelectionModel.SelectedIndex == i);
				//FIXME:中央寄せしてるけどなんか別のプロパティでやるように変更する
				float x = Point.X + ((Size.Width - cell.Size.Width) / 2);
				cell.Point = new XNAPoint(x, y);
				cell.Size = cell.PreferredSize;
				cell.Draw(canvas);
				y += cell.Size.Height;
				//FIXME:これもSwingみたくなんか別のアプローチを考えておく
				//選択されてるなら矩形でボーダーっぽく
				if(SelectionModel.SelectedIndex == i) {
					canvas.DrawRectangle(cell.Bounds.ToRectangle(), Foreground);
				}
			}
		}

		//
		//推奨サイズの計算
		//

		/// <summary>
		/// モデルとレンダラを利用して推奨サイズを再計算します.
		/// </summary>
		protected virtual void Update() {
			ListCellRenderer<T> renderer = Renderer;
			ListModel<T> model = Model;
			if(renderer == null || model == null) {
				return;
			}
			XNASize size = new XNASize();
			Tuple<int, int> range = GetVisibleRange();
			for(int i = range.Item1; i < range.Item2; i++) {
				IXNAComponent c = renderer.GetListCellComponent(this, i, model[i], HasFocus, SelectionModel.SelectedIndex == i);
				c.Size = c.PreferredSize;
				if(size.Width < c.Size.Width) {
					size.Width = c.Size.Width;
				}
				size.Height += c.Size.Height;
			}
			PreferredSize = size;
		}

		/// <summary>
		/// 表示される範囲を返します.
		/// </summary>
		/// <returns>表示される最初の行/表示される最後の行</returns>
		protected Tuple<int, int> GetVisibleRange() {
			ListModel<T> model = Model;
			int selectedIndex = SelectionModel.SelectedIndex;
			int startIndex = Math.Max(SelectionModel.SelectedIndex, 0);
			int endIndex = Math.Min(model.Count, startIndex + VisibleRowCount);
			if(model.Count <= VisibleRowCount) {
				startIndex = 0;
			}
			return new Tuple<int, int>(startIndex, endIndex);
		}

		//
		//イベント
		//

		/// <summary>
		/// 選択されている項目の位置が変わったら大きさを再計算.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnStateChanged(object sender, EventArgs e) {
			Update();
		}

		/// <summary>
		/// 要素が増減したら大きさを再計算.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnListDataChanged(object sender, ListDataEventArgs e) {
			Update();
		}
	}
}
