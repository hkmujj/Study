using System;
using System.Drawing;
using ATP200C.MainView;
using ATP200C.PublicComponents;
using CommonUtil.Util;
using MMI.Facility.Interface.Data;

namespace ATP200C.FunctionButton
{
    public class ButtonInfoExtension
    {
        
    }

    /// <summary>
    /// 按钮信息
    /// </summary>
    public class ButtonInfo
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="funBtn">按钮结构体</param>
        public ButtonInfo(FunBtnStruct funBtn)
        {
            FunButton = funBtn;
            _initRectangleF = BtnRects[(int)funBtn.BtnTypeName];
            if (!string.IsNullOrWhiteSpace(funBtn.ButtonEventHandlerName))
            {
                try
                {
                    var type = Type.GetType(funBtn.ButtonEventHandlerName);
                    if (type != null)
                    {
                        ButtonEventHandler = Activator.CreateInstance(type) as IButtonEventHandler;
                        if (ButtonEventHandler == null)
                        {
                            LogMgr.Warn(string.Format("{0} is not an class which is inherit from {1}", type, typeof (IButtonEventHandler)));
                        }
                    }
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("Can not create instance of type , name {0}.  {1}", funBtn.ButtonEventHandlerName, e));
                }
            }
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="gs"></param>
        public void DrawButtonInfo(Graphics gs)
        {
            if (FunButton.BtnImage != null)
            {
                gs.DrawImage(FunButton.BtnImage, _initRectangleF);
            }

            if (!string.IsNullOrEmpty(FunButton.BtnContent))
                gs.DrawString(FunButton.BtnContent, _normalFont, Locked ? _lockedNameBrush : _normalNameBrush, 
                    _initRectangleF, FontsItems.TheAlignment(FontRelated.居中));
            //外边框
            gs.DrawRectangle(_normalPen, Rectangle.Round(_initRectangleF));
        }


        public void RefreshState()
        {
            if (FunButton.LockedConditional != null)
            {
                Locked = FunButton.LockedConditional();
            }
            else if (FunButton.ButtonLockedStatePredicate != null)
            {
                Locked = FunButton.ButtonLockedStatePredicate.IsLocked(this);
            }
        }

        /// <summary>
        /// 发送配置好的数据
        /// </summary>
        /// <param name="sourceObject"></param>
        public void SendTheValue(ATPBaseClass sourceObject)
        {
            if (FunButton.SendValue != null && FunButton.SendValue.Count != 0)
            {
                foreach (var sendValue in FunButton.SendValue)
                {
                    if (!sendValue.Key())
                    {
                        continue;
                    }
                    foreach (var theValues in sendValue.Value)
                    {
                        sourceObject.append_postCmd(CmdType.SetBoolValue, Math.Abs(theValues), theValues > 0 ? 1 : 0, 0);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 8个按钮坐标
        /// </summary>
        public static RectangleF[] BtnRects =
        {
            new RectangleF(732, 75 * 0 + 1, 68, 70), new RectangleF(732, 75 * 1 + 1, 68, 70),
            new RectangleF(732, 75 * 2 + 1, 68, 70), new RectangleF(732, 75 * 3 + 1, 68, 70),
            new RectangleF(732, 75 * 4 + 1, 68, 70), new RectangleF(732, 75 * 5 + 1, 68, 70),
            new RectangleF(732, 75 * 6 + 1, 68, 70), new RectangleF(732, 75 * 7 + 1, 68, 70),
        };

        /// <summary>
        /// 按钮信息
        /// </summary>
        public FunBtnStruct FunButton { get; private set; }

        /// <summary>
        /// 按钮已锁定，无法操作
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// 事件处理
        /// </summary>
        public IButtonEventHandler ButtonEventHandler { set; get; }

        private readonly RectangleF _initRectangleF;
        private readonly Pen _normalPen = new Pen(SolidBrushsItems.WhiteBrush, 2);
        private readonly Font _normalFont = new Font("幼圆", 14f, FontStyle.Bold);
        private readonly SolidBrush _lockedNameBrush = new SolidBrush(Color.DimGray);
        private readonly SolidBrush _normalNameBrush = new SolidBrush(Color.White);
    }
}
