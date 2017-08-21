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
    public class InputDriverID : baseClass
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
        private RectText m_DriverIDText;

        /// <summary>
        /// 新设置司机号
        /// </summary>
        private string m_NewDriverID = string.Empty;

        //司机号选择界面序号
        private int m_MenuId;

        private bool m_InputRight;
        private int m_DriverId;

        #endregion

        #region  重载方法

        public override string GetInfo()
        {
            return "司机号输入界面";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            RefreshData();
            OnDraw(dcGs);
            OnButtonEvent();
        }

        #endregion

        #region 私有方法

        private void InitData()
        {

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
            m_DriverIDText = new RectText();
            m_DriverIDText.SetBkColor(69, 170, 255);
            m_DriverIDText.SetTextColor(255, 255, 255);
            m_DriverIDText.SetTextStyle(15, FormatStyle.Center, true, "Arial");
            m_DriverIDText.SetTextRect(m_Rect.X + 100, m_Rect.Y + 105, 120, 40);
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

            m_InputRight = int.TryParse(m_NewDriverID, out m_DriverId);

            m_DriverIDText.SetText(m_NewDriverID);
        }

        private void OnDraw(Graphics g)
        {
            Common2D.MeunTitle.SetText("司机号");
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
                g.DrawString("司机号更改", Common2D.Font14B,
                    Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 20);

                g.DrawString("当前司机号: ", Common2D.Font18B,
                    Common2D.WhiteBrush,
                    m_DriverIDText.m_RectPosition.X - 100, m_DriverIDText.m_RectPosition.Y - 15);

                g.DrawString(DataViewView.m_TheDataView.DriverID,
                    Common2D.Font12B, Common2D.WhiteBrush,
                    m_Rect.X + 60, m_Rect.Y + 145);


                g.DrawString("更改后司机号: ", Common2D.Font18B,
                    Common2D.WhiteBrush,
                    m_DriverIDText.m_RectPosition.X - 100, m_DriverIDText.m_RectPosition.Y + 100);

                g.DrawString(string.IsNullOrEmpty(m_NewDriverID) ? "-" : m_NewDriverID,
                    Common2D.Font14B, Common2D.WhiteBrush,
                    m_Rect.X + 60, m_Rect.Y + 260);
            }
            else if (m_MenuId == 1)
            {
                g.DrawString("司机号更改提示", Common2D.Font14B,
                    Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 20);

                g.DrawString("您输入的司机号: ", Common2D.Font18B,
                    Common2D.WhiteBrush,
                    m_DriverIDText.m_RectPosition.X - 100, m_DriverIDText.m_RectPosition.Y);

                g.DrawString(string.IsNullOrEmpty(m_NewDriverID) ? "-" : m_NewDriverID,
                    Common2D.Font16B, Common2D.WhiteBrush,
                    m_Rect.X + 60, m_Rect.Y + 160);

                g.DrawString(m_InputRight ? "" : "输入错误", Common2D.Font14B,
                    Common2D.WhiteBrush, m_Rect.X + 160, m_Rect.Y + 280);
            }
            //_driverIDText.OnDraw(g);
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
                                if (m_InputRight)
                                {
                                    //确定按钮更新司机号
                                    DataViewView.m_TheDataView.DriverID = m_NewDriverID;
                                    DataViewView.m_TheDataView.UpdateTime(this.CurrenTime());
                                }
                                append_postCmd(CmdType.ChangePage, 2, 0, 0);

                                m_NewDriverID = string.Empty;
                                m_MenuId = 0;
                            }
                            break;
                        case 6:
                            if (m_MenuId == 0)
                            {
                                //删除最后一位
                                if (m_NewDriverID.Length > 0)
                                    m_NewDriverID = m_NewDriverID.Remove(m_NewDriverID.Length - 1);
                            }
                            break;
                        case 7:
                            if (m_MenuId == 0)
                            {
                                //取消直接返回数据界面
                                m_NewDriverID = string.Empty;
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
                    RefreshSettingDriverID(index);
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
            if (m_NewDriverID.Length < 9)
            {
                if (index < 9)
                {
                    m_NewDriverID += (index + 1).ToString();
                }
                else if (index == 9)
                {
                    m_NewDriverID += "0";
                }
            }
        }

        #endregion

    }
}
