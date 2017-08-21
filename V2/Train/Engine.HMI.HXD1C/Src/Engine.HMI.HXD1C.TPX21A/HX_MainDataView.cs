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
    class HX_MainDataView : baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10];//底部数字按钮是否按下
        private readonly Rectangle[] m_ImgRect = new Rectangle[6];//显示受电弓 主断路器 车顶隔离开关等状态的图片信息绘制区域
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>();//界面中需要的界面元素
        private Rectangle[] m_DisconnectorRect = new Rectangle[2];//显示主断路器的柱状区域
        private readonly bool[] m_IsCurrentCollector1 = new bool[3];//受电弓1状态
        private readonly bool[] m_IsCurrentCollector2 = new bool[3];//受电弓2状态
        private readonly bool[] m_IsSeparateDisjunctor1 = new bool[2];//车顶隔离开关1
        private readonly bool[] m_IsSeparateDisjunctor2 = new bool[2];//车顶隔离开关2
        private readonly bool[] m_IsDisconnector = new bool[4];//主断路器状态
        private readonly bool[] m_IsTrainModel=new bool[4];//机车模式
        private readonly string[] m_StrTrainModel = new string[4] { "正常操作", "库内动车", "辅机测试", "定速模式" };
        private readonly string[] m_StrVcmStatus = { "主控", "从控", "断开" };//VCM的三种显示状态

        private HxRectText m_OperateText;//显示操作端 的状态的文本框
        private readonly HxRectText[] m_VcmText = new HxRectText[2];//显示VCM1 VCM2 的状态的文本框
        private readonly HxRectText[] m_ModemText = new HxRectText[5];//显示 机车模式 机车节点 司机室的文本框
        private readonly HxRectText[] m_MVBText = new HxRectText[4];//显示MVB WTB 状态的文本框 

        private readonly bool[] m_IsVcm1 = new bool[3];
        private readonly bool[] m_IsVcm2 = new bool[3];//VCM1 VCM2 的主从关系
        private readonly bool[] m_IsOperators = new bool[2];//操作端
        private readonly bool[] m_IsDriverRoom = new bool[2];//激活司机室
        private readonly bool[] m_IsMvbs = new bool[4];//MVB WTB 线的状态
        private readonly int[] m_TrainPoints = new int[3];//机车节点

        public static int m_MainSegmentNum=0;//主断分断次数计数器
        private bool m_CountStatus = false;
        
        public override string GetInfo()
        {
            return "主要数据视图";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            //加载图片
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Image image = Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
                m_ImageList.Add(i, image);
            }
            nErrorObjectIndex = -1;
            return true;
        }

        public void InitData()
        {
            //显示受电弓 主断路器 车顶隔离开关等状态的图片信息绘制区域初始化
            for (int i = 0; i < 4;i++ )
            {
                if (i % 2 == 0)
                {
                    m_ImgRect[i] = new Rectangle(HxCommon.Recposition.X + 300, HxCommon.Recposition.Y + (i / 2) * 63+50, 100, 60);
                }
                else
                {
                    m_ImgRect[i] = new Rectangle(HxCommon.Recposition.X + 403, HxCommon.Recposition.Y + (i / 2) * 63+50, 100, 60);
                }
            }

            for (int i = 4; i < 6; i++)
            {
                if (i % 2 == 0)
                {
                    m_ImgRect[i] = new Rectangle(HxCommon.Recposition.X + 300, HxCommon.Recposition.Y + (i / 2) * 63 + 50, 100, 40);
                }
                else
                {
                    m_ImgRect[i] = new Rectangle(HxCommon.Recposition.X + 403, HxCommon.Recposition.Y + (i / 2) * 63 + 50, 100, 40);
                }
            }
            
            #region 显示各种状态的文本框的初始化
            //操作端
            m_OperateText = new HxRectText();
            m_OperateText.SetBkColor(0, 0, 0);
            m_OperateText.SetDrawFrm(true);
            m_OperateText.SetLinePen(255, 255, 255, 2);
            m_OperateText.SetTextColor(255, 255, 255);
            m_OperateText.SetTextStyle(12, FormatStyle.Center, true, "宋体");
            m_OperateText.SetTextRect(m_ImgRect[4].X, m_ImgRect[4].Bottom + 3, 203, 33);

            //VCM
            for (int i = 0; i < 2;i++ )
            {
                m_VcmText[i] = new HxRectText();
                m_VcmText[i].SetBkColor(0, 0, 0);
                m_VcmText[i].SetDrawFrm(true);
                m_VcmText[i].SetLinePen(255, 255, 255, 2);
                m_VcmText[i].SetTextColor(255, 255, 255);
                m_VcmText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                if (i == 0)
                {
                    m_VcmText[i].SetTextRect(m_OperateText.m_RectPosition.X, m_OperateText.m_RectPosition.Bottom + 3, 80, 35);
                }
                else
                {
                    m_VcmText[i].SetTextRect(m_OperateText.m_RectPosition.X + 123, m_OperateText.m_RectPosition.Bottom + 3,80, 35);
                }
            }

            //模式信息
            for (int i = 0; i < 5; i++ )
            {
                m_ModemText[i] = new HxRectText();
                m_ModemText[i].SetBkColor(0, 0, 0);
                m_ModemText[i].SetDrawFrm(true);
                m_ModemText[i].SetLinePen(255, 255, 255, 2);
                m_ModemText[i].SetTextColor(255, 255, 255);
                m_ModemText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");

                if (i==0)
                {
                     m_ModemText[i].SetTextRect(m_VcmText[0].m_RectPosition.X, m_VcmText[0].m_RectPosition.Bottom + 3, 203, 34);
                }
                else if (i > 0 && i < 4)
                {
                    m_ModemText[i].SetTextRect(m_ModemText[0].m_RectPosition.X + 67 * (i-1), m_ModemText[0].m_RectPosition.Bottom + 3, 63, 34);
                }
                else
                {
                    m_ModemText[i].SetTextRect(m_ModemText[1].m_RectPosition.X , m_ModemText[1].m_RectPosition.Bottom + 3, 203, 34);
                }
            }

            //MVB WTA
            for (int i = 0; i < 4;i++ )
            {
               m_MVBText[i] = new HxRectText();
               m_MVBText[i].SetBkColor(0, 0, 0);
               m_MVBText[i].SetDrawFrm(true);
               m_MVBText[i].SetLinePen(255, 255, 255, 2);
               m_MVBText[i].SetTextColor(255, 255, 255);
               m_MVBText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");

               if (i < 2)
               {
                   m_MVBText[i].SetTextRect(m_ModemText[4].m_RectPosition.X + 103 * i, m_ModemText[4].m_RectPosition.Bottom + 3, 100, 34);
               }
               else
               {
                   m_MVBText[i].SetTextRect(m_MVBText[0].m_RectPosition.X + 103 * (i-2), m_MVBText[0].m_RectPosition.Bottom + 3, 100, 34);
               }
            }
            #endregion


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
            HxCommon.HTitle.SetText("主要数据");

            HxCommon.ButtonText[0].SetText("牵引数据");
            HxCommon.ButtonText[1].SetText("温度");
            HxCommon.ButtonText[2].SetText("网络");
            HxCommon.ButtonText[3].SetText("辅助系统");
            HxCommon.ButtonText[4].SetText("工作状态 ");
            HxCommon.ButtonText[5].SetText("无线重联");
            HxCommon.ButtonText[6].SetText(" ");
            HxCommon.ButtonText[7].SetText("辅机测试");
            HxCommon.ButtonText[8].SetText("库内动车");
            HxCommon.ButtonText[9].SetText("主界面");

            for (int i = 0; i < 10; i++)
            {
                    HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                    HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
            }

            #region 各状态信息Bool 量的读入
            //受电弓1
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector1[i] = BoolList[UIObj.InBoolList[i]];
            }

            //受电弓2
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector2[i] = BoolList[UIObj.InBoolList[i + 3]];
            }

            //车顶隔离开关1
            for (int i = 0; i < 2;i++ )
            {
                m_IsSeparateDisjunctor1[i] = BoolList[UIObj.InBoolList[i + 6]];
            }

            //车顶隔离开关2
            for (int i = 0; i < 2; i++)
            {
                m_IsSeparateDisjunctor2[i] = BoolList[UIObj.InBoolList[i + 8]];
            }

            //主断路器
            for (int i = 0; i < 4;i++ )
            {
                m_IsDisconnector[i] = BoolList[UIObj.InBoolList[i + 10]];
            }

            //计算主断分断次数
            if ((m_CountStatus!=m_IsDisconnector[3]))
            {
                m_CountStatus = m_IsDisconnector[3];
                if (m_IsDisconnector[3])
                {
                    m_MainSegmentNum++;
                }
            }

            //底部数字按钮
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i + 14]];
            }

            //VCM1
            for (int i = 0; i < 3; i++)
            { 
                m_IsVcm1[i]=BoolList[UIObj.InBoolList[i+38]];
            }

            //VCM2
            for (int i = 0; i < 3; i++)
            {
                m_IsVcm2[i] = BoolList[UIObj.InBoolList[i + 41]];
            }

            for (int i = 0; i < 3; i++)
            {
                if (m_IsVcm1[i])
                {
                    m_VcmText[0].SetText(m_StrVcmStatus[i]);
                    break;
                }
                m_VcmText[0].SetText(" ");
            }
            for (int i = 0; i < 3; i++)
            {
                if (m_IsVcm2[i])
                {
                    m_VcmText[1].SetText(m_StrVcmStatus[i]);
                    break;    
                }
                m_VcmText[1].SetText(" ");
            }
            //if (isVcm)//VCM1为从 VCM2 为主
            //{
            //    vcmText[0].SetText("从");
            //    vcmText[1].SetText("主");
            //}
            //else //VCM1为主 VCM2 为从
            //{
            //    vcmText[0].SetText("主");
            //    vcmText[1].SetText("从");
            //}

            //操作端
            for (int i = 0; i < 2; i++)
            {
                m_IsOperators[i] = BoolList[UIObj.InBoolList[25 + i]];
            }

            if (m_IsOperators[0])//操作端1
            {
                m_OperateText.SetText("司机室1占用");
            }
            else if (m_IsOperators[1])//操作端2
            {
                m_OperateText.SetText("司机室2占用");
            }
            else
            {
                m_OperateText.SetText("");
            }

            if (m_IsDriverRoom[0])//司机室1处于激活状态
            {
                m_ModemText[4].SetText("1");
            }
            else if (m_IsDriverRoom[1])//司机室2处于激活状态
            {
                m_ModemText[4].SetText("2");
            }
            else
            {
                m_ModemText[4].SetText("无");
            }

            //司机室
            for (int i = 0; i < 2; i++)
            {
                m_IsDriverRoom[i] = BoolList[UIObj.InBoolList[27 + i]];
            }

            //MVB WTB
            for (int i = 0; i < 4; i++)
            {
                m_IsMvbs[i] = BoolList[UIObj.InBoolList[29 + i]];

                if (!m_IsMvbs[i])
                {
                    m_MVBText[i].SetText("正常");
                }
                else
                {
                    m_MVBText[i].SetText("故障");
                }
            }

            //机车模式
            for (int i = 0; i < 4; i++)
            {
                m_IsTrainModel[i] = BoolList[UIObj.InBoolList[33 + i]];
                if (m_IsTrainModel[i])
                {
                    m_ModemText[0].SetText(m_StrTrainModel[i]);
                    break;
                }
                m_ModemText[0].SetText("");
            }
            #endregion

            #region 各 Float 量的读入
           
         
            //主断分断次数-----由屏经过计算给出 不用经过接口；
           // MainSegmentNum = Convert.ToInt32(FloatList[UIObj.InFloatList[1]]);

            //机车节点
            for (int i = 0; i < 3;i++ )
            {
                m_TrainPoints[i] = Convert.ToInt32(FloatList[UIObj.InFloatList[i+2]]);
                m_ModemText[i + 1].SetText(m_TrainPoints[i].ToString());
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

            //图形显示区域的矩形框绘制
            for (int i = 0; i < 6;i++ )
            {
                g.DrawRectangle(HxCommon.LinePen1, m_ImgRect[i]);
            }

            //绘制文字信息
            g.DrawString("受电弓1", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[0].X - 90, m_ImgRect[0].Y + 20 );
            g.DrawString("受电弓2", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[1].Right + 5, m_ImgRect[1].Y + 20);
            g.DrawString("车顶隔离开关1", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[2].X -150, m_ImgRect[2].Y + 20);
            g.DrawString("车顶隔离开关2", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[3].Right + 5, m_ImgRect[3].Y + 20);
            g.DrawString("主断", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[4].X - 50, m_ImgRect[4].Y + 15);
            g.DrawString("主断分断次数", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[5].Right + 5 , m_ImgRect[5].Y + 15);
            g.DrawString(m_MainSegmentNum.ToString(), HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[5].X + 25, m_ImgRect[5].Y + 15);

            #region 各图片状态信息更新
            //受电弓绘制1
            for (int i = 0; i < 3; i++)
            {
                if (m_IsCurrentCollector1[i])
                {
                    g.DrawImage(m_ImageList[i], m_ImgRect[0].X + 8, m_ImgRect[0].Y + 8, m_ImgRect[0].Width - 16, m_ImgRect[0].Height - 16);
                    break;
                }
                else
                {
                }
            }

            //受电弓绘制2
            for (int i = 0; i < 3; i++)
            {
                if (m_IsCurrentCollector2[i])
                {
                    g.DrawImage(m_ImageList[i], m_ImgRect[1].X + 8, m_ImgRect[1].Y + 8, m_ImgRect[1].Width - 16, m_ImgRect[1].Height - 16);
                    break;
                }
                else
                {
                }
            }

            //车顶隔离开关1
            for (int i = 0; i < 2; i++)
            {
                if (m_IsSeparateDisjunctor1[i])
                {
                    g.DrawImage(m_ImageList[i + 3], m_ImgRect[2].X + 8, m_ImgRect[2].Y + 8, m_ImgRect[2].Width - 16, m_ImgRect[2].Height - 16);
                    break;
                }
            }

            //车顶隔离开关2
            for (int i = 0; i < 2; i++)
            {
                if (m_IsSeparateDisjunctor2[i])
                {
                    g.DrawImage(m_ImageList[i + 5], m_ImgRect[3].X + 8, m_ImgRect[3].Y + 8, m_ImgRect[3].Width - 16, m_ImgRect[3].Height - 16);
                    break;
                }
            }

            //断路器
            for (int i = 0; i < 4; i++)
            {
                if (m_IsDisconnector[i])
                {
                    g.DrawImage(m_ImageList[i + 7], m_ImgRect[4].X + 8, m_ImgRect[4].Y + 8, m_ImgRect[4].Width - 16, m_ImgRect[4].Height - 16);
                    break;
                }
            }
            #endregion

            #region 各文本信息绘制
            //操作端
            m_OperateText.OnDraw(g);

            //VCM
            for (int i = 0; i < 2;i++ )
            {
                m_VcmText[i].OnDraw(g);
            }

            g.DrawString("VCM1", HxCommon.Font16B, HxCommon.WhiteBrush, m_VcmText[0].m_RectPosition.X - 60, m_VcmText[0].m_RectPosition.Y + 10);
            g.DrawString("VCM2", HxCommon.Font16B, HxCommon.WhiteBrush, m_VcmText[1].m_RectPosition.Right + 5, m_VcmText[1].m_RectPosition.Y + 10);

            //模式信息
            for (int i = 0; i < 5;i++ )
            {
                m_ModemText[i].OnDraw(g);
            }

            g.DrawString("机车模式", HxCommon.Font16B, HxCommon.WhiteBrush, m_ModemText[0].m_RectPosition.X - 94, m_ModemText[0].m_RectPosition.Y + 10);
            g.DrawString("机车节点", HxCommon.Font16B, HxCommon.WhiteBrush, m_ModemText[1].m_RectPosition.X - 94, m_ModemText[1].m_RectPosition.Y + 10);
            g.DrawString("司机室", HxCommon.Font16B, HxCommon.WhiteBrush, m_ModemText[4].m_RectPosition.X - 75, m_ModemText[4].m_RectPosition.Y + 10);

            //MVB WTB
            for (int i = 0; i < 4;i++ )
            {
                m_MVBText[i].OnDraw(g);
            }

            g.DrawString("MVB A线状态", HxCommon.Font16B, HxCommon.WhiteBrush, m_MVBText[0].m_RectPosition.X - 135, m_MVBText[0].m_RectPosition.Y + 10);
            g.DrawString("MVB B线状态", HxCommon.Font16B, HxCommon.WhiteBrush, m_MVBText[1].m_RectPosition.Right + 5, m_MVBText[1].m_RectPosition.Y + 10);
            g.DrawString("WTB A线状态", HxCommon.Font16B, HxCommon.WhiteBrush, m_MVBText[2].m_RectPosition.X - 135, m_MVBText[2].m_RectPosition.Y + 10);
            g.DrawString("WTB B线状态", HxCommon.Font16B, HxCommon.WhiteBrush, m_MVBText[3].m_RectPosition.Right + 5, m_MVBText[3].m_RectPosition.Y + 10);

            #endregion



           
           
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
                            append_postCmd(CmdType.ChangePage, 4, 0, 0);
                            break;
                        case 1://跳转到温度视图
                           append_postCmd(CmdType.ChangePage, 5, 0, 0);
                            break;
                        case 2://跳转到网络界面
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 3://跳转到辅助系统
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 4://跳转到牵引状态
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7://跳转到辅机测试 
                            append_postCmd(CmdType.ChangePage, 11, 0, 0);
                            break;
                        case 8://跳转到库内动车 
                            append_postCmd(CmdType.ChangePage, 12, 0, 0);
                            break;
                        case 9://返回主界面
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 2)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int)(btn - 800)] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 2)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }
    }
}
