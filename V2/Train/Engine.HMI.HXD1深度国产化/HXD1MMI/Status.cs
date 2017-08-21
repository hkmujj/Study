using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 底布状态信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class Status : baseClass
    {
        private readonly List<RectText> m_DownRectTextList = new List<RectText>();
        private readonly string[] m_MainScreenStringList = { "", "主要数据", "维护", "", "", "参数配置", "牵引数据", "隔离解锁", "", "", "" };
        private readonly string[] m_MainDataStringList = { "", "主要数据", "温度", "控制系统", "辅助系统", "", "", "封锁条件", "库内动车", "辅机测试", "主界面" };
        private readonly string[] m_TractionDataStringList = { "", "", "", "", "", "", "", "", "", "", "主界面" };
        private readonly string[] m_TrainStateStringList = { "", "机车\n1001B", "机车\n1001A", "", "", "牵引状态", "主断状态", "受电弓", "", "", "主界面" };
        private readonly string[] m_TrainConfiger = { "", "机车配置", "列车参数", "轮缘润滑", "分相/\n警惕", "车顶开关", "", "", "", "", "主界面" };
        private readonly string[] m_TrainParameter = { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        private readonly string[] m_TrainTopSwitch = { "", "", "", "", "", "RDC1\nON", "", "", "", "", "返回" };
        private readonly string[] m_AllFault = { "", "机车\n1001B", "机车\n1001A", "", "", "", "", "当前故障", "所有故障", "历史故障", "主界面" };
        private readonly string[] m_HisteryFault = { "", "", "", "", "", "", "", "当前故障", "所有故障", "历史故障", "主界面" };
        public string[] FaultAssist = { "", "V=0", "V>0", "信息", "", "", "", "", "", "", "主界面" };

        private Image m_Image;

        public override string GetInfo()
        {
            return "底部状态信息";
        }

        private void SetMainView()
        {
            for (int i = 0; i < m_DownRectTextList.Count; i++)
            {
                m_DownRectTextList[i].Text = m_MainScreenStringList[i];
                m_DownRectTextList[i].BackgroundColor = Common.BlackColor;
            }
        }

        private void SetMainDataView()
        {
            for (int i = 0; i < m_DownRectTextList.Count; i++)
            {
                m_DownRectTextList[i].Text = m_MainDataStringList[i];
                m_DownRectTextList[i].BackgroundColor = Common.BlackColor;
            }
        }

        private void SetTrainState()
        {
            for (int i = 0; i < m_DownRectTextList.Count; i++)
            {
                m_DownRectTextList[i].Text = m_TrainStateStringList[i];
                m_DownRectTextList[i].BackgroundColor = Common.BlackColor;
            }
        }

        private void SetTrainConfg()
        {
            for (int i = 0; i < m_DownRectTextList.Count; i++)
            {
                m_DownRectTextList[i].Text = m_TrainConfiger[i];
                m_DownRectTextList[i].BackgroundColor = Common.BlackColor;
            }
        }


        private void SetDraughtView()
        {
            for (int i = 0; i < m_DownRectTextList.Count; i++)
            {
                m_DownRectTextList[i].Text = m_TractionDataStringList[i];
                m_DownRectTextList[i].BackgroundColor = Common.BlackColor;
            }
        }

        private void SetTrainParameter()
        {

            for (int i = 0; i < m_DownRectTextList.Count; i++)
            {
                m_DownRectTextList[i].Text = m_TrainParameter[i];
                m_DownRectTextList[i].BackgroundColor = Common.BlackColor;
            }
        }


        private void SetTrainTopSwitch()
        {
            for (int i = 0; i < m_DownRectTextList.Count; i++)
            {
                m_DownRectTextList[i].Text = m_TrainTopSwitch[i];
                m_DownRectTextList[i].BackgroundColor = Common.BlackColor;
            }
        }

        private void SetAllFault()
        {
            for (int i = 0; i < m_DownRectTextList.Count; i++)
            {
                m_DownRectTextList[i].Text = m_AllFault[i];
                m_DownRectTextList[i].BackgroundColor = Common.BlackColor;
            }
        }

        private void SetHisteryFault()
        {
            for (int i = 0; i < m_DownRectTextList.Count; i++)
            {
                m_DownRectTextList[i].Text = m_HisteryFault[i];
                m_DownRectTextList[i].BackgroundColor = Common.BlackColor;
            }
        }

        private void SetFaultAssist()
        {

            for (int i = 0; i < m_DownRectTextList.Count; i++)
            {
                m_DownRectTextList[i].Text = FaultAssist[i];
                m_DownRectTextList[i].BackgroundColor = Common.BlackColor;
            }
        }

        private int currentViewID;
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                currentViewID = nParaB;
            }
            switch (nParaB)
            {
                case 1:   //主界面
                    {
                        Title.CurrentView = "主界面";
                        SetMainView();
                        if (BoolList[1207])
                        {
                            m_DownRectTextList[8].Text = "联挂";
                            m_DownRectTextList[8].BackgroundColor = Common.BlackColor;
                            m_DownRectTextList[8].TextColor = Color.White;
                        }

                        if (BoolList[1208])
                        {
                            m_DownRectTextList[8].Text = "联挂";
                            m_DownRectTextList[8].BackgroundColor = Color.Yellow;
                            m_DownRectTextList[8].TextColor = Color.Black;
                        }
                        if (BoolList[1217])
                        {
                            m_DownRectTextList[9].Text = "定速\n控制";
                            m_DownRectTextList[9].TextColor = Color.White;
                            m_DownRectTextList[9].BackgroundColor = Common.BlackColor;
                        }

                        if (BoolList[1218])
                        {
                            m_DownRectTextList[9].Text = "定速\n控制";
                            m_DownRectTextList[9].TextColor = Color.Black;
                            m_DownRectTextList[9].BackgroundColor = Color.Yellow;
                        }
                    }
                    break;
                case 2:
                    {
                        Title.CurrentView = "主要数据";
                        SetMainDataView();
                        m_DownRectTextList[1].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 16:
                    {
                        Title.CurrentView = "机车配置";
                        SetTrainConfg();
                        m_DownRectTextList[1].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 17:
                    {
                        Title.CurrentView = "维护";
                    }
                    break;
                case 18:
                    {
                        Title.CurrentView = "参数配置";

                    }
                    break;
                case 5:
                    {
                        Title.CurrentView = "牵引数据";
                        SetDraughtView();
                    }
                    break;
                case 19:
                    {

                        Title.CurrentView = "隔离解锁";
                    }
                    break;
                case 4:
                    {
                        Title.CurrentView = "温度界面";
                        SetMainDataView();
                        m_DownRectTextList[2].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 6:
                    {
                        Title.CurrentView = "控制系统";
                        SetMainDataView();
                        m_DownRectTextList[3].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 7:
                    {
                        Title.CurrentView = "辅助系统";
                        SetMainDataView();
                        m_DownRectTextList[4].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 8:
                    {
                        Title.CurrentView = "牵引封锁";
                        SetTrainState();
                        m_DownRectTextList[1].BackgroundColor = Common.YellowColor;
                        m_DownRectTextList[5].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 31:
                    {
                        Title.CurrentView = "牵引封锁";
                        SetTrainState();
                        m_DownRectTextList[2].BackgroundColor = Common.YellowColor;
                        m_DownRectTextList[5].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 9:
                    {
                        Title.CurrentView = "主断状态";
                        SetTrainState();
                        m_DownRectTextList[1].BackgroundColor = Common.YellowColor;
                        m_DownRectTextList[6].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 32:
                    {
                        Title.CurrentView = "主断状态";
                        SetTrainState();
                        m_DownRectTextList[2].BackgroundColor = Common.YellowColor;
                        m_DownRectTextList[6].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 10:
                    {
                        Title.CurrentView = "受电弓状态";
                        SetTrainState();
                        m_DownRectTextList[1].BackgroundColor = Common.YellowColor;
                        m_DownRectTextList[7].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 33:
                    {
                        Title.CurrentView = "受电弓状态";
                        SetTrainState();
                        m_DownRectTextList[2].BackgroundColor = Common.YellowColor;
                        m_DownRectTextList[7].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 11:
                    {
                        Title.CurrentView = "辅机测试条件";
                        SetMainDataView();
                        m_DownRectTextList[9].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 12:
                    {
                        Title.CurrentView = "库内动车条件";
                        SetMainDataView();
                        m_DownRectTextList[8].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 21:
                    {
                        Title.CurrentView = "列车参数";
                        SetTrainParameter();
                    }
                    break;
                case 22:
                    {
                        Title.CurrentView = "轮缘润滑";
                        SetTrainParameter();
                    }
                    break;
                case 23:
                    {
                        Title.CurrentView = "分相/警惕";
                        SetTrainParameter();
                    }
                    break;
                case 24:
                    {
                        Title.CurrentView = "车顶隔开开关状态设置";
                        SetTrainTopSwitch();
                    }
                    break;
                case 13:
                    {
                        Title.CurrentView = "当前故障";
                        SetAllFault();
                        m_DownRectTextList[1].BackgroundColor = Common.YellowColor;
                    }
                    break;

                case 14:
                    {
                        Title.CurrentView = "所有故障";
                        SetAllFault();
                        m_DownRectTextList[1].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 34:
                    {
                        Title.CurrentView = "当前故障";
                        SetAllFault();
                        m_DownRectTextList[2].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 35:
                    {
                        Title.CurrentView = "所有故障";
                        SetAllFault();
                        m_DownRectTextList[2].BackgroundColor = Common.YellowColor;
                    }
                    break;
                case 15:
                    {
                        Title.CurrentView = "历史故障";
                        SetHisteryFault();
                    }
                    break;
                case 25:
                    {
                        Title.CurrentView = "处理提示";
                        SetFaultAssist();
                    }
                    break;
            }
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_Image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[0]);

            m_DownRectTextList.Add(new RectText(new Rectangle(3, Common.InitialPosY + 547, 81, 50), "", 12, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true, m_Image));

            for (int i = 0; i < 10; i++)
            {
                m_DownRectTextList.Add(new RectText(new Rectangle(90 + 71 * i, Common.InitialPosY + 547, 63, 50), "", 14, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true));
            }

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            if (currentViewID == 24)
            {
                m_DownRectTextList[5].Text = BoolList[1033] ? "RDC1\nOFF" : BoolList[1034] ? "RDC1\nON" : "";
            }

            if (currentViewID != 1)
            {
                m_DownRectTextList[8].TextColor = Color.White;
                m_DownRectTextList[9].TextColor = Color.White;
                append_postCmd(CmdType.SetBoolValue, 2408, 0, 0);
            }
            else
            {
                append_postCmd(CmdType.SetBoolValue, 2408, 1, 0);
            }

            foreach (var v in m_DownRectTextList)
            {
                v.StatusOnDraw(dcGs);
            }
        }
    }
}
