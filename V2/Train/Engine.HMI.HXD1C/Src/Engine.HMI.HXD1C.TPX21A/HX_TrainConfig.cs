using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class HX_TrainConfig : baseClass, IButtonEventListener
    {
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>(); //界面中需要的界面元素
        private readonly bool[] m_IsNumButtonDown = new bool[10]; //底部数字按钮是否按下
        private readonly bool[] m_IsCurrentCollector1 = new bool[3]; //受电弓1状态
        private readonly bool[] m_IsCurrentCollector2 = new bool[3]; //受电弓2状态
        private readonly bool[] m_IsDriverRooms = new bool[2]; //司机室占用情况
        private readonly bool[] m_IsMainSegment = new bool[4]; //主断状态
        private readonly bool[] m_IsElectricMachine = new bool[4]; //电机状态 
        private readonly bool[] m_IsStopBrake = new bool[4]; //停放制动
        private readonly bool[] m_IsTrainBrake = new bool[5]; //机车制动
        private readonly bool[] m_IsTrainTop1 = new bool[2]; //车顶隔离开关1状态
        private readonly bool[] m_IsTrainTop2 = new bool[2]; //车顶隔离开关2状态
        private int m_BCUMode;
        private readonly Image[] m_TrainStatusImages = new Image[16]; //表示车上受电弓和车顶隔离开关的组合状态的图片

        private Rectangle m_TrainRect;
        private Rectangle[] m_BottomRect = new Rectangle[6];
        private readonly Rectangle[] m_ImgRect = new Rectangle[7];

        public override string GetInfo()
        {
            return "机车配置";
        }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 16)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int) (btn - 800)] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 16)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            //加载图片
            for (int i = 0; i < 17; i++)
            {
                Image image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                m_ImageList.Add(i, image);

                if (i < 16)
                {
                    m_TrainStatusImages[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i + 17]);
                }

            }
            nErrorObjectIndex = -1;

            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            base.paint(g);
        }

        public void InitData()
        {
            //机车位置初始化
            m_TrainRect = new Rectangle(HxCommon.Recposition.X + 130, HxCommon.Recposition.Y + 50, 520, 120);

            //图标矩形框
            for (int i = 0; i < 7; i++)
            {
                if (i < 4)
                {
                    if (i%2 == 0)
                    {
                        m_ImgRect[i] = new Rectangle(m_TrainRect.X + 100, m_TrainRect.Bottom + 20 + i/2*61, 120, 60);
                    }
                    else
                    {
                        m_ImgRect[i] = new Rectangle(m_TrainRect.X + 260, m_TrainRect.Bottom + 20 + i/2*61, 120, 60);
                    }
                }
                else
                {
                    m_ImgRect[i] = new Rectangle(m_ImgRect[2].X + 65, m_ImgRect[2].Bottom + (i - 4)*61, 120, 60);
                }
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void GetValue()
        {
            //设置标题
            HxCommon.HTitle.SetText("牵引数据");

            HxCommon.ButtonText[0].SetText("主要数据");
            HxCommon.ButtonText[1].SetText("维护");
            HxCommon.ButtonText[2].SetText("");
            HxCommon.ButtonText[3].SetText("");
            HxCommon.ButtonText[4].SetText("列车重量");
            HxCommon.ButtonText[5].SetText("机车配置");
            HxCommon.ButtonText[6].SetText("牵引数据");
            HxCommon.ButtonText[7].SetText("");
            HxCommon.ButtonText[8].SetText(" ");
            HxCommon.ButtonText[9].SetText("主界面");
            for (int i = 0; i < 10; i++)
            {
                if (i == 5)
                {
                    HxCommon.ButtonText[i].SetBkColor(255, 255, 0);
                    HxCommon.ButtonText[i].SetTextColor(0, 0, 0);
                }
                else
                {
                    HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                    HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
                }
            }

            #region 各状态信息Bool 量的读入

            //底部数字按钮
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i]];
            }

            //受电弓1
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector1[i] = BoolList[UIObj.InBoolList[i + 10]];
            }

            //受电弓2
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector2[i] = BoolList[UIObj.InBoolList[i + 13]];
            }

            //司机室
            for (int i = 0; i < 2; i++)
            {
                m_IsDriverRooms[i] = BoolList[UIObj.InBoolList[i + 16]];
            }

            //主断状态
            for (int i = 0; i < 4; i++)
            {
                m_IsMainSegment[i] = BoolList[UIObj.InBoolList[i + 18]];
            }

            //电机状态
            for (int i = 0; i < 4; i++)
            {
                m_IsElectricMachine[i] = BoolList[UIObj.InBoolList[i + 22]];
            }

            //停放制动
            for (int i = 0; i < 4; i++)
            {
                m_IsStopBrake[i] = BoolList[UIObj.InBoolList[i + 26]];
            }

            //机车制动
            for (int i = 0; i < 4; i++)
            {
                m_IsTrainBrake[i] = BoolList[UIObj.InBoolList[i + 30]];
            }
            m_IsTrainBrake[4] = BoolList[UIObj.InBoolList[UIObj.InBoolList.Count - 1]];

            #endregion

            //BCU 模式 读入
            m_BCUMode = Convert.ToInt32(FloatList[UIObj.InFloatList[0]]);

            //车顶隔离开关1
            for (int i = 0; i < 2; i++)
            {
                m_IsTrainTop1[i] = BoolList[UIObj.InBoolList[i + 34]];
            }

            //车顶隔离开关1
            for (int i = 0; i < 2; i++)
            {
                m_IsTrainTop2[i] = BoolList[UIObj.InBoolList[i + 36]];
            }
        }

        public void DrawOn(Graphics g)
        {

            //绘制电机状态
            for (int i = 0; i < 4; i++)
            {
                if (m_IsElectricMachine[i])
                {
                    g.DrawImage(m_ImageList[i], m_ImgRect[0].X + 5, m_ImgRect[0].Y + 5, m_ImgRect[0].Width - 10,
                        m_ImgRect[0].Height - 10);
                    g.DrawImage(m_ImageList[i], m_ImgRect[1].X + 5, m_ImgRect[1].Y + 5, m_ImgRect[1].Width - 10,
                        m_ImgRect[1].Height - 10);
                    break;
                }
            }

            //停车制动
            for (int i = 0; i < 5; i++)
            {
                if (m_IsTrainBrake[i])
                {
                    if (i < 4)
                    {
                        g.DrawImage(m_ImageList[i + 4], m_ImgRect[2].X + 5, m_ImgRect[2].Y + 5, m_ImgRect[2].Width - 10,
                            m_ImgRect[2].Height - 10);
                        g.DrawImage(m_ImageList[i + 4], m_ImgRect[3].X + 5, m_ImgRect[3].Y + 5, m_ImgRect[3].Width - 10,
                            m_ImgRect[3].Height - 10);
                    }
                    else
                    {
                        g.DrawImage(m_ImageList[16], m_ImgRect[2].X + 5, m_ImgRect[2].Y + 5, m_ImgRect[2].Width - 10,
                            m_ImgRect[2].Height - 10);
                        g.DrawImage(m_ImageList[16], m_ImgRect[3].X + 5, m_ImgRect[3].Y + 5, m_ImgRect[3].Width - 10,
                            m_ImgRect[3].Height - 10);
                    }

                }
            }

            //停放制动
            for (int i = 0; i < 4; i++)
            {
                if (m_IsStopBrake[i])
                {
                    g.DrawImage(m_ImageList[i + 8], m_ImgRect[4].X + 5, m_ImgRect[4].Y + 5, m_ImgRect[2].Width - 10,
                        m_ImgRect[4].Height - 10);
                    break;
                }
            }

            //BCU 模式
            switch (m_BCUMode)
            {
                case 1:
                    g.DrawString("BCU", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 25, m_ImgRect[6].Y + 10);
                    g.DrawString("本机投入", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 5, m_ImgRect[6].Y + 30);
                    break;
                case 2:
                    g.DrawString("BCU", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 25, m_ImgRect[6].Y + 10);
                    g.DrawString("重联", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 25, m_ImgRect[6].Y + 30);
                    break;
                case 3:
                    g.DrawString("BCU", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 25, m_ImgRect[6].Y + 10);
                    g.DrawString("本机切除", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 5, m_ImgRect[6].Y + 30);
                    break;
                default:
                    break;

            }

            //底部导航按钮绘制
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //车身受电弓和车顶隔离开关组合状态
            if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[0] && m_IsTrainTop1[0] && m_IsTrainTop2[0]) //0gggg
            {
                g.DrawImage(m_TrainStatusImages[0], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[0] && m_IsTrainTop1[0] && m_IsTrainTop2[1]) //1gggk
            {
                g.DrawImage(m_TrainStatusImages[1], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[0] && m_IsTrainTop1[1] && m_IsTrainTop2[0]) //2ggkg
            {
                g.DrawImage(m_TrainStatusImages[2], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[0] && m_IsTrainTop1[1] && m_IsTrainTop2[1]) //3ggkk
            {
                g.DrawImage(m_TrainStatusImages[3], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[1] && m_IsTrainTop1[0] && m_IsTrainTop2[0]) //4gkgg
            {
                g.DrawImage(m_TrainStatusImages[4], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[1] && m_IsTrainTop1[0] && m_IsTrainTop2[1]) //5gkgk
            {
                g.DrawImage(m_TrainStatusImages[5], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[1] && m_IsTrainTop1[1] && m_IsTrainTop2[0]) //6gkkg
            {
                g.DrawImage(m_TrainStatusImages[6], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[1] && m_IsTrainTop1[1] && m_IsTrainTop2[1]) //7gkkk
            {
                g.DrawImage(m_TrainStatusImages[7], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[0] && m_IsTrainTop1[0] && m_IsTrainTop2[0]) //8kggg
            {
                g.DrawImage(m_TrainStatusImages[8], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[0] && m_IsTrainTop1[0] && m_IsTrainTop2[1]) //9kggk
            {
                g.DrawImage(m_TrainStatusImages[9], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[0] && m_IsTrainTop1[1] && m_IsTrainTop2[0]) //10kgkg
            {
                g.DrawImage(m_TrainStatusImages[10], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[0] && m_IsTrainTop1[1] && m_IsTrainTop2[1]) //11kgkk
            {
                g.DrawImage(m_TrainStatusImages[11], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[1] && m_IsTrainTop1[0] && m_IsTrainTop2[0]) //12kkgg
            {
                g.DrawImage(m_TrainStatusImages[12], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[1] && m_IsTrainTop1[0] && m_IsTrainTop2[1]) //13kkgk
            {
                g.DrawImage(m_TrainStatusImages[13], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[1] && m_IsTrainTop1[1] && m_IsTrainTop2[0]) //14kkkg
            {
                g.DrawImage(m_TrainStatusImages[14], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[1] && m_IsTrainTop1[1] && m_IsTrainTop2[1]) //15kkkk
            {
                g.DrawImage(m_TrainStatusImages[15], m_TrainRect);
            }





            for (int i = 0; i < 7; i++)
            {
                g.DrawRectangle(HxCommon.LinePen2, m_ImgRect[i]);
            }
        }

        /// <summary>
        /// 响应硬件按钮按下事件
        /// </summary>
        public void ButtonDownEvent()
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_IsNumButtonDown[i])
                {
                    switch (i)
                    {
                        case 0: //按下"1"键跳转到主要数据视图
                            append_postCmd(CmdType.ChangePage, 2, 0, 0);
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4: //按下5切换到列车参数视图
                            append_postCmd(CmdType.ChangePage, 3, 0, 0);
                            break;
                        case 5:
                            break;
                        case 6: //按下7切换到牵引数据界面
                            append_postCmd(CmdType.ChangePage, 4, 0, 0);
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9: //返回主界面
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}

   