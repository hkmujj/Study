using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 功能描述 InputDriverID类 
    /// 司机号设置界面
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-03
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class InputTrainID : baseClass
    {
        #region 私 有 资 源

        /// <summary>
        /// 填充区域范围
        /// </summary>
        private Rectangle m_Rect;

        /// <summary>
        /// 右部菜单名称
        /// </summary>
        private readonly RectText[] m_GText = new RectText[8];

        /// <summary>
        /// 司机号文本框
        /// </summary>
        private RectText m_TrainIDText;

        /// <summary>
        /// 新设置司机号
        /// </summary>
        private string m_NewTrainID = string.Empty;

        //车次号选择界面序号
        private int m_MenuId;

        private bool m_InputRight;

        private readonly Image[] m_Image = new Image[4];


        /// <summary>
        /// 输入判断标志位 true 表示输入数字 false 表示输入字母;
        /// </summary>
        private bool m_IsInputNo = true;

        /// <summary>
        /// 输入索引
        /// </summary>
        private int m_InputIndex;

        /// <summary>
        /// 输入字母
        /// </summary>
        private readonly string[] m_ZiMuStrs = {"ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"};

        /// <summary>
        /// 当前输入的字母
        /// </summary>
        private string m_CurrentInput = string.Empty;

        /// <summary>
        /// 计时器
        /// </summary>
        private int m_JiShiCount;

        /// <summary>
        /// 当前按压按钮
        /// </summary>
        private int m_CurrentEnterIndex = -1;

        #endregion

        #region  重载方法

        public override string GetInfo()
        {
            return "车次号输入界面";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();

            return true;
        }

        public override void paint(Graphics g)
        {
            RefreshData();
            RefreshInputZi();
            OnDraw(g);
            OnButtonEvent();
        }

        #endregion

        #region 私有方法

        private void InitData()
        {
            if (UIObj.ParaList.Count >= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    m_Image[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                }
            }

            m_Rect = new Rectangle(Common2D.RectB[0].Right + 5, Common2D.rectposition.Y + 45, 280, 328);

            for (int i = 0; i < 8; i++)
            {
                m_GText[i] = new RectText();
                m_GText[i].SetBkColor(0, 0, 0);
                m_GText[i].SetTextColor(255, 255, 255);
                m_GText[i].SetTextStyle(16, FormatStyle.Center, true, "宋体");
                m_GText[i].SetTextRect(Common2D.RectF[i].X + 2, Common2D.RectF[i].Y + 2, Common2D.RectF[i].Width - 4,
                    Common2D.RectF[i].Height - 4);
            }
            m_GText[0].SetText(string.Empty);
            m_GText[1].SetText(string.Empty);
            m_GText[2].SetText("");
            m_GText[3].SetText("");
            m_GText[4].SetText("");
            m_GText[5].SetImage(ImageResource.确定);
            m_GText[6].SetImage(ImageResource.删除);
            m_GText[7].SetImage(ImageResource.取消);

            //初始化司机号文本框
            m_TrainIDText = new RectText();
            m_TrainIDText.SetBkColor(69, 170, 255);
            m_TrainIDText.SetTextColor(255, 255, 255);
            m_TrainIDText.SetTextStyle(15, FormatStyle.Center, true, "Arial");
            m_TrainIDText.SetTextRect(m_Rect.X + 100, m_Rect.Y + 105, 120, 40);
        }

        private void RefreshData()
        {
            if (m_MenuId == 1)
            {
                m_GText[6].SetImage(ImageResource.空白按钮);
                m_GText[7].SetImage(ImageResource.空白按钮);
            }
            else if (m_MenuId == 0)
            {
                m_GText[6].SetImage(ImageResource.删除);
                m_GText[7].SetImage(ImageResource.取消);
            }

            m_InputRight = true; // int.TryParse(_newTrainID, out trainId);

            m_TrainIDText.SetText(m_NewTrainID);
        }

        private void OnDraw(Graphics g)
        {
            Common2D.MeunTitle.SetText("车次号");
            Common2D.MeunTitle.OnDraw(g);

            //填充设置区域
            g.FillRectangle(Common2D.DarkBlueBrush, m_Rect);
            g.FillRectangle(Common2D.LightBlueBrush, m_Rect.X, m_Rect.Y, m_Rect.Width, 60);


            for (int index = 0; index < 8; index++)
            {
                m_GText[index].OnDraw(g);
            }

            if (m_MenuId == 0)
            {
                g.DrawString("车次号更改", Common2D.Font14B,
                    Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 20);

                g.DrawString("当前车次号: ", Common2D.Font18B,
                    Common2D.WhiteBrush,
                    m_TrainIDText.m_RectPosition.X - 100, m_TrainIDText.m_RectPosition.Y - 15);

                g.DrawString(DataViewView.m_TheDataView.TrainID,
                    Common2D.Font12B, Common2D.WhiteBrush,
                    m_Rect.X + 60, m_Rect.Y + 145);


                g.DrawString("更改后车次号: ", Common2D.Font18B,
                    Common2D.WhiteBrush,
                    m_TrainIDText.m_RectPosition.X - 100, m_TrainIDText.m_RectPosition.Y + 100);

                g.DrawString(string.IsNullOrEmpty(m_NewTrainID) ? "-" : m_NewTrainID,
                    Common2D.Font14B, Common2D.WhiteBrush,
                    m_Rect.X + 60, m_Rect.Y + 260);

                g.DrawString(m_CurrentInput,
                    Common2D.Font14B, Common2D.WhiteBrush,
                    m_Rect.X + 80, m_Rect.Y + 285);
            }
            else if (m_MenuId == 1)
            {
                g.DrawString("车次号更改提示", Common2D.Font14B,
                    Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 20);

                g.DrawString("您输入的车次号: ", Common2D.Font18B,
                    Common2D.WhiteBrush,
                    m_TrainIDText.m_RectPosition.X - 100, m_TrainIDText.m_RectPosition.Y);

                g.DrawString(string.IsNullOrEmpty(m_NewTrainID) ? "-" : m_NewTrainID,
                    Common2D.Font16B, Common2D.WhiteBrush,
                    m_Rect.X + 60, m_Rect.Y + 160);

                g.DrawString(m_InputRight ? "" : "输入错误", Common2D.Font14B,
                    Common2D.WhiteBrush, m_Rect.X + 160, m_Rect.Y + 280);
            }
        }

        private void OnButtonEvent()
        {
            #region 响应 F1至F8按钮事件

            for (int index = 0; index < 8; index++)
            {
                if (ButtonStatus.m_IsRightButtonUp[index])
                {
                    switch (index)
                    {
                        case 5:
                            if (m_MenuId == 0)
                            {
                                if (!m_InputRight)
                                {
                                    append_postCmd(CmdType.ChangePage, 2, 0, 0);
                                }
                                else
                                    m_MenuId = 1;
                            }
                            else if (m_MenuId == 1)
                            {
                                //确定按钮更新车次号
                                if (m_InputRight)
                                {
                                    DataViewView.m_TheDataView.TrainID = m_NewTrainID;
                                    DataViewView.m_TheDataView.UpdateTime(this.CurrenTime());
                                }

                                append_postCmd(CmdType.ChangePage, 2, 0, 0);
                                m_NewTrainID = string.Empty;
                                m_MenuId = 0;
                            }
                            break;
                        case 6:
                            if (m_MenuId == 0)
                            {
                                //删除最后一位
                                if (m_NewTrainID.Length > 0)
                                    m_NewTrainID = m_NewTrainID.Remove(m_NewTrainID.Length - 1);
                            }
                            break;
                        case 7:
                            if (m_MenuId == 0)
                            {
                                //取消直接返回数据界面
                                m_NewTrainID = string.Empty;
                                append_postCmd(CmdType.ChangePage, 2, 0, 0);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            #endregion

            #region 响应底部按钮事件

            for (int index = 0; index < 11; index++)
            {
                if (ButtonStatus.m_IsBottomButtonUp[index])
                {
                    if (index == 10)
                    {
                        m_IsInputNo = !m_IsInputNo;
                    }
                    else
                    {
                        RefreshSettingDriverID(index);
                    }

                }
            }

            #endregion
        }

        /// <summary>
        /// 更新司机号
        /// </summary>
        /// <param name="index"></param>
        private void RefreshSettingDriverID(int index)
        {
            //车次号最多为5位
            if (m_NewTrainID.Length > 5)
            {
                return;
            }

            //输入数字模式
            if (m_IsInputNo)
            {
                if (index < 9)
                {
                    m_NewTrainID += (index + 1).ToString();
                }
                else if (index == 9)
                {
                    m_NewTrainID += "0";
                }
            }
            else //输入字母模式
            {
                if (index >= 1 && index <= 8) //输入的键位字母键
                {
                    if (index != m_CurrentEnterIndex)
                    {
                        m_CurrentEnterIndex = index;
                        m_InputIndex = 0;
                    }
                    InPutZiMu(index);
                }
            }

        }

        /// <summary>
        /// 输入字母
        /// </summary>
        /// <param name="index"></param>
        private void InPutZiMu(int index)
        {
            m_CurrentInput = GetStringByIndex(m_InputIndex, m_ZiMuStrs[index - 1]);
            m_JiShiCount = 0;
            m_InputIndex++;
            if (index == 6 || index == 8)
            {
                if (m_InputIndex >= 4)
                {
                    m_InputIndex = 0;
                }
            }
            else
            {
                if (m_InputIndex >= 3)
                {
                    m_InputIndex = 0;
                }
            }
        }

        private string GetStringByIndex(int index, string str)
        {
            if (index < 0 || index >= str.Length)
            {
                return string.Empty;
            }

            char[] strChars = str.ToCharArray();

            return strChars[index].ToString();

        }

        /// <summary>
        /// 刷新字母
        /// </summary>
        private void RefreshInputZi()
        {
            m_JiShiCount++;
            if (m_IsInputNo)
            {
                m_JiShiCount = 0;
                m_CurrentInput = string.Empty;
                return;
            }
            if (m_JiShiCount == 8)
            {
                m_NewTrainID += m_CurrentInput;
                m_CurrentInput = string.Empty;
                m_JiShiCount = 0;
            }
        }

        #endregion
    }
}
