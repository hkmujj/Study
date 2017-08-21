using System.Drawing;
using System.Runtime.CompilerServices;

namespace GT_MMI.Interface.State
{
    interface IState
    {
        IState F1Down();
        IState F1Up();

        IState F2Down();
        IState F2Up();

        IState F3Down();
        IState F3Up();

        IState F4Down();
        IState F4Up();

        IState F5Down();
        IState F5Up();

        IState F6Down();
        IState F6Up();

        IState F7Down();
        IState F7Up();

        IState F8Down();
        IState F8Up();

        IState F9Down();
        IState F9Up();

        IState Num1Down();
        IState Num1Up();

        IState Num2Down();
        IState Num2Up();

        IState Num3Down();
        IState Num3Up();

        IState Num4Down();
        IState Num4Up();

        IState Num5Down();
        IState Num5Up();

        IState Num6Down();
        IState Num6Up();

        IState Num7Down();
        IState Num7Up();

        IState Num8Down();
        IState Num8Up();

        IState Num9Down();
        IState Num9Up();

        IState Num0Down();
        IState Num0Up();

        /// <summary>
        /// 警惕按钮按下
        /// </summary>
        /// <returns></returns>
        IState VigilantDown();

        /// <summary>
        /// 警惕按钮弹起
        /// </summary>
        /// <returns></returns>
        IState VigilantUp();

        /// <summary>
        /// 状态更新
        /// </summary>
        /// <returns></returns>
        IState Update();

        void OnPaint(Graphics g);
    }
}
