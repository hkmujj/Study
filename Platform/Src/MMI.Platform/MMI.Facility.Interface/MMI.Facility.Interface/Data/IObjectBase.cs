using System.Drawing;
// ReSharper disable All
#pragma warning disable 1591

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// ��������ӿ�
    /// </summary>
    public interface IObjectBase : ITypeBase
    {

        /// <summary>
        /// ����ͼ��
        /// </summary>
        /// <param name="g"></param>
        void paint(Graphics g);

        /// <summary>
        /// ��굥����
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool mouseDown(Point point);

        /// <summary>
        /// ��굯��
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool mouseUp(Point point);

        /// <summary>
        /// ���ö�̬����
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        void setRunValue(int nParaA, int nParaB, float nParaC);

        /// <summary>
        /// ���ø��б�ָ��λ�õ�ֵ
        /// </summary>
        /// <param name="listType"></param>
        /// <param name="nindex"></param>
        /// <param name="objValue"></param>
        void setListValue(ParaType listType, int nindex, object objValue);

        /// <summary>
        /// ���ò���
        /// </summary>
        IUIObject UIObj { set; get; }


    }

    /// <summary>
    /// 
    /// </summary>
    public enum ParaType { inBool, outBool, inFloat, outFloat, view, uiPara, uiIndex}
}
