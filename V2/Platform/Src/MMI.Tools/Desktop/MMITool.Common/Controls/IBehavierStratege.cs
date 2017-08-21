using System.Drawing;

namespace MMITool.Common.Controls
{
    /// <summary>
    /// 控件的行为策略
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBehavierStratege<out T> where  T: ICommonInnerControl
    {


        /// <summary>
        /// 所属控件 
        /// </summary>
        T Control { get; }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"></param>
        bool OnMouseDown(Point point);

        /// <summary>
        /// 鼠标按下后弹起
        /// </summary>
        /// <param name="point"></param>
        bool OnMouseUp(Point point);

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        void OnDraw(Graphics g);
    }
}
