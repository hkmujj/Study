using System.Drawing;
// ReSharper disable All
#pragma warning disable 1591

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 基础对象接口
    /// </summary>
    public interface IObjectBase : ITypeBase
    {

        /// <summary>
        /// 绘制图像
        /// </summary>
        /// <param name="g"></param>
        void paint(Graphics g);

        /// <summary>
        /// 鼠标单点下
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool mouseDown(Point point);

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool mouseUp(Point point);

        /// <summary>
        /// 设置动态数据
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        void setRunValue(int nParaA, int nParaB, float nParaC);

        /// <summary>
        /// 设置各列表指定位置的值
        /// </summary>
        /// <param name="listType"></param>
        /// <param name="nindex"></param>
        /// <param name="objValue"></param>
        void setListValue(ParaType listType, int nindex, object objValue);

        /// <summary>
        /// 配置参数
        /// </summary>
        IUIObject UIObj { set; get; }


    }

    /// <summary>
    /// 
    /// </summary>
    public enum ParaType { inBool, outBool, inFloat, outFloat, view, uiPara, uiIndex}
}
