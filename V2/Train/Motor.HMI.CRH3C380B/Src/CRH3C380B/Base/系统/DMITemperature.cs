using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using CommonUtil.Controls;
using Excel.Interface;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Base.系统.Temperature;
using Motor.HMI.CRH3C380B.Common;
using ProjectType = Motor.HMI.CRH3C380B.Common.ProjectType;

namespace Motor.HMI.CRH3C380B.Base.系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMITemperature : CRH3C380BBase
    {
        public override bool Initalize()
        {

            InitData();
            return true;
        }

        private List<RectangleF> m_Rectangle;

        private List<TemperatureTrainView> m_TemperatureTrainViews;

        private readonly string[] m_Menu1 =
        {
            "温度℃",
            "小齿轮牵引电机侧",
            "大齿轮牵引电机侧",
            "小齿轮车轮侧",
            "大齿轮车轮侧",
            "小齿轮牵引电机侧",
            "大齿轮牵引电机侧",
            "小齿轮车轮侧",
            "大齿轮车轮侧"
        };

        private readonly string[] m_Menu2 =
        {
            "温度℃",
            "小齿轮牵引电机侧",
            "大齿轮牵引电机侧",
            "小齿轮车轮侧",
            "大齿轮车轮侧",
            "小齿轮牵引电机侧",
            "大齿轮牵引电机侧",
            "小齿轮车轮侧",
            "大齿轮车轮侧"
        };

        private readonly string[] m_Menu3 = {"温度℃", "电机轴承驱动侧", "电机轴承非驱动侧", "牵引点击定子", "电机轴承驱动侧", "电机轴承非驱动侧", "牵引点击定子"};
        private readonly string[] m_Menu4 = {"温度℃", "电机轴承驱动侧", "电机轴承非驱动侧", "牵引点击定子", "电机轴承驱动侧", "电机轴承非驱动侧", "牵引点击定子"};
        private readonly string[] m_Title = {"系统; 齿轮箱温度 动车组1", "系统; 齿轮箱温度 动车组2", "系统; 牵引电机温度 动车组1", "系统; 牵引电机温度 动车组1"};

        private readonly string[] m_ButtomStr =
        {
            "齿轮箱\r\n动车组1",
            "齿轮箱\r\n动车组2",
            "电机\r\n动车组1",
            "电机\r\n动车组2",
            "",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private int m_CurrentMenu;

        private int CurrentMenu
        {
            get { return m_CurrentMenu; }
            set
            {
                if (m_CurrentMenu == value)
                {
                    return;
                }

                m_CurrentMenu = value;
                m_GdiTemperaturesCollection.ForEach(e => e.NavigationTo(m_CurrentMenu));
                ResetButtonList();
            }
        }

        private void InitData()
        {
            m_TemperatureTrainViews = new List<TemperatureTrainView>
            {
                new TemperatureTrainView(false, new Point(200, 70), new List<CommonInnerControlBase>()),
                new TemperatureTrainView(true, new Point(760, 290), new List<CommonInnerControlBase>())
            };
            m_GdiTemperaturesCollection = new List<GDITemperature>();
            var textSize = new Size(200, 30);
            var numberSzie = new Size(30, 30);
            m_Rectangle = new List<RectangleF>
            {
                new RectangleF(40, 50, textSize.Width, textSize.Height),
                new RectangleF(40, 100, textSize.Width, textSize.Height),
                new RectangleF(40, 140, textSize.Width, textSize.Height),
                new RectangleF(40, 180, textSize.Width, textSize.Height),
                new RectangleF(40, 220, textSize.Width, textSize.Height),
                new RectangleF(40, 320, textSize.Width, textSize.Height),
                new RectangleF(40, 360, textSize.Width, textSize.Height),
                new RectangleF(40, 400, textSize.Width, textSize.Height),
                new RectangleF(40, 440, textSize.Width, textSize.Height),
                new RectangleF(178, 0, 450, 25),
                new RectangleF(40, 50, textSize.Width, textSize.Height),
                new RectangleF(40, 100, textSize.Width, textSize.Height),
                new RectangleF(40, 150, textSize.Width, textSize.Height),
                new RectangleF(40, 200, textSize.Width, textSize.Height),
                new RectangleF(40, 320, textSize.Width, textSize.Height),
                new RectangleF(40, 370, textSize.Width, textSize.Height),
                new RectangleF(40, 420, textSize.Width, textSize.Height),
                new RectangleF(295, 35, numberSzie.Width, numberSzie.Height),
                new RectangleF(590, 35, numberSzie.Width, numberSzie.Height),
                new RectangleF(335, 255, numberSzie.Width, numberSzie.Height),
                new RectangleF(630, 255, numberSzie.Width, numberSzie.Height)
            };

            ReadPointList();

            DMITitle.MarshallModeChanged += m => ResetButtonList();

        }

        private void ResetButtonList()
        {
            if (DMITitle.CurrentViewID != 322)
            {
                return;
            }

            var tmp = m_ButtomStr.Clone() as string[];
            if (tmp == null)
            {
                return;
            }

            tmp[CurrentMenu] = string.Empty;
            if (!DMITitle.MarshallMode)
            {
                tmp[1] = string.Empty;
                tmp[3] = string.Empty;
            }
            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = tmp[i];
            }

            DMITitle.TitleName = m_Title[m_CurrentMenu];
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                CurrentMenu = 0;
                ResetButtonList();
            }
        }

        private List<GDITemperature> m_GdiTemperaturesCollection;

        /// <summary>
        /// 读取温度界面  坐标  以及  Float接口索引
        /// </summary>
        private void ReadPointList()
        {
            var excel = Path.Combine(DataPackage.Config.SystemDicrectory.BaseDirectory, @"Config\CRH380B温度配置.xls");
            var config = new ExcelReaderConfig
            {
                File = excel,
                Coloumns = new List<ColoumnConfig>
                {
                    new ColoumnConfig
                    {
                        Name = "*",
                        IsPrimaryKey = false
                    }
                },
                SheetNames = new List<string>
                {
                    "温度界面"
                }
            };
            var dt = config.Adapter();
            foreach (DataRow row in dt.Tables[0].Rows)
            {

                if (m_GdiTemperaturesCollection.Find(f => f.ID == int.Parse(row["ID"].ToString())) == null)
                {
                    var tmp = new GDITemperature(this, int.Parse(row["ID"].ToString()),
                        int.Parse(row["MenuID"].ToString()));
                    tmp.InFoaltDictionary.Add(int.Parse(row["MenuID"].ToString()),
                        IndexDescriptionConfig.InFloatDescriptionDictionary[row["InFloat"].ToString()]);

                    tmp.OutRectangle.Add(int.Parse(row["MenuID"].ToString()),
                        new RectangleF(float.Parse(row["X"].ToString()), float.Parse(row["Y"].ToString()),
                            float.Parse(row["Width"].ToString()), float.Parse(row["Height"].ToString())));
                    tmp.DrawFont = FontsItems.FontC11;
                    tmp.BackgroundColor = SolidBrushsItems.WhiteBrush;
                    m_GdiTemperaturesCollection.Add(tmp);
                }
                else
                {
                    var index = m_GdiTemperaturesCollection.FindIndex(f => f.ID == int.Parse(row["ID"].ToString()));
                    m_GdiTemperaturesCollection[index].InFoaltDictionary.Add(int.Parse(row["MenuID"].ToString()),
                        IndexDescriptionConfig.InFloatDescriptionDictionary[row["InFloat"].ToString()]);

                    m_GdiTemperaturesCollection[index].OutRectangle.Add(int.Parse(row["MenuID"].ToString()),
                        new RectangleF(float.Parse(row["X"].ToString()), float.Parse(row["Y"].ToString()),
                            float.Parse(row["Width"].ToString()), float.Parse(row["Height"].ToString())));
                }
            }
        }

        public void GetValue()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 120, 0, 0);
                    break;
                case 6:
                    CurrentMenu = 0;
                    break;
                case 7:
                    if (DMITitle.MarshallMode)
                    {
                        CurrentMenu = 1;
                    }
                    break;
                case 8:
                    CurrentMenu = 2;
                    break;
                case 9:
                    if (DMITitle.MarshallMode)
                    {
                        CurrentMenu = 3;
                    }
                    break;
                case 15:
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();
            m_TemperatureTrainViews.ForEach(e => e.OnDraw(g));
            m_GdiTemperaturesCollection.ForEach(e => e.OnDraw(g));
            if (m_CurrentMenu == 0 || m_CurrentMenu == 1)
            {
                for (int i = 0; i < 9; i++)
                {
                    g.DrawString(m_CurrentMenu == 0 ? m_Menu1[i] : m_Menu2[i], FontsItems.FontC11,
                        SolidBrushsItems.WhiteBrush, m_Rectangle[i], FontsItems.TheAlignment(FontRelated.靠左));
                }
            }
            if (m_CurrentMenu == 2 || m_CurrentMenu == 3)
            {
                for (int i = 0; i < 7; i++)
                {
                    g.DrawString(m_CurrentMenu == 2 ? m_Menu3[i] : m_Menu4[i], FontsItems.FontC11,
                        SolidBrushsItems.WhiteBrush, m_Rectangle[i + 10], FontsItems.TheAlignment(FontRelated.靠左));
                }
            }

            g.DrawString(m_CurrentMenu == 0 || m_CurrentMenu == 2 ? "1" : "11", FontsItems.FontC11,
                SolidBrushsItems.WhiteBrush, m_Rectangle[17], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(m_CurrentMenu == 0 || m_CurrentMenu == 2 ? "3" : "13", FontsItems.FontC11,
                SolidBrushsItems.WhiteBrush, m_Rectangle[18], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(m_CurrentMenu == 0 || m_CurrentMenu == 2 ? "6" : "16", FontsItems.FontC11,
                SolidBrushsItems.WhiteBrush, m_Rectangle[19], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(m_CurrentMenu == 0 || m_CurrentMenu == 2 ? "8" : "18", FontsItems.FontC11,
                SolidBrushsItems.WhiteBrush, m_Rectangle[20], FontsItems.TheAlignment(FontRelated.居中));

        }
    }
}
