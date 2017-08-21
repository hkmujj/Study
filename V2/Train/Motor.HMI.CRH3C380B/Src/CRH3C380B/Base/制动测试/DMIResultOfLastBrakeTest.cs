using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.制动测试
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DMIResultOfLastBrakeTest : CRH3C380BBase
    {
        //2

        private List<RectangleF> m_RectsList;

        /// <summary>
        /// 隐藏制动试验时间
        /// </summary>
        private bool m_HideTime;

        /// <summary>
        /// 制动试验完成时间
        /// </summary>
        private string[] m_ResultOfBrakeTest;

        private readonly string[] m_BrakeTestNameArr =
        {
            "直接制动试验",
            "紧急制动试验",
            "总风管(MRP)贯通性试验",
            "列车管(BP)泄漏试验",
            "间接制动试验",
            "列车管(BP)贯通性试验"
        };

        /// <summary>
        /// 未完成的制动试验列表
        /// </summary>
        private List<string> m_UnfinishTestList;

        /// <summary>
        /// 存在未完成制动试验
        /// </summary>
        private bool m_ExistUnfinishTest;

        private readonly string[] m_BtnStr =
        {
            "",
            "",
            "制动\n有效率",
            "",
            "制动\n试验",
            "",
            "显示\n试验时间",
            "",
            "",
            "制动\n状态"
        };

        private readonly string[] m_BrakeTestRltIndexs =
        {
            OutBoolKeys.Oub测试结果5104,
            OutBoolKeys.Oub测试结果5105,
            OutBoolKeys.Oub测试结果5106,
            OutBoolKeys.Oub测试结果5107,
            OutBoolKeys.Oub测试结果5108,
            OutBoolKeys.Oub测试结果5109,

        };

        public override string GetInfo()
        {
            return "DMI上次试验结果";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2)
            {
                return;
            }

            m_HideTime = false; //初始隐藏时间
            m_ExistUnfinishTest = false;
            m_UnfinishTestList.Clear(); //清空未完成制动试验列表

            for (int i = 0; i < 6; i++)
            {

                if (OutBoolList[GetOutBoolIndex(m_BrakeTestRltIndexs[i])])
                {
                    m_ResultOfBrakeTest[i] = ControlBrakeTest.BrakeTestTime[i];

                }
                else
                {
                    m_ResultOfBrakeTest[i] = string.Empty;
                    m_UnfinishTestList.Add(m_BrakeTestNameArr[i]);
                    m_ExistUnfinishTest = true;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
        }


        public override void Paint(Graphics g)
        {
            GetValue();

            Draw(g);

        }

        private void Draw(Graphics g)
        {

            //标题
            g.DrawString("制动有效率; 上次制动试验结果", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            //底图
            g.DrawImage(m_HideTime ? BrakeImages.制动试验结果1 : BrakeImages.制动试验结果0, m_RectsList[2]);

            //日期时间
            foreach (var i in m_ResultOfBrakeTest.Where(i => !string.IsNullOrEmpty(i)))
            {
                g.DrawString(i, FontsItems.FontC10, SolidBrushsItems.BlackBrush, m_RectsList[4],
                    FontsItems.TheAlignment(FontRelated.靠左));
                break;
            }

            //状态栏1
            g.DrawString(m_ExistUnfinishTest ? "制动试验未完成！缺少下列制动试验：" : "制动试验完成",
                FontsItems.FontC11, SolidBrushsItems.BlackBrush, m_RectsList[6], FontsItems.TheAlignment(FontRelated.靠左));
            //状态栏2
            if (m_ExistUnfinishTest)
            {
                for (int i = 0; i < m_UnfinishTestList.Count; i++)
                {
                    g.DrawString(m_UnfinishTestList[i], FontsItems.FontC11, SolidBrushsItems.BlackBrush,
                        m_RectsList[8 + i], FontsItems.TheAlignment(FontRelated.靠左));
                }
            }
            //试验时间
            for (int i = 0; i < 6; i++)
            {
                g.DrawString(m_ResultOfBrakeTest[i], FontsItems.FontC10, SolidBrushsItems.BlackBrush,
                    m_RectsList[19 + i], FontsItems.TheAlignment(FontRelated.居中));
            }
        }

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 230, 0, 0);
                    break;
                case 8: //制动有效率
                    append_postCmd(CmdType.ChangePage, 230, 0, 0);
                    break;
                case 10: //制动试验
                    append_postCmd(CmdType.ChangePage, 250, 0, 0);
                    break;
                case 12: //显示试验时间
                    m_HideTime = !m_HideTime;
                    DMITitle.BtnContentList[6].BtnStr = m_HideTime ? "显示\n试验时间" : "隐藏\n试验时间";
                    break;
                case 15: //制动状态
                    append_postCmd(CmdType.ChangePage, 21, 0, 0);
                    break;
            }
        }

        private void InitData()
        {
            m_ResultOfBrakeTest = new string[6];
            m_UnfinishTestList = new List<string>(6);

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIResultOfLastBrakeTest, ref m_RectsList))
            {

            }
        }
    }
}
