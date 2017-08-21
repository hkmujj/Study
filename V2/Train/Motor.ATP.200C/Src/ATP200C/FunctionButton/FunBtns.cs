using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using ATP200C.ButtonStateControl;
using ATP200C.MainView.FunctionButton;
using ATP200C.PublicComponents;

namespace ATP200C.FunctionButton
{
    /// <summary>
    /// 每一页菜单
    /// </summary>
    public abstract class FunBtnMenu
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="btnTypeToContents"></param>
        protected FunBtnMenu(int menuId, IList<FunBtnStruct> btnTypeToContents)
        {
            MenuId = menuId;
            if (btnTypeToContents.Count != 8) return;
            for (var i = 0; i < 8; i++)
            {
                m_BtnInfoArr[i] = new ButtonInfo(btnTypeToContents[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public abstract void Handle(FunctionButtonView context);

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="gs"></param>
        public void Paint(Graphics gs)
        {
            foreach (var info in m_BtnInfoArr)
            {
                info.RefreshState();

                info.DrawButtonInfo(gs);
            }
        }

        public void RefreshButtonState()
        {
            foreach (var info in m_BtnInfoArr)
            {
                info.RefreshState();
            }
        }

        /// <summary>
        /// 菜单中8个按钮状态信息
        /// </summary>
        protected readonly ButtonInfo[] m_BtnInfoArr = new ButtonInfo[8];
        public IEnumerable<ButtonInfo> BtnInfoArr { get { return m_BtnInfoArr; } }

        /// <summary>
        /// 当前菜单意义
        /// </summary>
        public string BtnMeans { get; set; }

        /// <summary>
        /// 数字键盘启用
        /// </summary>
        public bool NumbKeyboardOpen { get; set; }

        /// <summary>
        /// 当前菜单编号
        /// </summary>
        protected int MenuId;
    }

    /// <summary>
    /// 按钮元素结构
    /// </summary>
    public struct FunBtnStruct
    {
        /// <summary>
        /// 按钮类型名
        /// </summary>
        public BtnTypeName BtnTypeName;

        /// <summary>
        /// 按钮文字内容
        /// </summary>
        public string BtnContent;

        /// <summary>
        /// 按钮图片内容
        /// </summary>
        public Image BtnImage;

        /// <summary>
        /// 按钮锁定条件
        /// </summary>
        public Func<bool> LockedConditional;


        /// <summary>
        /// 按钮锁定条件
        /// </summary>
        public IButtonLockedStatePredicate ButtonLockedStatePredicate { set; get; }

        /// <summary>
        /// 跳转菜单号与跳转条件
        /// </summary>
        public Dictionary<int, Func<bool>> JumpConditional;

        /// <summary>
        /// 发送位
        /// </summary>
        public Dictionary<Func<bool>, List<int>> SendValue; 

        /// <summary>
        /// 事件处理类名
        /// </summary>
        public string ButtonEventHandlerName { set; get; }
    }
}
