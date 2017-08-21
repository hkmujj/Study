using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TrainWeightInput : baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10]; //底部数字按钮是否按下
        private readonly bool[] m_IsRightButtonDown = new bool[6]; //右边按钮是否按下
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>(); //界面中需要的界面元素
        private readonly Rectangle[] m_Rect = new Rectangle[6]; //各参数 输入区域边框 

        private readonly HxRectText[] m_TxtWeightValue = new HxRectText[5]; //列车重量使用值
        private readonly HxRectText[] m_TxtWeightInput = new HxRectText[5]; //列车重量输入值

        private readonly bool[] m_Select = new bool[8] {true, false, false, false, false, false, false, false};
            //被选中的文本框的select为true;

        private int m_Index = 0;
        private readonly int[] m_InputNum = new int[8]; //记录每个输入框输入的数值
        private bool m_IsCutOut = false; //投入或切除
        private HxRectText m_TxtTrainTypeValue; //列车类型使用值
        private HxRectText m_TxtTrainTypeInput; //列车类型输入值

        private HxRectText m_TxtTrainAxesValue; //列车轴重使用值
        private HxRectText m_TxtTrainAxesInput; //列车轴重输入值

        private HxRectText m_TxtTrainUnionValue; //机车联挂速度范围使用值
        private HxRectText m_TxtTrainUnionInput; //机车联挂速度输入值

        private Rectangle m_WeightRect;
        private Rectangle m_TrainTypeRect;
        private Rectangle m_TrainAxesRect;
        private Rectangle m_TrainUnion;

        private int m_TrainWeight = 0;
        private int m_TrainType = 0;
        private int m_TrainAxes = 0;
        private float m_TrainUnionf = 0f;
        private float m_PointDistance;

        private readonly Rectangle[] m_NavigateIcon = new Rectangle[5]; //导航图标
        private readonly SolidBrush m_IconBrush = new SolidBrush(Color.FromArgb(33, 36, 33)); //导航图标背景刷
        private readonly SolidBrush m_YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0)); //黄色背景画刷
        private readonly Pen m_IconPen = new Pen(Color.FromArgb(89, 92, 89), 2); //导航图标边框画笔
        private bool m_IsConfig = false;

        private List<int> m_RightBtnIdxs;

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 3)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }
                for (int i = 0; i < m_IsRightButtonDown.Length; i++)
                {
                    m_IsRightButtonDown[i] = false;
                }
                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int) (btn - 800)] = true;
                }
                var idx = m_RightBtnIdxs.IndexOf((int) btn);
                if (idx != -1)
                {
                    m_IsRightButtonDown[idx] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 3)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                } 
                
                for (int i = 0; i < m_IsRightButtonDown.Length; i++)
                {
                    m_IsRightButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }

        public override string GetInfo()
        {
            return "列车重量输入";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_RightBtnIdxs = Enumerable.Range(0, 6).Select(s => UIObj.InBoolList[s + 10]).ToList();

            GlobalParam.Instance.AddButtonEventListener(this);

            //加载图片
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Image image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                m_ImageList.Add(i, image);
            }

            InitData();
            nErrorObjectIndex = -1;
            return true;
        }

        public void InitData()
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    m_Rect[i] = new Rectangle(HxCommon.Recposition.X + 20, HxCommon.Recposition.Y + 35, 560, 125);
                }
                else if (i == 1 || i == 2)
                {
                    m_Rect[i] = new Rectangle(HxCommon.Recposition.X + 20 + 283*(i - 1),
                        HxCommon.Recposition.Y + 35 + 130, 280, 125);
                }
                else
                {
                    m_Rect[i] = new Rectangle(m_Rect[1].X, m_Rect[1].Bottom + 3 + (i - 3)*56, 560, 53);
                }
            }

            m_WeightRect = new Rectangle(m_Rect[0].X + 200, m_Rect[0].Y + 6, 310, 65);
            m_TrainTypeRect = new Rectangle(m_Rect[1].X + 105, m_Rect[1].Y + 6, 150, 65);
            m_TrainAxesRect = new Rectangle(m_Rect[2].X + 105, m_Rect[2].Y + 6, 150, 65);
            m_TrainUnion = new Rectangle(m_Rect[3].X + 195, m_Rect[3].Y + 4, 175, 45);

            //显示的文本框的初始化
            for (int i = 0; i < 5; i++)
            {
                m_TxtWeightValue[i] = new HxRectText();
                m_TxtWeightValue[i].SetTextColor(255, 255, 255);
                m_TxtWeightValue[i].SetTextStyle(14, FormatStyle.Center, true, "宋体");
                m_TxtWeightValue[i].SetDrawFrm(true);
                m_TxtWeightValue[i].SetLinePen(255, 255, 255, 2);
                m_TxtWeightValue[i].SetTextRect(m_WeightRect.X + 95 + i*30, m_WeightRect.Y + 10, 25, 35);
                m_TxtWeightValue[i].SetBkColor(0, 0, 0);
            }

            //输入文本框的初始化
            for (int i = 0; i < 5; i++)
            {
                m_TxtWeightInput[i] = new HxRectText();
                m_TxtWeightInput[i].SetBkColor(0, 0, 0);
                m_TxtWeightInput[i].SetTextColor(255, 255, 255);
                m_TxtWeightInput[i].SetTextStyle(14, FormatStyle.Center, true, "宋体");
                m_TxtWeightInput[i].SetDrawFrm(true);
                m_TxtWeightInput[i].SetLinePen(255, 255, 255, 2);
                m_TxtWeightInput[i].SetTextRect(m_WeightRect.X + 95 + i*30, m_WeightRect.Bottom + 10, 25, 35);
            }

            #region 列车类型

            m_TxtTrainTypeValue = new HxRectText();
            m_TxtTrainTypeValue.SetBkColor(0, 0, 0);
            m_TxtTrainTypeValue.SetTextColor(255, 255, 255);
            m_TxtTrainTypeValue.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_TxtTrainTypeValue.SetDrawFrm(true);
            m_TxtTrainTypeValue.SetLinePen(255, 255, 255, 2);
            m_TxtTrainTypeValue.SetTextRect(m_TrainTypeRect.X + 95, m_TrainTypeRect.Y + 15, 25, 40);

            m_TxtTrainTypeInput = new HxRectText();
            m_TxtTrainTypeInput.SetBkColor(0, 0, 0);
            m_TxtTrainTypeInput.SetTextColor(255, 255, 255);
            m_TxtTrainTypeInput.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_TxtTrainTypeInput.SetDrawFrm(true);
            m_TxtTrainTypeInput.SetLinePen(255, 255, 255, 2);
            m_TxtTrainTypeInput.SetTextRect(m_TrainTypeRect.X + 95, m_TrainTypeRect.Bottom + 10, 25, 40);

            #endregion

            #region 列 车 轴 重

            m_TxtTrainAxesValue = new HxRectText();
            m_TxtTrainAxesValue.SetBkColor(0, 0, 0);
            m_TxtTrainAxesValue.SetTextColor(255, 255, 255);
            m_TxtTrainAxesValue.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_TxtTrainAxesValue.SetDrawFrm(true);
            m_TxtTrainAxesValue.SetLinePen(255, 255, 255, 2);
            m_TxtTrainAxesValue.SetTextRect(m_TrainAxesRect.X + 95, m_TrainAxesRect.Y + 15, 25, 40);

            m_TxtTrainAxesInput = new HxRectText();
            m_TxtTrainAxesInput.SetBkColor(0, 0, 0);
            m_TxtTrainAxesInput.SetTextColor(255, 255, 255);
            m_TxtTrainAxesInput.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_TxtTrainAxesInput.SetDrawFrm(true);
            m_TxtTrainAxesInput.SetLinePen(255, 255, 255, 2);
            m_TxtTrainAxesInput.SetTextRect(m_TrainAxesRect.X + 95, m_TrainAxesRect.Bottom + 10, 25, 40);

            #endregion

            #region  机 车 联 挂

            m_TxtTrainUnionValue = new HxRectText();
            m_TxtTrainUnionValue.SetBkColor(0, 0, 0);
            m_TxtTrainUnionValue.SetTextColor(255, 255, 255);
            m_TxtTrainUnionValue.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_TxtTrainUnionValue.SetDrawFrm(true);
            m_TxtTrainUnionValue.SetLinePen(255, 255, 255, 2);
            m_TxtTrainUnionValue.SetTextRect(m_TrainUnion.X + 95, m_TrainUnion.Y + 5, 25, 35);

            m_TxtTrainUnionInput = new HxRectText();
            m_TxtTrainUnionInput.SetBkColor(0, 0, 0);
            m_TxtTrainUnionInput.SetTextColor(255, 255, 255);
            m_TxtTrainUnionInput.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_TxtTrainUnionInput.SetDrawFrm(true);
            m_TxtTrainUnionInput.SetLinePen(255, 255, 255, 2);
            m_TxtTrainUnionInput.SetTextRect(m_TrainUnion.Right + 35, m_TrainUnion.Y + 5, 25, 35);

            #endregion

            //导航图标区初始化
            for (int i = 0; i < 5; i++)
            {
                if (i < 2)
                {
                    m_NavigateIcon[i] = new Rectangle(HxCommon.Recposition.X + 725, HxCommon.Recposition.Y + 75 + 62*i,
                        65, 55);
                }
                else
                {
                    m_NavigateIcon[i] = new Rectangle(HxCommon.Recposition.X + 725,
                        HxCommon.Recposition.Y + 280 + 62*(i - 2), 65, 55);
                }
            }
        }

        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            base.paint(g);
        }

        public void GetValue()
        {
            //设置标题
            HxCommon.HTitle.SetText("列车参数输入");

            HxCommon.ButtonText[0].SetText("1");
            HxCommon.ButtonText[1].SetText("2");
            HxCommon.ButtonText[2].SetText("3");
            HxCommon.ButtonText[3].SetText("4");
            HxCommon.ButtonText[4].SetText("5");
            HxCommon.ButtonText[5].SetText("6");
            HxCommon.ButtonText[6].SetText("7");
            HxCommon.ButtonText[7].SetText("8");
            HxCommon.ButtonText[8].SetText("9");
            HxCommon.ButtonText[9].SetText("0");
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
            }

            m_TrainWeight = Convert.ToInt32(FloatList[UIObj.InFloatList[0]]);
            m_TrainType = Convert.ToInt32(FloatList[UIObj.InFloatList[1]]);

            m_TxtWeightValue[0].SetText((m_TrainWeight/10000).ToString());
            m_TxtWeightValue[1].SetText((m_TrainWeight/1000%10).ToString());
            m_TxtWeightValue[2].SetText((m_TrainWeight/100%10).ToString());
            m_TxtWeightValue[3].SetText((m_TrainWeight/10%10).ToString());
            m_TxtWeightValue[4].SetText((m_TrainWeight%10).ToString());

            m_TrainAxes = Convert.ToInt32(FloatList[UIObj.InFloatList[2]]);
            m_TrainUnionf = FloatList[UIObj.InFloatList[3]];
            m_PointDistance = FloatList[UIObj.InFloatList[4]];

            m_TxtTrainAxesValue.SetText(m_TrainAxes.ToString());
            m_TxtTrainUnionValue.SetText(m_TrainUnionf.ToString());

            if (m_TrainType > 0 && m_TrainType < 4)
            {
                m_TxtTrainTypeValue.SetText(m_TrainType.ToString());
            }
            else
            {
                m_TxtTrainTypeValue.SetText("0");
            }
            for (int i = 0; i < 8; i++)
            {
                if (i < 5)
                {
                    if (m_Select[i])
                    {
                        m_TxtWeightInput[i].SetBkColor(0, 0, 255);
                    }
                    else
                    {
                        m_TxtWeightInput[i].SetBkColor(0, 0, 0);
                    }
                }
                else if (i == 5)
                {
                    if (m_Select[i])
                    {
                        m_TxtTrainTypeInput.SetBkColor(0, 0, 255);
                    }
                    else
                    {
                        m_TxtTrainTypeInput.SetBkColor(0, 0, 0);
                    }
                }
                else if (i == 6)
                {
                    if (m_Select[i])
                    {
                        m_TxtTrainAxesInput.SetBkColor(0, 0, 255);
                    }
                    else
                    {
                        m_TxtTrainAxesInput.SetBkColor(0, 0, 0);
                    }
                }
                else if (i == 7)
                {
                    if (m_Select[i])
                    {
                        m_TxtTrainUnionInput.SetBkColor(0, 0, 255);
                    }
                    else
                    {
                        m_TxtTrainUnionInput.SetBkColor(0, 0, 0);
                    }
                }
                else
                {
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (i < 5)
                {
                    m_TxtWeightInput[i].SetText(m_InputNum[i].ToString());
                }
                else if (i == 5)
                {
                    if (m_InputNum[i] >= 1 && m_InputNum[i] <= 3)
                    {
                        m_TxtTrainTypeInput.SetText(m_InputNum[i].ToString());
                    }

                }
                else if (i == 6)
                {
                    if (m_InputNum[i] >= 1 && m_InputNum[i] <= 3)
                    {
                        m_TxtTrainAxesInput.SetText(m_InputNum[i].ToString());
                    }

                }
                else
                {
                    if (m_InputNum[i] >= 1 && m_InputNum[i] <= 3)
                    {
                        m_TxtTrainUnionInput.SetText(m_InputNum[i].ToString());
                    }

                }

            }

            #region 各状态信息Bool 量的读入

            //底部数字按钮
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i]];
            }

            //右边数值按钮
            for (int i = 0; i < 6; i++)
            {
                m_IsRightButtonDown[i] = BoolList[UIObj.InBoolList[i + 10]];
            }

            #endregion
        }

        public void DrawOn(Graphics g)
        {
            //底部导航按钮绘制
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //的矩形框绘制
            for (int i = 0; i < 6; i++)
            {
                g.DrawRectangle(HxCommon.LinePen2, m_Rect[i]);
            }

            //列车重量
            g.DrawRectangle(HxCommon.LinePen2, m_WeightRect);
            g.DrawString("列车重量", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[0].X + 30, m_Rect[0].Y + 20);
            g.DrawString("(<=25000t)", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[0].X + 28, m_Rect[0].Y + 50);

            for (int i = 0; i < 5; i++)
            {
                m_TxtWeightValue[i].OnDraw(g);
                m_TxtWeightInput[i].OnDraw(g);
            }

            g.DrawString("使用值", HxCommon.Font16B, HxCommon.WhiteBrush, m_WeightRect.X + 8, m_WeightRect.Y + 20);
            g.DrawString("t", HxCommon.Font16B, HxCommon.WhiteBrush, m_WeightRect.X + 238, m_WeightRect.Y + 20);

            //列车类型
            g.DrawRectangle(HxCommon.LinePen2, m_TrainTypeRect);
            g.DrawString("列车类型", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[1].X + 12, m_Rect[1].Y + 25);
            g.DrawString("1=客车", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[1].X + 26, m_Rect[1].Y + 50);
            g.DrawString("2=货车", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[1].X + 26, m_Rect[1].Y + 75);
            g.DrawString("3=油罐车", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[1].X + 26, m_Rect[1].Y + 100);
            m_TxtTrainTypeValue.OnDraw(g);
            m_TxtTrainTypeInput.OnDraw(g);
            g.DrawString("使用值", HxCommon.Font16B, HxCommon.WhiteBrush, m_TrainTypeRect.X + 8, m_TrainTypeRect.Y + 20);
            g.DrawString("t", HxCommon.Font16B, HxCommon.WhiteBrush, m_TrainTypeRect.X + 130, m_TrainTypeRect.Y + 20);

            //列车轴重
            g.DrawRectangle(HxCommon.LinePen2, m_TrainAxesRect);
            g.DrawString("列车轴重", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[2].X + 12, m_Rect[2].Y + 25);
            g.DrawString("1=25t", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[2].X + 26, m_Rect[2].Y + 50);
            g.DrawString("2=23t", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[2].X + 26, m_Rect[2].Y + 75);
            g.DrawString("3=21t", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[2].X + 26, m_Rect[2].Y + 100);
            m_TxtTrainAxesValue.OnDraw(g);
            m_TxtTrainAxesInput.OnDraw(g);
            g.DrawString("使用值", HxCommon.Font16B, HxCommon.WhiteBrush, m_TrainAxesRect.X + 8, m_TrainAxesRect.Y + 20);
            g.DrawString("t", HxCommon.Font16B, HxCommon.WhiteBrush, m_TrainAxesRect.X + 130, m_TrainAxesRect.Y + 20);

            //机车联挂
            g.DrawRectangle(HxCommon.LinePen2, m_TrainUnion);
            m_TxtTrainUnionValue.OnDraw(g);
            m_TxtTrainUnionInput.OnDraw(g);
            g.DrawString("机车联挂速度范围", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[3].X + 8, m_Rect[3].Y + 10);
            g.DrawString("(1-3Km/h)", HxCommon.Font12B, HxCommon.WhiteBrush, m_Rect[3].X + 36, m_Rect[3].Y + 30);

            g.DrawString("使用值", HxCommon.Font12B, HxCommon.WhiteBrush, m_TrainUnion.X + 8, m_TrainUnion.Y + 15);
            g.DrawString("Km/h", HxCommon.Font12B, HxCommon.WhiteBrush, m_TxtTrainUnionValue.m_RectPosition.Right + 5,
                m_TxtTrainUnionValue.m_RectPosition.Y + 10);
            g.DrawString("Km/h", HxCommon.Font12B, HxCommon.WhiteBrush, m_TxtTrainUnionInput.m_RectPosition.Right + 5,
                m_TxtTrainUnionInput.m_RectPosition.Y + 10);

            //自动过分相装置
            g.DrawString("自动过分相装置", HxCommon.Font16B, HxCommon.WhiteBrush, m_Rect[4].X + 8, m_Rect[4].Y + 10);
            g.DrawString("(按【^】)键选择1", HxCommon.Font12B, HxCommon.WhiteBrush, m_Rect[4].X + 6, m_Rect[4].Y + 30);

            if (!m_IsCutOut) //投入
            {
                g.DrawEllipse(HxCommon.WhitePen2, m_Rect[4].X + 200, m_Rect[4].Y + 15, 20, 20);
                g.FillEllipse(HxCommon.WhiteBrush, m_Rect[4].X + 203, m_Rect[4].Y + 18, 14, 14);

                g.DrawEllipse(HxCommon.WhitePen2, m_Rect[4].X + 300, m_Rect[4].Y + 15, 20, 20);
            }
            else
            {
                g.DrawEllipse(HxCommon.WhitePen2, m_Rect[4].X + 200, m_Rect[4].Y + 15, 20, 20);

                g.FillEllipse(HxCommon.WhiteBrush, m_Rect[4].X + 303, m_Rect[4].Y + 18, 14, 14);
                g.DrawEllipse(HxCommon.WhitePen2, m_Rect[4].X + 300, m_Rect[4].Y + 15, 20, 20);
            }

            g.DrawString("投入", HxCommon.Font14B, HxCommon.WhiteBrush, m_Rect[4].X + 230, m_Rect[4].Y + 15);
            g.DrawString("切除", HxCommon.Font14B, HxCommon.WhiteBrush, m_Rect[4].X + 330, m_Rect[4].Y + 15);

            //过分相G1 G2点距离
            g.DrawString("过分相G1 G2点距离", HxCommon.Font14B, HxCommon.WhiteBrush, m_Rect[5].X + 20, m_Rect[5].Y + 6);
            g.FillEllipse(HxCommon.WhiteBrush, m_Rect[4].X + 203, m_Rect[5].Y + 8, 14, 14);
            g.DrawEllipse(HxCommon.WhitePen2, m_Rect[4].X + 200, m_Rect[5].Y + 5, 20, 20);

            g.DrawString("当前使用值", HxCommon.Font12B, HxCommon.WhiteBrush, m_Rect[5].X + 200, m_Rect[5].Y + 36);

            g.DrawString("170m", HxCommon.Font14B, HxCommon.WhiteBrush, m_Rect[5].X + 230, m_Rect[5].Y + 5);

            g.DrawString(m_PointDistance.ToString() + "m", HxCommon.Font14B, HxCommon.WhiteBrush, m_Rect[5].X + 302,
                m_Rect[5].Y + 32);
            g.DrawRectangle(HxCommon.WhitePen2, m_Rect[5].X + 295, m_Rect[5].Y + 27, 50, 25);

            //绘制导航区图标
            for (int i = 0; i < 4; i++)
            {
                g.FillRectangle(m_IconBrush, m_NavigateIcon[i]);
                g.DrawLine(m_IconPen, m_NavigateIcon[i].X + 3, m_NavigateIcon[i].Bottom + 3, m_NavigateIcon[i].Right + 3,
                    m_NavigateIcon[i].Bottom + 3);
                g.DrawLine(m_IconPen, m_NavigateIcon[i].Right + 3, m_NavigateIcon[i].Y + 3, m_NavigateIcon[i].Right + 3,
                    m_NavigateIcon[i].Bottom + 3);
            }

            if (m_IsConfig)
            {
                g.FillRectangle(m_YellowBrush, m_NavigateIcon[4]);
                g.DrawLine(m_IconPen, m_NavigateIcon[4].X + 3, m_NavigateIcon[4].Bottom + 3, m_NavigateIcon[4].Right + 3,
                    m_NavigateIcon[4].Bottom + 3);
                g.DrawLine(m_IconPen, m_NavigateIcon[4].Right + 3, m_NavigateIcon[4].Y + 3, m_NavigateIcon[4].Right + 3,
                    m_NavigateIcon[4].Bottom + 3);
            }
            else
            {
                g.FillRectangle(m_IconBrush, m_NavigateIcon[4]);
                g.DrawLine(m_IconPen, m_NavigateIcon[4].X + 3, m_NavigateIcon[4].Bottom + 3, m_NavigateIcon[4].Right + 3,
                    m_NavigateIcon[4].Bottom + 3);
                g.DrawLine(m_IconPen, m_NavigateIcon[4].Right + 3, m_NavigateIcon[4].Y + 3, m_NavigateIcon[4].Right + 3,
                    m_NavigateIcon[4].Bottom + 3);
            }

            g.DrawString("主", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[0].X + 20, m_NavigateIcon[0].Y + 10);
            g.DrawString("界面", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[0].X + 12, m_NavigateIcon[0].Y + 27);

            g.DrawString("选择1", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[1].X + 14,
                m_NavigateIcon[1].Y + 10);
            g.DrawString("^", HxCommon.Font24B, HxCommon.WhiteBrush, m_NavigateIcon[1].X + 22, m_NavigateIcon[1].Y + 30);
            g.DrawString("[ ]", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[1].X + 18,
                m_NavigateIcon[1].Y + 27);


            g.DrawString("后退", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[2].X + 18, m_NavigateIcon[2].Y + 10);
            g.DrawString("[>]", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[2].X + 22,
                m_NavigateIcon[2].Y + 27);

            g.DrawString("前进", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[3].X + 18, m_NavigateIcon[3].Y + 10);
            g.DrawString("[<]", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[3].X + 22,
                m_NavigateIcon[3].Y + 27);

            if (m_IsConfig)
            {
                g.DrawString("确认", HxCommon.Font12B, Brushes.Black, m_NavigateIcon[4].X + 18, m_NavigateIcon[4].Y + 5);
                g.DrawString("输入", HxCommon.Font12B, Brushes.Black, m_NavigateIcon[4].X + 18, m_NavigateIcon[4].Y + 22);
                g.DrawString("[E]", HxCommon.Font12B, Brushes.Black, m_NavigateIcon[4].X + 22, m_NavigateIcon[4].Y + 39);
            }
            else
            {
                g.DrawString("确认", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[4].X + 18,
                    m_NavigateIcon[4].Y + 5);
                g.DrawString("输入", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[4].X + 18,
                    m_NavigateIcon[4].Y + 22);
                g.DrawString("[E]", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[4].X + 22,
                    m_NavigateIcon[4].Y + 39);
            }


            g.DrawImage(m_ImageList[0], m_Rect[2].Right + 20, m_Rect[1].Y + 30);
        }

        /// <summary>
        /// 响应硬件按钮按下事件
        /// </summary>
        public void ButtonDownEvent()
        {
            //响应底部按钮
            for (int i = 0; i < 10; i++)
            {
                if (m_IsNumButtonDown[i])
                {
                    m_IsConfig = false;
                    switch (i)
                    {
                        case 0:
                            m_InputNum[m_Index] = 1;
                            if (m_Index < 4)
                            {
                                m_Index++;
                            }

                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }

                            break;
                        case 1:
                            m_InputNum[m_Index] = 2;
                            if (m_Index < 4)
                            {
                                m_Index++;
                            }

                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }

                            break;
                        case 2:
                            m_InputNum[m_Index] = 3;
                            if (m_Index < 4)
                            {
                                m_Index++;
                            }
                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }

                            break;
                        case 3:
                            m_InputNum[m_Index] = 4;
                            if (m_Index < 4)
                            {
                                m_Index++;
                            }

                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }

                            break;
                        case 4:
                            m_InputNum[m_Index] = 5;
                            if (m_Index < 4)
                            {
                                m_Index++;
                            }

                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }

                            break;
                        case 5:
                            m_InputNum[m_Index] = 6;
                            if (m_Index < 4)
                            {
                                m_Index++;
                            }

                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }

                            break;
                        case 6:
                            m_InputNum[m_Index] = 7;
                            if (m_Index < 4)
                            {
                                m_Index++;
                            }

                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }

                            break;
                        case 7:
                            m_InputNum[m_Index] = 8;
                            if (m_Index < 4)
                            {
                                m_Index++;
                            }

                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }

                            break;
                        case 8:
                            m_InputNum[m_Index] = 9;
                            if (m_Index < 7)
                            {
                                m_Index++;
                            }

                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }

                            break;
                        case 9:
                            m_InputNum[m_Index] = 0;
                            if (m_Index < 7)
                            {
                                m_Index++;
                            }

                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }

            #region 响应右边按钮

            for (int i = 0; i < 6; i++)
            {
                if (m_IsRightButtonDown[i])
                {
                    if (i != 5)
                    {
                        m_IsConfig = false;
                    }

                    if (i == 0) //点击取消按钮 返回主界面
                    {
                        append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    }
                    else if (i == 1) //点击【^】切换
                    {
                        m_IsCutOut = !m_IsCutOut;
                    }
                    else if (i == 3)
                    {
                        if (m_Index > 0)
                        {
                            m_Index--;
                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }
                        }
                    }
                    else if (i == 4)
                    {
                        if (m_Index < 7)
                        {
                            m_Index++;
                            for (int j = 0; j < 8; j++)
                            {
                                if (j == m_Index)
                                {
                                    m_Select[j] = true;
                                }
                                else
                                {
                                    m_Select[j] = false;
                                }
                            }
                        }
                    }
                    else if (i == 5)
                    {
                        if (m_Index < 5) //更新列车重量
                        {
                            int valueWeight = m_InputNum[0]*10000 + m_InputNum[1]*1000 + m_InputNum[2]*100 + m_InputNum[3]*10 +
                                              m_InputNum[4];
                            if (valueWeight <= 25000)
                            {
                                m_Index++;
                                for (int j = 0; j < 8; j++)
                                {
                                    if (j == m_Index)
                                    {
                                        m_Select[j] = true;
                                    }
                                    else
                                    {
                                        m_Select[j] = false;
                                    }
                                }
                                m_IsConfig = true;
                                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, valueWeight);

                            }

                        }
                        else if (m_Index == 5) //更新列车类型
                        {
                            if (m_InputNum[5] == 1 || m_InputNum[5] == 2 || m_InputNum[5] == 3) //只有类型为 1 2 3 时符合要求才更新
                            {
                                m_Index++;
                                for (int j = 0; j < 8; j++)
                                {
                                    if (j == m_Index)
                                    {
                                        m_Select[j] = true;
                                    }
                                    else
                                    {
                                        m_Select[j] = false;
                                    }
                                }
                                m_IsConfig = true;
                                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[1], 0, m_InputNum[5]);
                            }
                        }
                        else if (m_Index == 6)
                        {
                            if (m_InputNum[6] == 1 || m_InputNum[6] == 2 || m_InputNum[6] == 3) //只有类型为 1 2 3 时符合要求才更新
                            {
                                m_Index++;
                                for (int j = 0; j < 8; j++)
                                {
                                    if (j == m_Index)
                                    {
                                        m_Select[j] = true;
                                    }
                                    else
                                    {
                                        m_Select[j] = false;
                                    }
                                }
                                m_IsConfig = true;
                                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[2], 0, m_InputNum[6]);
                            }
                        }
                        else
                        {
                            if (m_InputNum[7] >= 1 && m_InputNum[7] <= 3) //只有类型为 1 2 3 时符合要求才更新
                            {
                                m_IsConfig = true;
                                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[3], 0, m_InputNum[7]);
                            }
                        }

                    }
                }
            }

            #endregion


        }
    }
}
