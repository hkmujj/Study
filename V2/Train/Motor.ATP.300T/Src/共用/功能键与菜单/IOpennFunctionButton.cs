using System.Drawing;
using CommonUtil.Controls;

namespace Motor.ATP._300T.共用.功能键与菜单
{
    public interface IOpennFunctionButton
    {
        /// <summary>
        /// 按钮状态响应
        /// </summary>
        /// <param name="btnId"></param>
        /// <param name="mouseState"></param>
        void ButtonStateChange(BtnInfo btnId, MouseState mouseState);

        /// <summary>
        /// 数据更新
        /// </summary>
        /// <param name="source"></param>
        void Update(ATPBaseClass source);

        /// <summary>
        /// 实时绘制
        /// </summary>
        /// <param name="g"></param>
        void Paint(Graphics g);

        /// <summary>
        /// 数据清空
        /// </summary>
        void ClearData();

        /// <summary>
        /// 数据发送事件
        /// </summary>
        event AppendPostCmd OnAppendPostCmd;

        /// <summary>
        /// 是否需要绘制
        /// </summary>
        bool Painting { get; set; }


        ATP_Type ATPCurrentType { get; set; }
        //int MenuID { get; }
    }
}
