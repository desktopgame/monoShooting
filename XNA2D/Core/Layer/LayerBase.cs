using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Layer {
	/// <summary>
	/// レイヤーの基底クラスです.
	/// </summary>
	public abstract class LayerBase : Layer {
		/// <summary>
		/// このレイヤーを描画するために割り当てられたクリッピング領域.
		/// </summary>
		public Rectangle Allocate {
			set; get;
		}

		/// <summary>
		/// アルファ値.
		/// </summary>
		public float Alpha {
			set; get;
		}

		/// <summary>
		/// 文字色(デフォルトはColor.Black).
		/// </summary>
		public Color Foreground {
			set; get;
		}

		/// <summary>
		/// 背景色(デフォルトはColor.White).
		/// </summary>
		public Color Background {
			set; get;
		}

		public LayerBase(Rectangle allocate, float alpha) {
			this.Allocate = allocate;
			this.Alpha = alpha;
			this.Foreground = Color.Black;
			this.Background = Color.White;
		}

		public LayerBase(GraphicsDeviceManager gdm, float alpha) : 
			this(new Rectangle(0, 0, gdm.PreferredBackBufferWidth, gdm.PreferredBackBufferHeight), alpha) {
		}

		public LayerBase(Rectangle allocate) : this(allocate, 1f) {
		}

		public override void Update(GameTime gameTime) {
		}
	}
}
