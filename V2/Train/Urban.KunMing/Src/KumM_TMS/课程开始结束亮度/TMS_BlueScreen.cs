using System;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KumM_TMS.课程开始结束亮度
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TMS_BlueScreen : baseClass
    {
        private Image m_Image;
        private bool m_IsJump;
        private DateTime m_StartTime;
        private bool IsSetChangViwTime;
        private DateTime SetChangViewTime;
        public override string GetInfo()
        {
            return "开始试图";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            m_Image = Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[0]));
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            if (m_IsJump)
            {
                dcGs.FillRectangle(Brushes.Black, 0, 0, 800, 600);
                if (!IsSetChangViwTime)
                {
                    SetChangViewTime = this.NowTime();
                    IsSetChangViwTime = true;
                }
                else
                {
                    if ((this.NowTime().Ticks - SetChangViewTime.Ticks) / 10000000 > 5 && BoolList[1099])
                    {

                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        IsSetChangViwTime = false;
                    }
                }
            }
            else
            {
                dcGs.DrawImage(m_Image, 0, 0, 800, 600);
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                m_IsJump = false;
                m_StartTime = this.NowTime();
            }
            else
            {
                if ((this.NowTime().Ticks - m_StartTime.Ticks) / 10000000 > 5)
                {
                    m_IsJump = true;
                }

            }
        }
    }
}