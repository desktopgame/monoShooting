using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.Layer {
	/// <summary>
	/// 連結リストの構造を持つレイヤです.
	/// </summary>
	public abstract class Layer {
		/// <summary>
		/// このレイヤーの直前のレイヤー.
		/// </summary>
		public Layer Prev {
			private set; get;
		}

		/// <summary>
		/// このレイヤーの直後のレイヤー.
		/// </summary>
		public Layer Next {
			private set; get;
		}

		public Layer() {
			this.Prev = null;
			this.Next = null;
		}

		/// <summary>
		/// レイヤーを追加します.
		/// </summary>
		/// <param name="layer"></param>
		public void Append(Layer layer) {
			if(Next == null) {
				this.Next = layer;
				layer.Prev = this;
				return;
			}
			Next.Append(layer);
		}

		/// <summary>
		/// この要素をレイヤから削除します.
		/// </summary>
		/// <param name="children">このレイヤ以降のレイヤも削除するならtrue</param>
		public void Remove(bool children) {
			if(Prev == null) {
				throw new InvalidOperationException();
			}
			if(Prev.Next != null && !Prev.Next.Equals(this)) {
				throw new InvalidOperationException();
			}
			if(children) {
				Prev.Next = null;
				Next = null;
			} else {
				if(Next == null) {
					Prev.Next = null;
					return;
				}
				Layer left = Prev;
				Layer right = Next;
				while(left != null && right != null) {
					right.Prev = left;
					left.Next = right;
					left = left.Next;
					right = right.Next;
				}
			}
		}

		/// <summary>
		/// 全てのレイヤを訪問します.
		/// </summary>
		/// <param name="a"></param>
		public void ForEach(Action<Layer> a) {
			Layer root = this;
			while(root != null) {
				a(root);
				root = root.Next;
			}
		}

		/// <summary>
		/// このレイヤを更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		public abstract void Update(GameTime gameTime);

		/// <summary>
		/// このレイヤから最後まで更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		public void UpdateAll(GameTime gameTime) {
			ForEach(elem => elem.Update(gameTime));
		}

		/// <summary>
		/// このレイヤを描画します.<br>
		/// 描画中にこのレイヤを削除することは許可されます。
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		public abstract void Draw(GameTime gameTime, SpriteBatch batch);

		/// <summary>
		/// このレイヤから最後まで描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		public void DrawAll(GameTime gameTime, SpriteBatch batch) {
			ForEach(elem => elem.Draw(gameTime, batch));
		}
	}
}
