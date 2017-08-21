using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    /// <summary>
    /// �������� MainAeroB�� 
    ///     �ź��������� B����ʾ��Ϣ ����Ԥ����Ϣ 
    /// �����ˣ�Ԭ ��    
    /// ����ʱ�䣺2013-12-13
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class MMIBlackScreen : baseClass
    {
        /// <summary>
        /// ��ȡͼԪ��������
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "�� �� ״ ̬";
        }

        /// <summary>
        /// ͼԪ��ʼ������ִ�й��� (ֻ�ڳ�ʼ������ִ��һ��)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;

            append_postCmd(CmdType.SetInBoolValue, this.GetInboolIndex(InBoolKeys.InbATP��������־), 1, 0);

            return true;
        }

        /// <summary>
        /// ���Ʒ���(�÷����ᱻ��ʱ����һ����ʱ�����ظ�����) 
        /// </summary>
        /// <param name="dcGs">���� GDI+ ��ͼ����</param>
        public override void paint(Graphics dcGs)
        {
            if (BoolList[this.GetInboolIndex(InBoolKeys.InbATP��������־)])
            {
                append_postCmd(CmdType.ChangePage, 11, 0, 0);
            }
            else
            {
                append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf˾����), 0, 0);
                append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf���κ�ͷ), 0, 0);
                append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf���κ�β), 0, 0);
                append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf����), 0, 0);
            }
        }
    }
}