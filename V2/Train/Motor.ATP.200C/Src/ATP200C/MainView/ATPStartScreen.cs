using System;
using System.Drawing;
using ATP200C.PublicComponents;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace ATP200C.MainView
{
    /// <summary>
    /// �������� GT_MainAeroB�� 
    ///     ����׼������
    /// �����ˣ�Ԭ ��    
    /// ����ʱ�䣺2013-12-13
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ATPStartScreen : ATPBaseClass
    {
        #region  ���ط���
        /// <summary>
        /// ��ȡͼԪ��������
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "ATP��������";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2) return;
            _switchInTime = CurrentTime;
        }

        /// <summary>
        /// ���Ʒ���(�÷����ᱻ��ʱ����һ����ʱ�����ظ�����) 
        /// </summary>
        /// <param name="dcGs">���� GDI+ ��ͼ����</param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString(_info, FontsItems.Font28_Def_B,
                SolidBrushsItems.WhiteBrush, _infoRect, FontsItems.TheAlignment(FontRelated.����));
            dcGs.DrawRectangle(PenItems.WhitePen1, _infoRect);

            if (CurrentTime - _switchInTime > _timeSpan)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        #endregion

        private readonly string _info = "DMI CTCS\nV01-18";
        private readonly Rectangle _infoRect = new Rectangle(0, 0, 800, 600);
        private readonly TimeSpan _timeSpan = new TimeSpan(0, 0, 0, 3);
        private DateTime _switchInTime;
    }
}