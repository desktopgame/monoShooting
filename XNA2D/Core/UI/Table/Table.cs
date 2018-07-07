using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA2D.Core.UI;

namespace XNA2D.Core.UI.Table {
	//FIXME:選択要素の概念を追加する
	/// <summary>
	/// 行と列で表されるデータを表示するコンポーネントです.
	/// </summary>
	public class Table : XNAComponentBase {
		/// <summary>
		/// 行方向の要素の概要を示すヘッダです.<br>
		/// nullのときは描画されません。
		/// </summary>
		public Header RowHeader {
			set {
				this.rowHeader = value;
				UpdatCellSize();
			}
			get{ return rowHeader; }
		}

		/// <summary>
		/// 列方向の要素の概要を示すヘッダです.<br>
		/// nullのときは描画されません。
		/// </summary>
		public Header ColumnHeader {
			set {
				this.columnHeader = value;
				UpdatCellSize();
			}
			get { return columnHeader; }
		}

		/// <summary>
		/// ヘッダを描画するレンダラです.<br>
		/// nullのときは描画されません。
		/// </summary>
		public HeaderCellRenderer HeaderRenderer {
			set {
				this.headerRenderer = value;
				UpdatCellSize();
			}
			get { return headerRenderer; }
		}

		/// <summary>
		/// 行と列に格納されるデータです.
		/// </summary>

		public TableModel Model {
			set {
				if(model != null) {
					model.OnDataChanged -= OnDataChanged;
				}
				this.model = value;
				model.OnDataChanged += OnDataChanged;
				UpdatCellSize();
			}
			get { return model; }
		}

		/// <summary>
		/// テーブルの項目を描画するレンダラです.
		/// </summary>
		public TableCellRenderer Renderer {
			set {
				this.renderer = value;
				UpdatCellSize();
			}
			get { return renderer; }
		}

		/// <summary>
		/// 行ヘッダがあるとき、左上にセルを描画するか.
		/// </summary>
		public bool IsCornerDraw {
			set {
				this.isCornerDraw = value;
				UpdatCellSize();
			}
			get { return isCornerDraw; }
		}
		
		private Header rowHeader;
		private Header columnHeader;
		private HeaderCellRenderer headerRenderer;
		private TableModel model;
		private TableCellRenderer renderer;
		private XNASize cellSize;
		private bool isCornerDraw;

		public Table(TableModel model) : base() {
			this.Model = model;
			this.IsCornerDraw = true;
		}
		
		public Table(object[,] values) : this(new DefaultTableModel(values)) {
		}
		
		public override void Update(GameTime time, KeyboardState keyState, MouseState mouseState) {
		}

		//
		//描画
		//

		public override void Draw(Canvas canvas) {
			canvas.Clear(this);
			DrawRowHeader(canvas);
			DrawColumnHeader(canvas);
			DrawContent(canvas);
		}

		/// <summary>
		/// 行ヘッダを描画します.
		/// </summary>
		/// <param name="canvas"></param>
		private void DrawRowHeader(Canvas canvas) {
			if(RowHeader == null || HeaderRenderer == null) {
				return;
			}
			//コーナーの描画
			if(IsCornerDraw) {
				IXNAComponent corner = HeaderRenderer.GetHeaderCellComponent(this, RowHeader, -1, null);
				corner.Point.X = Point.X;
				corner.Point.Y = Point.Y;
				corner.Size = cellSize;
				canvas.DrawRectangle(corner.Bounds.ToRectangle(), Foreground);
				corner.Draw(canvas);
			}
			//行ヘッダは一段下から開始する
			for(int i = 0; i < RowHeader.Count; i++) {
				object value = RowHeader[i];
				IXNAComponent component = HeaderRenderer.GetHeaderCellComponent(this, RowHeader, i, value);
				component.Point.X = Point.X;
				component.Point.Y = Point.Y + ((i + 1) * cellSize.Height);
				component.Size = cellSize;
				canvas.DrawRectangle(component.Bounds.ToRectangle(), Foreground);
				component.Draw(canvas);
			}
		}

		/// <summary>
		/// 列ヘッダを描画します.
		/// </summary>
		/// <param name="canvas"></param>
		private void DrawColumnHeader(Canvas canvas) {
			int columnStart = RowHeader != null && HeaderRenderer != null ? 1 : 0;
			if(ColumnHeader == null || HeaderRenderer == null) {
				return;
			}
			for(int i = 0; i < ColumnHeader.Count; i++) {
				object value = ColumnHeader[i];
				IXNAComponent component = HeaderRenderer.GetHeaderCellComponent(this, ColumnHeader, i, value);
				component.Point.X = Point.X + ((i + columnStart) * cellSize.Width);
				component.Point.Y = Point.Y;
				component.Size = cellSize;
				canvas.DrawRectangle(component.Bounds.ToRectangle(), Foreground);
				component.Draw(canvas);
			}
		}

		/// <summary>
		/// 項目を描画します.
		/// </summary>
		/// <param name="canvas"></param>
		private void DrawContent(Canvas canvas) {
			int rowStart = ColumnHeader != null && HeaderRenderer != null ? 1 : 0;
			int columnStart = RowHeader != null && HeaderRenderer != null ? 1 : 0;
			if(Renderer == null || Model == null) {
				return;
			}
			Model.ForEach((row, column, value) => {
				IXNAComponent component = Renderer.GetTableCellComponent(this, row, column, value, HasFocus, false);
				component.Point.X = Point.X + ((columnStart + column) * cellSize.Width);
				component.Point.Y = Point.Y + ((rowStart + row) * cellSize.Height);
				component.Size = cellSize;
				canvas.DrawRectangle(component.Bounds.ToRectangle(), Foreground);
				component.Draw(canvas);
				return false;
			});
		}

		//
		//推奨サイズの計算
		//

		/// <summary>
		/// モデルとレンダラを使用してセルサイズを再計算します.
		/// </summary>
		protected void UpdatCellSize() {
			XNASize contentSize = new XNASize();
			CheckHeaderSize(RowHeader, HeaderRenderer, contentSize);
			CheckHeaderSize(ColumnHeader, HeaderRenderer, contentSize);
			CheckContentSize(contentSize);
			this.cellSize = contentSize;
			UpdateComponentSize();
		}

		/// <summary>
		/// ヘッダのサイズを計算します.
		/// </summary>
		/// <param name="header"></param>
		/// <param name="renderer"></param>
		/// <param name="size"></param>
		private void CheckHeaderSize(Header header, HeaderCellRenderer renderer, XNASize size) {
			if(header == null || renderer == null) {
				return;
			}
			for(int i = 0; i < header.Count; i++) {
				IXNAComponent cell = renderer.GetHeaderCellComponent(this, header, i, header[i]);
				cell.Size = cell.PreferredSize;
				size.Width = Math.Max(size.Width, cell.Size.Width);
				size.Height = Math.Max(size.Height, cell.Size.Height);
			}
		}

		/// <summary>
		/// 項目のサイズを再計算します.
		/// </summary>
		/// <param name="size"></param>
		private void CheckContentSize(XNASize size) {
			if(Model == null || Renderer == null) {
				return;
			}
			Model.ForEach((row, column, value) => {
				IXNAComponent cell = Renderer.GetTableCellComponent(this, row, column, value, HasFocus, false);
				cell.Size = cell.PreferredSize;
				size.Width = Math.Max(size.Width, cell.Size.Width);
				size.Height = Math.Max(size.Height, cell.Size.Height);
				return false;
			});
		}

		/// <summary>
		/// セルサイズを使用してコンポーネントサイズを再計算します.
		/// </summary>
		protected void UpdateComponentSize() {
			XNASize size = new XNASize();
			if(RowHeader != null) {
				size.Width += cellSize.Width;
			}
			if(ColumnHeader != null) {
				size.Height += cellSize.Height;
			}
			if(Model != null) {
				size.Width += cellSize.Width * Model.ColumnCount;
				size.Height += cellSize.Height * Model.RowCount;
			}
			this.PreferredSize = size;
		}

		//
		//イベントハンドラ
		//

		/// <summary>
		/// モデルの要素が置換されると呼ばれます.<br>
		/// サイズを再計算します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnDataChanged(object sender, TableDataEventArgs e) {
			UpdatCellSize();
		}
	}
}
