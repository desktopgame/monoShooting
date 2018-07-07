using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA2D.Core.Utils;

namespace XNA2D.Core.UI.Talk {
	/// <summary>
	/// 会話の流れを制御するコントローラ.
	/// 指定した文字列配列を会話として表示する
	/// </summary>
	public class TalkFlow {
		/// <summary>
		/// 文章のモデル
		/// </summary>
		public MutableTalkModel Model {
			set {
				if(model != null) {
					model.OnTalkUpdate -= OnTalkUpdate;
				}
				this.model = value;
				model.OnTalkUpdate += OnTalkUpdate;
			}
			get { return model; }
		}
		
		/// <summary>
		/// 表示される会話文
		/// </summary>
		public string[] Content {
			set {
				this.contents = value;
				this.Offset = 0;
			}
			get { return contents; }
		}

		/// <summary>
		/// 現在表示している会話文の位置.
		/// </summary>
		public int Offset {
			set {
				this.offset = value;
			}
			get { return offset; }
		}

		/// <summary>
		/// 会話文をループするか.
		/// </summary>
		public bool IsLoop {
			set; get;
		}

		/// <summary>
		/// 会話文を更新する間隔.
		/// </summary>
		public float FlowUpdateInterval {
			set {
				this.flowUpdateInterval = value;
				scheduleOfUpdateFlow.Interval = flowUpdateInterval;
			}
			get { return flowUpdateInterval; }
		}

		private MutableTalkModel model;
		private string[] contents;
		private int offset;
		private bool isComplete;
		private bool isBlocked;
		private float flowUpdateInterval;
		private Schedule scheduleOfUpdateFlow;
		private Detector keyDetector;

		public TalkFlow(MutableTalkModel model, string[] contents) {
			this.scheduleOfUpdateFlow = new Schedule();
			this.Model = model;
			this.Content = contents;
			this.FlowUpdateInterval = 1000f;
			this.keyDetector = Detector.GetInstance();
			this.isComplete = false;
			this.isBlocked = false;
		}

		/// <summary>
		/// 会話文を更新します.
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time) {
			if((!isComplete && !isBlocked)) {
				return;
			} else if(Offset >= Content.Length) {
				if(IsLoop) {
					this.Offset = 0;
				}
				return;
			}
			if(!isComplete && isBlocked && keyDetector.IsDetect(Keys.Enter)) {
				this.isBlocked = false;
				Model.Text = Content[Offset++];
				return;
			}
			if(isComplete) {
				scheduleOfUpdateFlow.Clear(time);
				this.isComplete = false;
				this.isBlocked = true;
			} else if(scheduleOfUpdateFlow.Update(time)) {
				this.isBlocked = false;
				Model.Text = Content[Offset++];
			}
		}

		//
		//イベントハンドラ
		//

		private void OnTalkUpdate(object sender, TalkModelEventArgs e) {
			if(!e.IsComplete) {
				return;
			} else if(e.IsComplete && !isComplete) {
				this.isComplete = true;
			}
		}
	}
}
