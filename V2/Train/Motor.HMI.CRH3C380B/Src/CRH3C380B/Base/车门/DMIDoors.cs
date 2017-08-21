using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using CommonUtil.Util;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Constant;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.车门
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIDoors : CRH3C380BBase
    {
        /// <summary>
        /// 是否显示禁用门
        /// </summary>
        private bool m_IsDisplayDoorUse;

        private List<CommonInnerControlBase> m_ComtrolCollection;

        private Dictionary<int, RectangleF> m_TrainIDictionary;
        private bool m_MarshallMode;
        private bool m_TrainInSide;
        private bool m_DoorForbidden;

        private int[] m_AllDoorsStateInBoolList;
        private int[] m_SidesDoorStateInBoolList;

        private Dictionary<int, int> m_AllDoorsUnknowStatus;
        private List<RectangleF> m_RectsList;
        private DateTime m_LastTime;

        private SenvalueType m_CurrenType;

        private readonly string[] m_BtnStr =
        {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private List<DoorUnit> m_TrainDoorsStateList;

        private int[] m_TheAllDoorOpenOrClose;
        private List<int[]> m_AllDoorOpenOrClose;

        private int[] m_TheSidesDoorState;
        private List<int[]> m_SidesDoorState;

        //2
        public override string GetInfo()
        {
            return "DMI车门";
        }

        //6
        public override bool Initalize()
        {
            InitData();

            m_TrainDoorsStateList = new List<DoorUnit>();

            var file = Path.Combine(RecPath, "..\\Config\\车门车站信息.txt");

            try
            {
                var allLines = File.ReadAllLines(file, Encoding.Default);

                ParseLines(allLines);
            }
            catch (Exception e)
            {
                AppLog.Fatal(string.Format("Can not read file : {0} \r\n {1}", file, e));
            }

            return true;
        }

        private void ParseLines(IEnumerable<string> allLines)
        {
            // 第1行是标题
            foreach (var line in allLines.Skip(1))
            {
                string[] tmpArr = line.Split('\t');
                if (tmpArr.Length == 4)
                {
                    var indexs = StringToIntArray(tmpArr[3]);
                    var tmp = new DoorUnit(Convert.ToInt32(tmpArr[0]),
                        tmpArr[1],
                        new[]
                        {
                            m_RectsList[StringToIntArray(tmpArr[2])[0] + 13],
                            m_RectsList[StringToIntArray(tmpArr[2])[1] + 13],
                            m_RectsList[StringToIntArray(tmpArr[2])[2] + 13],
                            m_RectsList[StringToIntArray(tmpArr[2])[3] + 13]
                        },
                        indexs);
                    m_TrainDoorsStateList.Add(tmp);
                    UIObj.InBoolList.AddRange(indexs);
                }
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {

            if (DMITitle.BtnContentList.Count != 0)
            {
                m_MarshallMode = DMITitle.MarshallMode;
                m_TrainInSide = DMITitle.TrainInSide;
                m_TheAllDoorOpenOrClose = null;
                m_TheSidesDoorState = null;
                if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
                {
                    if (m_TrainInSide)
                    {
                        m_TheAllDoorOpenOrClose = m_AllDoorOpenOrClose[3];
                        m_TheSidesDoorState = m_SidesDoorState[3];
                    }
                    else
                    {
                        m_TheAllDoorOpenOrClose = m_AllDoorOpenOrClose[2];
                        m_TheSidesDoorState = m_SidesDoorState[2];
                    }
                }
                else
                {
                    if (m_MarshallMode && m_TrainInSide)
                    {
                        m_TheAllDoorOpenOrClose = m_AllDoorOpenOrClose[3];
                        m_TheSidesDoorState = m_SidesDoorState[3];
                    }
                    else if (m_MarshallMode && !m_TrainInSide)
                    {
                        m_TheAllDoorOpenOrClose = m_AllDoorOpenOrClose[2];
                        m_TheSidesDoorState = m_SidesDoorState[2];
                    }
                    else if (!m_MarshallMode && m_TrainInSide)
                    {
                        m_TheAllDoorOpenOrClose = m_AllDoorOpenOrClose[1];
                        m_TheSidesDoorState = m_SidesDoorState[1];
                    }
                    else
                    {
                        m_TheAllDoorOpenOrClose = m_AllDoorOpenOrClose[0];
                        m_TheSidesDoorState = m_SidesDoorState[0];
                    }
                }

                if (nParaA == 2)
                {
                    DMITitle.BtnContentList[9].BtnStr = m_BtnStr[9];
                    SetButtonStr(SenvalueType.Null);
                    SetButtonStr(m_CurrenType);
                }

            }
        }

        public override void Paint(Graphics g)
        {
            SendTheValue(SenvalueType.Null);

            if (GlobalParam.Instance.ProjectType != ProjectType.CRH380BL)
            {
                m_ComtrolCollection.ForEach(e => e.OnPaint(g));
            }

            GetValue();

            Draw(g);
        }

        private void Draw(Graphics g)
        {
            PaintState(g);

            PaintRectangles(g);
        }

        private int[] StringToIntArray(string str)
        {
            return str.Split(',', ' ').Select(s => Convert.ToInt32(s)).ToArray();
        }


        private void SendTheValue(SenvalueType type)
        {
            append_postCmd(CmdType.SetBoolValue, this.GetActureOutbIndex((int) SenvalueType.释放司机室门), 0, 0);
            append_postCmd(CmdType.SetBoolValue, this.GetActureOutbIndex((int) SenvalueType.禁用司机室门), 0, 0);
            append_postCmd(CmdType.SetBoolValue, this.GetActureOutbIndex((int) SenvalueType.释放门), 0, 0);
            append_postCmd(CmdType.SetBoolValue, this.GetActureOutbIndex((int) SenvalueType.禁用门), 0, 0);
            if (type == SenvalueType.Null)
            {
                return;
            }

            m_CurrenType = type;
            append_postCmd(CmdType.SetBoolValue, this.GetActureOutbIndex((int)type), 1, 0);
        }

        private void SetButtonStr(SenvalueType type)
        {
            switch (type)
            {
                case SenvalueType.释放司机室门:
                    DMITitle.BtnContentList[0].BtnStr = string.Empty;
                    DMITitle.BtnContentList[1].BtnStr = "禁用\n司机室门";
                    break;
                case SenvalueType.禁用司机室门:
                    DMITitle.BtnContentList[0].BtnStr = "释放\n司机室门";
                    DMITitle.BtnContentList[1].BtnStr = string.Empty;
                    break;
                case SenvalueType.释放门:
                    DMITitle.BtnContentList[3].BtnStr = string.Empty;
                    DMITitle.BtnContentList[4].BtnStr = "禁用门";
                    break;
                case SenvalueType.禁用门:
                    DMITitle.BtnContentList[3].BtnStr = "释放门";
                    DMITitle.BtnContentList[4].BtnStr = string.Empty;
                    break;
                case SenvalueType.Null:
                    DMITitle.BtnContentList[0].BtnStr = "释放\n司机室门";
                    DMITitle.BtnContentList[1].BtnStr = string.Empty;
                    DMITitle.BtnContentList[3].BtnStr = string.Empty;
                    DMITitle.BtnContentList[4].BtnStr = "禁用门";
                    break;
            }
        }

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0: //C键
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                    case 6: //释放司机室门
                        if (DMITitle.BtnContentList[0].BtnStr != string.Empty)
                        {
                            SendTheValue(SenvalueType.释放司机室门);
                            SetButtonStr(SenvalueType.释放司机室门);
                        }
                        break;
                    case 7: //禁用司机室门
                        if (DMITitle.BtnContentList[1].BtnStr != string.Empty)
                        {
                            SendTheValue(SenvalueType.禁用司机室门);
                            SetButtonStr(SenvalueType.禁用司机室门);
                        }
                        break;
                    case 9: //释放门
                        if (DMITitle.BtnContentList[3].BtnStr != string.Empty)
                        {
                            SendTheValue(SenvalueType.释放门);
                            SetButtonStr(SenvalueType.释放门);
                        }
                        break;
                    case 10: //禁用门
                        if (DMITitle.BtnContentList[4].BtnStr != string.Empty)
                        {
                            SendTheValue(SenvalueType.禁用门);
                            SetButtonStr(SenvalueType.禁用门);
                        }
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                }
            }

            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                var temp = new int[32];
                for (int i = 0; i < 32; i++)
                {
                    temp[i] = m_SidesDoorStateInBoolList[i];
                }

                m_DoorForbidden = !temp.Any(s => !BoolList[s]);
            }
            else
            {
                var temp = new int[(DMITitle.MarshallMode ? 32 : 16)];
                for (int i = 0; i < (DMITitle.MarshallMode ? 32 : 16); i++)
                {
                    temp[i] = m_SidesDoorStateInBoolList[i];
                }

                m_DoorForbidden = !temp.Any(s => !BoolList[s]);
            }


        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("门", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
            g.DrawImage(DMITitle.NightMode ? DoorImages.instruction_1 : DoorImages.instruction_0, m_RectsList[12]);
            //////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void PaintState(Graphics g)
        {
            m_TrainIDictionary.Clear();
            if (m_TheAllDoorOpenOrClose.Length == 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    RectangleF a = new RectangleF(m_RectsList[m_TheAllDoorOpenOrClose[i]].X,
                        m_RectsList[m_TheAllDoorOpenOrClose[i]].Y - 40, m_RectsList[m_TheAllDoorOpenOrClose[i]].Width,
                        m_RectsList[m_TheAllDoorOpenOrClose[i]].Height);
                    m_TrainIDictionary.Add(i, a);
                }

            }
            else
            {

                for (int i = 0; i < 16; i++)
                {
                    RectangleF a = new RectangleF(m_RectsList[m_TheAllDoorOpenOrClose[i]].X,
                        m_RectsList[m_TheAllDoorOpenOrClose[i]].Y - 40, m_RectsList[m_TheAllDoorOpenOrClose[i]].Width,
                        m_RectsList[m_TheAllDoorOpenOrClose[i]].Height);
                    m_TrainIDictionary.Add(i, a);
                }
            }

            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                //所有门
                for (var index = 0; index < 16; index++)
                {
                    g.DrawString(
                        (index + 1).ToString("00"), FontsItems.FontC11,
                        SolidBrushsItems.WhiteBrush,
                        m_TrainIDictionary[index],
                        FontsItems.TheAlignment(FontRelated.居中));

                    if (index == 8)
                    {
                        continue;
                    }
                    //if (DMITitle.MarshallMode && index == 7)
                    //{
                    //    continue;
                    //}

                    if (BoolList[m_AllDoorsUnknowStatus[index + 1]])
                    {
                        g.DrawImage(CommonImages.StateUnkown,
                            m_RectsList[m_TheAllDoorOpenOrClose[index]]);
                    }
                    else if (BoolList[m_AllDoorsStateInBoolList[index]])
                    {
                        g.FillRectangle(SolidBrushsItems.WhiteBrush, m_RectsList[m_TheAllDoorOpenOrClose[index]]);
                        g.DrawRectangle(PenItems.WhiltPen, Rectangle.Round(m_RectsList[m_TheAllDoorOpenOrClose[index]]));
                    }
                    else
                    {
                        g.DrawRectangle(PenItems.WhiltPen, Rectangle.Round(m_RectsList[m_TheAllDoorOpenOrClose[index]]));
                    }
                }
                //车门
                foreach (var tmp in m_TrainDoorsStateList)
                {
                    tmp.Marshalling16 = true;
                    if (!tmp.Marshalling16)
                    {
                        tmp.Drawing = tmp.TrainID <= 8;
                    }
                    else
                    {
                        tmp.Drawing = true;
                    }
                    tmp.Inside = m_TrainInSide;

                    tmp.Update(BoolList[tmp.LogicFault], BoolList[tmp.LogicIdOfOpen], BoolList[tmp.LogicIdOfLocked],
                        BoolList[tmp.LogicIdOfUnkown]);
                    tmp.DrawDoorState(g);
                }


                //两侧门
                for (var index = 0; index < 32; index++)
                {
                    if (BoolList[m_SidesDoorStateInBoolList[index]])
                    {
                        g.DrawImage(DoorImages.doors1, m_RectsList[m_TheSidesDoorState[index]]);
                    }
                }

                if (BoolList[UIObj.InBoolList[6]])
                {
                    if (CompareTime.Compare(CurrentTime, m_LastTime, 1))
                    {
                        m_LastTime = CurrentTime;
                        g.DrawImage(DoorImages.doors, m_RectsList[125]);
                    }
                    else
                    {
                        g.DrawImage(DoorImages.doors0, m_RectsList[125]);
                    }
                }
                else
                {
                    g.DrawImage(BoolList[UIObj.InBoolList[7]] ? DoorImages.doors : DoorImages.doors0, m_RectsList[125]);
                }
            }
            else
            {
                //所有门
                for (var index = 0; index < (m_MarshallMode ? 16 : 8); index++)
                {
                    g.DrawString(
                        GlobalParam.Instance.ProjectType == ProjectType.CRH3C
                            ? (index + 1 + (m_MarshallMode ? index > 7 ? 2 : 0 : 0)).ToString("0")
                            : (index + 11 + (m_MarshallMode ? index > 7 ? 2 : 0 : 0)).ToString("00"), FontsItems.FontC11,
                        SolidBrushsItems.WhiteBrush,
                        m_TrainIDictionary[index],
                        FontsItems.TheAlignment(FontRelated.居中));

                    if (index == 3)
                    {
                        continue;
                    }
                    if (DMITitle.MarshallMode && index == 11)
                    {
                        continue;
                    }

                    if (BoolList[m_AllDoorsUnknowStatus[index + 1]])
                    {
                        g.DrawImage(CommonImages.StateUnkown,
                            m_RectsList[m_TheAllDoorOpenOrClose[index]]);
                    }
                    else if (BoolList[m_AllDoorsStateInBoolList[index]])
                    {
                        g.FillRectangle(SolidBrushsItems.WhiteBrush, m_RectsList[m_TheAllDoorOpenOrClose[index]]);
                        g.DrawRectangle(PenItems.WhiltPen, Rectangle.Round(m_RectsList[m_TheAllDoorOpenOrClose[index]]));
                    }
                    else
                    {
                        g.DrawRectangle(PenItems.WhiltPen, Rectangle.Round(m_RectsList[m_TheAllDoorOpenOrClose[index]]));
                    }
                }
                //车门
                foreach (var tmp in m_TrainDoorsStateList)
                {
                    tmp.Marshalling16 = m_MarshallMode;
                    if (!tmp.Marshalling16)
                    {
                        tmp.Drawing = tmp.TrainID <= 8;
                    }
                    else
                    {
                        tmp.Drawing = true;
                    }
                    tmp.Inside = m_TrainInSide;

                    tmp.Update(BoolList[tmp.LogicFault], BoolList[tmp.LogicIdOfOpen], BoolList[tmp.LogicIdOfLocked],
                        BoolList[tmp.LogicIdOfUnkown]);
                    tmp.DrawDoorState(g);
                }


                //两侧门
                for (var index = 0; index < (m_MarshallMode ? 32 : 16); index++)
                {
                    if (BoolList[m_SidesDoorStateInBoolList[index]])
                    {
                        g.DrawImage(DoorImages.doors1, m_RectsList[m_TheSidesDoorState[index]]);
                    }
                }

                if (BoolList[UIObj.InBoolList[6]])
                {
                    if (CompareTime.Compare(CurrentTime, m_LastTime, 1))
                    {
                        m_LastTime = CurrentTime;
                        g.DrawImage(DoorImages.doors, m_RectsList[125]);
                    }
                    else
                    {
                        g.DrawImage(DoorImages.doors0, m_RectsList[125]);
                    }
                }
                else
                {
                    g.DrawImage(BoolList[UIObj.InBoolList[7]] ? DoorImages.doors : DoorImages.doors0, m_RectsList[125]);
                }
            }

            if (!m_DoorForbidden || !m_IsDisplayDoorUse)
            {
                return;
            }

            g.FillRectangle(SolidBrushsItems.YellowBrush, m_RectsList[126]);
            g.DrawString("门已禁用",
                FontsItems.FontC11,
                SolidBrushsItems.BlackBrush,
                m_RectsList[126],
                FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void InitData()
        {
            m_ComtrolCollection = new List<CommonInnerControlBase>
            {
                new Line(new Point(178, 260), new Point(178, 362))
                {
                    Color = DMITitle.NightMode ? Color.Black : Color.White,
                    Tag = 1,
                    Visible = true
                },
                new Line(new Point(474, 260), new Point(474, 362))
                {
                    Color = DMITitle.NightMode ? Color.Black : Color.White,
                    Tag = 2,
                    Visible = false
                },
                new GDIRectText
                {
                    Text = "动车组1",
                    TextColor = DMITitle.NightMode ? Color.Black : Color.White,
                    OutLineRectangle = new Rectangle(180, 260, 130, 30),
                    NeedDarwOutline = false,
                    RefreshAction = o =>
                    {
                        ((GDIRectText) o).Text = DMITitle.MarshallMode && DMITitle.TrainInSide ? "动车组2" : "动车组1";
                    },
                    Tag = 1
                },
                new GDIRectText
                {
                    Text = "动车组2",
                    TextColor = DMITitle.NightMode ? Color.Black : Color.White,
                    OutLineRectangle = new Rectangle(476, 260, 130, 30),
                    NeedDarwOutline = false,
                    RefreshAction = o =>
                    {
                        ((GDIRectText) o).Text = DMITitle.MarshallMode && DMITitle.TrainInSide ? "动车组1" : "动车组2";
                    },
                    Tag = 2,
                    Visible = false
                }
            };

            DMITitle.MarshallModeChanged += m =>
            {
                m_ComtrolCollection[m_ComtrolCollection.FindIndex(f => f is Line && (int) f.Tag == 2)].Visible =
                    DMITitle.MarshallMode;
                m_ComtrolCollection[m_ComtrolCollection.FindIndex(f => f is GDIRectText && (int) f.Tag == 2)].Visible =
                    DMITitle.MarshallMode;
            };
            m_TrainIDictionary = new Dictionary<int, RectangleF>();
            m_AllDoorsStateInBoolList = new int[UIObj.InBoolList[3] - UIObj.InBoolList[2] + 1];
            int[] tmp = new int[16];
            for (int index = 0; index < m_AllDoorsStateInBoolList.Length; index++)
            {
                m_AllDoorsStateInBoolList[index] = UIObj.InBoolList[2] + index;
                tmp[index] = index + 1;
            }

            m_AllDoorsUnknowStatus = tmp.ToDictionary(kvp => kvp,
                kvp => GetInBoolIndex(string.Format("车厢号{0}全部门状态未知", kvp)));
            m_SidesDoorStateInBoolList = new int[UIObj.InBoolList[5] - UIObj.InBoolList[4] + 1];
            for (int index = 0; index < m_SidesDoorStateInBoolList.Length; index++)
            {
                m_SidesDoorStateInBoolList[index] = UIObj.InBoolList[4] + index;
            }


            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIDoors, ref m_RectsList))
            {
            }
            m_AllDoorOpenOrClose = new List<int[]>
            {
                new[] {77, 78, 79, 80, 81, 82, 83, 84},
                new[] {84, 83, 82, 81, 80, 79, 78, 77},
                new[]
                {
                    77,
                    78,
                    79,
                    80,
                    81,
                    82,
                    83,
                    84,
                    85,
                    86,
                    87,
                    88,
                    89,
                    90,
                    91,
                    92
                },
                new[]
                {
                    92,
                    91,
                    90,
                    89,
                    88,
                    87,
                    86,
                    85,
                    84,
                    83,
                    82,
                    81,
                    80,
                    79,
                    78,
                    77
                }
            };

            m_SidesDoorState = new List<int[]>
            {
                new[] {93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108},
                new[] {107, 108, 105, 106, 103, 104, 101, 102, 99, 100, 97, 98, 95, 96, 93, 94},
                new[]
                {
                    93,
                    94,
                    95,
                    96,
                    97,
                    98,
                    99,
                    100,
                    101,
                    102,
                    103,
                    104,
                    105,
                    106,
                    107,
                    108,
                    109,
                    110,
                    111,
                    112,
                    113,
                    114,
                    115,
                    116,
                    117,
                    118,
                    119,
                    120,
                    121,
                    122,
                    123,
                    124
                },
                new[]
                {
                    123,
                    124,
                    121,
                    122,
                    119,
                    120,
                    117,
                    118,
                    115,
                    116,
                    113,
                    114,
                    111,
                    112,
                    109,
                    110,
                    107,
                    108,
                    105,
                    106,
                    103,
                    104,
                    101,
                    102,
                    99,
                    100,
                    97,
                    98,
                    95,
                    96,
                    93,
                    94
                }
            };
            m_IsDisplayDoorUse =
                GlobalParam.Instance.TrainDiffConfig.AllTrainConfig.Find(f => f.Type == GlobalParam.Instance.ProjectType)
                    .DoorUseDisplay;
        }
    }
}