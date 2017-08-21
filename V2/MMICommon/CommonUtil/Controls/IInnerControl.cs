using System;
using System.Drawing;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 内部控件需要的实现的接口
    /// </summary>
    public interface IInnerControl
    {
        /// <summary>
        /// 
        /// </summary>
        Action<object> RefreshAction { set; get; }

        /// <summary>
        /// 刷新, 调用 RefreshAction
        /// </summary>
        void Refresh();

        /// <summary>
        /// 初始化
        /// </summary>
        void Init();

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
        /// 只绘图, 不刷新
        /// </summary>
        /// <param name="g"></param>
        void OnDraw(Graphics g);

        /// <summary>
        /// 刷新并绘图, 会调用 Refresh
        /// </summary>
        /// <param name="g"></param>
        void OnPaint(Graphics g);

        /// <summary>
        /// 是否包含一个点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool Contains(Point point);

        /// <summary>
        /// 获取或设置包含有关控件的数据的对象。
        /// </summary>
        object Tag { set; get; }
    }
}
