using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class HX_TemperatureView : baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10]; //底部数字按钮是否按下
        private readonly HxRectText[] m_StrText = new HxRectText[18]; //显示各种状态的标题
        private readonly HxRectText[] m_StrDate = new HxRectText[18]; //显示各种状态的数据量

        private readonly string[] m_Text = new string[18]
        {
            "电机1温度", "电机2温度", "电机3温度", "电机4温度", "电机5温度", "电机6温度", "主变流器1冷却水温度", "主变流器2冷却水温度",
            "主变流器1柜体温度", "主变流器2柜体温度", "主变压器1冷却油温度", "主变压器2冷却油温度", "主变压器1冷却水压力", "主变压器2冷却水压力",
            "油流1状态", "油流2状态", "主变流器1中间直流电压", "主变流器2中间直流电压"
        };

        private readonly float[] m_Temperatures = new float[12]; //依次显示各量的温度
        private readonly float[] m_Presures = new float[2]; //主变压器冷却水压力
        private readonly float[] m_Voltages = new float[2]; //主变流器中间直流电压
        private readonly bool[] m_IsOilStatus = new bool[2]; //油流状态

        public override string GetInfo()
        {
            return "温度视图";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            nErrorObjectIndex = -1;

            return true;
        }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 5)
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
            if (GlobalParam.Instance.CurrentViewId == 5)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
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
            //文本框的初始化

            //标题文本框
            for (int i = 0; i < 18; i++)
            {
                m_StrText[i] = new HxRectText();
                m_StrText[i].SetBkColor(0, 0, 0);
                m_StrText[i].SetTextColor(255, 255, 255);

                if (i%2 == 0)
                {
                    m_StrText[i].SetTextRect(HxCommon.Recposition.X + 20, HxCommon.Recposition.Y + 50 + 43*(i/2), 250,
                        38);
                    m_StrText[i].SetTextStyle(14, FormatStyle.DirectionRightToLeft, true, "宋体");

                }
                else
                {
                    m_StrText[i].SetTextRect(HxCommon.Recposition.X + 510, HxCommon.Recposition.Y + 50 + 43*(i/2), 250,
                        38);
                    m_StrText[i].SetTextStyle(14, FormatStyle.DirectionLeftToRight, true, "宋体");

                }

                m_StrText[i].SetText(m_Text[i]);
            }

            //数据文本框
            for (int i = 0; i < 18; i++)
            {
                m_StrDate[i] = new HxRectText();
                m_StrDate[i].SetBkColor(0, 0, 0);
                m_StrDate[i].SetTextColor(255, 255, 255);
                m_StrDate[i].SetTextStyle(14, FormatStyle.Center, true, "宋体");

                if (i%2 == 0)
                {
                    m_StrDate[i].SetTextRect(m_StrText[i].m_RectPosition.Right + 15, m_StrText[i].m_RectPosition.Y, 100, 38);
                }
                else
                {
                    m_StrDate[i].SetTextRect(m_StrDate[i - 1].m_RectPosition.Right, m_StrDate[i - 1].m_RectPosition.Y, 100, 38);
                }

                m_StrDate[i].SetLinePen(84, 84, 84, 2);
                m_StrDate[i].SetDrawFrm(true);
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void GetValue()
        {
            //设置标题
            HxCommon.HTitle.SetText("温度界面");

            HxCommon.ButtonText[0].SetText("牵引数据");
            HxCommon.ButtonText[1].SetText("温度");
            HxCommon.ButtonText[2].SetText("网络");
            HxCommon.ButtonText[3].SetText("辅助系统");
            HxCommon.ButtonText[4].SetText("工作状态");
            HxCommon.ButtonText[5].SetText("无线重联");
            HxCommon.ButtonText[6].SetText(" ");
            HxCommon.ButtonText[7].SetText("辅机测试");
            HxCommon.ButtonText[8].SetText("库内动车");
            HxCommon.ButtonText[9].SetText("主界面");
            for (int i = 0; i < 10; i++)
            {
                if (i == 1)
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

            //油流状态
            for (int i = 0; i < 2; i++)
            {
                m_IsOilStatus[i] = BoolList[UIObj.InBoolList[i + 10]];
            }

            #endregion

            //各温度 及电压等float量的读入
            //温度
            for (int i = 0; i < 12; i++)
            {
                m_Temperatures[i] = FloatList[UIObj.InFloatList[i]];
            }

            //水压 电压
            for (int i = 0; i < 2; i++)
            {
                m_Presures[i] = FloatList[UIObj.InFloatList[i + 12]];
                m_Voltages[i] = FloatList[UIObj.InFloatList[i + 14]];
            }

            //更新数据文本框
            //温度
            for (int i = 0; i < 12; i++)
            {
                m_StrDate[i].SetText(m_Temperatures[i].ToString() + "℃");
            }

            //压力
            for (int i = 12; i < 14; i++)
            {
                m_StrDate[i].SetText(m_Presures[i - 12].ToString() + "bar");
            }

            //油流状态
            for (int i = 14; i < 16; i++)
            {
                if (m_IsOilStatus[i - 14])
                {
                    m_StrDate[i].SetText("异常");
                }
                else
                {
                    m_StrDate[i].SetText("正常");
                }
            }

            //电压
            for (int i = 16; i < 18; i++)
            {
                m_StrDate[i].SetText(m_Presures[i - 16].ToString() + "V");
            }
        }

        public void DrawOn(Graphics g)
        {
            //底部导航按钮绘制
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //标题 及 数据 文本绘制
            for (int i = 0; i < 18; i++)
            {
                m_StrText[i].OnDraw(g);
                m_StrDate[i].OnDraw(g);
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
                        case 0:
                            break;
                        case 1: //跳转到温度视图
                            append_postCmd(CmdType.ChangePage, 5, 0, 0);
                            break;
                        case 2: //跳转到网络界面
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 3: //跳转到辅助系统
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 4: //跳转到牵引状态
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7: //跳转到辅机测试 
                            append_postCmd(CmdType.ChangePage, 11, 0, 0);
                            break;
                        case 8: //跳转到库内动车 
                            append_postCmd(CmdType.ChangePage, 12, 0, 0);
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
