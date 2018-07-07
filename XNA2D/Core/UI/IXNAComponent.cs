using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2D.Core.UI {
	/// <summary>
	/// XNAの画面上で定期的に更新されるオブジェクトです.<br>
	/// XNA2D.Core.UIで提供されるコンポーネントは絶対座標でのレイアウト、レイアウトマネージャを利用した自動レイアウトの両方に対応するように実装されます。<br>
	/// <br>
	/// 実装クラスの描画メソッドは必ず、
	/// 直前にSpriteBatch#Begin、
	/// 直後にSpriteBatch#End	が呼ばれると想定して実装します。
	/// 例えば、下記のような実装なら正しく描画を行えます。
	/// spriteBatch.Begin();
	///		component.Draw(spriteBatch);
	///	spriteBatch.End();
	///	<br>
	///	コンポーネントを自動で更新/描画するには、一度コンテナに収容して、
	///	トップレベルのコンテナの更新/描画メソッドだけを呼ぶのが効率的です。
	///	或いは、XNAのDrawableGameComponent機能を利用する方法もあります。
	/// </summary>
	public interface IXNAComponent {
		/// <summary>
		/// フォーカス状態が変更されると呼ばれます.
		/// </summary>
		event EventHandler OnFocusChanged;

		/// <summary>
		/// このコンポーネントを包含しているコンテナを返します.<br>
		/// トップレベルのコンテナではnullを返します。
		/// </summary>
		IXNAContainer Parent {
			set; get;
		}

		/// <summary>
		/// 位置.
		/// </summary>
		XNAPoint Point {
			set; get;
		}

		/// <summary>
		/// 大きさ.
		/// </summary>
		XNASize Size {
			set; get;
		}

		/// <summary>
		/// 最小サイズ.<br>
		/// レイアウトマネージャはこれを厳守します.
		/// </summary>
		XNASize MinimumSize {
			set; get;
		}

		/// <summary>
		/// 推奨サイズ.<br>
		/// レイアウトマネージャによっては利用されることがあります。<br>
		/// <br>
		/// コンポーネントを限界まで引き延ばして表示するようなレイアウトでは利用されません。
		/// </summary>
		XNASize PreferredSize {
			set; get;
		}

		/// <summary>
		/// 最大サイズ.<br>
		/// レイアウトマネージャはこれを厳守します.
		/// </summary>
		XNASize MaximumSize {
			set; get;
		}

		/// <summary>
		/// 位置と大きさを一度に扱う.
		/// </summary>
		XNARectangle Bounds {
			set; get;
		}

		/// <summary>
		/// 文字色.
		/// </summary>
		Color Foreground {
			set; get;
		}

		/// <summary>
		/// 背景色.
		/// </summary>
		Color Background {
			set; get;
		}

		/// <summary>
		/// 背景を透過しないならtrue.
		/// </summary>
		bool IsOpaque {
			set; get;
		}

		/// <summary>
		/// フォーカスを持っているならtrue.<br>
		/// 原則、コンポーネントはフォーカスを持っていない限りユーザの入力を受け付けないように実装されます。
		/// </summary>
		bool HasFocus {
			set; get;
		}

		/// <summary>
		/// このコンポーネントのレイアウトが有効ならtrue.
		/// </summary>
		bool IsValid {
			get;
		}
		
		/// <summary>
		/// 更新します.
		/// </summary>
		/// <param name="time"></param>
		/// <param name="keyState"></param>
		/// <param name="mouseState"></param>
		void Update(GameTime time, KeyboardState keyState, MouseState mouseState);

		/// <summary>
		/// 描画します.
		/// </summary>
		/// <param name="canvas"></param>
		void Draw(Canvas canvas);

		/// <summary>
		///	このコンポーネント及び親コンテナを無効にします.<br>
		///	コンテナでは自身に設定されたレイアウトのキャッシュを破棄します(ILayoutManager#InvalidateLayout)
		/// </summary>
		void Invalidate();

		/// <summary>
		/// このコンポーネントを含むコンテナでレイアウトを再検証します.<br>
		/// 再検証はこのコンポーネント及びその下位コンポーネント全てに対して行われます。
		/// </summary>
		void Validate();

		/// <summary>
		/// このコンポーネントから最も近いIValidRootを実装するコンテナまでを無効にして、そのコンテナのLazyValidateを呼び出します。<br>
		/// レイアウトに関係するプロパティが変更されたときに自動で呼び出されます。<br>
		/// このメソッドは頻繁によばれるため、高速に実装されます。
		/// </summary>
		void Revalidate();
	}
}
