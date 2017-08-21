using System;
using System.Drawing;
using System.IO;
using KumM_TMS.空调;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KumM_TMS.课程开始结束亮度
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TMS_Starting : baseClass
    {
        private DateTime StartTime;
        private bool IsJump;
        private Image Image;
        public override string GetInfo()
        {
            return "开始试图";
        }


        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == 2)
            {
                IsJump = false;
                StartTime = this.NowTime();
                Air_condition.Instance.Clear();
            }
            else
            {
                if ((this.NowTime().Ticks - StartTime.Ticks) / 10000000 > 5)
                {
                    IsJump = true;
                }
            }
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            Image = Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[0]));

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(Image, 0, 0, 800, 600);
            if (IsJump)
            {
                append_postCmd(CmdType.ChangePage, 42, 0, 0);
            }
        }
    }
}