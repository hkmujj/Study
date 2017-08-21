using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1C8ConfirmListButton : baseClass
    {
        private Button m_BtnConfirmInfo;
        private Button m_BtnConfirmFault;
        private Button m_BtnListInfo;
        private Button m_BtnListFault;
        private Boolean m_IsInfoShow = true;
        private Boolean m_IsFaultShow = true;
        private Int32 m_InfoCount = 0;
        private Int32 m_FaultCount = 0;
        private Int32 m_ReadTxtID = 0;
        private List<Button> m_ListBtns = new List<Button>();
        private List<Image> m_Images = new List<Image>();

        public override string GetInfo()
        {
            return "确认与列表按钮";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(
                a =>
                {
                    using (FileStream fs = new FileStream(RecPath + "\\" + a, FileMode.Open))
                    {
                        m_Images.Add(Image.FromStream(fs));
                    }
                }
                );

            Button btnOk1 = new Button(
                "确认",
                new RectangleF(7, 464, 100, 44),
                0,
                new ButtonStyle()
                {
                    Background = m_Images[0],
                    DownImage = m_Images[1],
                    FontStyle = new FontStyleEs()
                    {
                        Font = Global.m_FontArial13B,
                        TextBrush = (SolidBrush)Brushes.Black,
                        StringFormat = Global.m_SfCc
                    }
                }
                );
            btnOk1.ClickEvent += btn_ClickEvent;
            m_ListBtns.Add(btnOk1);

            Button btnOk2 = new Button(
                "确认",
                new RectangleF(7, 513, 100, 44),
                1,
                new ButtonStyle()
                {
                    Background = m_Images[0],
                    DownImage = m_Images[1],
                    FontStyle = new FontStyleEs()
                    {
                        Font = Global.m_FontArial13B,
                        TextBrush = (SolidBrush)Brushes.Black,
                        StringFormat = Global.m_SfCc
                    }
                }
                );
            btnOk2.ClickEvent += btn_ClickEvent;
            m_ListBtns.Add(btnOk2);

            Button btnList1 = new Button(
                "列表",
                new RectangleF(588, 464, 100, 44),
                2,
                new ButtonStyle()
                {
                    Background = m_Images[0],
                    DownImage = m_Images[1],
                    FontStyle = new FontStyleEs()
                    {
                        Font = Global.m_FontArial13B,
                        TextBrush = (SolidBrush)Brushes.Black,
                        StringFormat = Global.m_SfCc
                    }
                }
                );
            btnList1.ClickEvent += btn_ClickEvent;
            m_ListBtns.Add(btnList1);

            Button btnList2 = new Button(
                "列表",
                new RectangleF(588, 513, 100, 44),
                3,
                new ButtonStyle()
                {
                    Background = m_Images[0],
                    DownImage = m_Images[1],
                    FontStyle = new FontStyleEs()
                    {
                        Font = Global.m_FontArial13B,
                        TextBrush = (SolidBrush)Brushes.Black,
                        StringFormat = Global.m_SfCc
                    }
                }
                );
            btnList2.ClickEvent += btn_ClickEvent;
            m_ListBtns.Add(btnList2);

            Button btnLanguage = new Button(
                "English",
                new RectangleF(692, 464, 100, 44),
                4,
                new ButtonStyle()
                {
                    Background = m_Images[0],
                    DownImage = m_Images[1],
                    FontStyle = new FontStyleEs()
                    {
                        Font = Global.m_FontArial13B,
                        TextBrush = (SolidBrush)Brushes.Black,
                        StringFormat = Global.m_SfCc
                    }
                }
                );
            btnLanguage.ClickEvent += btn_ClickEvent;
            m_ListBtns.Add(btnLanguage);

            Button btnDriver = new Button(
                "司机 车",
                new RectangleF(692, 513, 100, 44),
                5,
                new ButtonStyle()
                {
                    Background = m_Images[0],
                    DownImage = m_Images[1],
                    FontStyle = new FontStyleEs()
                    {
                        Font = Global.m_FontArial13B,
                        TextBrush = (SolidBrush)Brushes.Black,
                        StringFormat = Global.m_SfCc
                    }
                }
                );
            btnDriver.ClickEvent += btn_ClickEvent;
            m_ListBtns.Add(btnDriver);

            return true;
        }

        private int m_CurrentViewID = 0;

        /// <summary>
        /// 获取到当前运行视图：根据当前视图设置按钮状态
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                m_CurrentViewID = nParaB;
            }
        }

        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0:
                    //this._isFaultShow = false;
                    break;
                case 1:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);

                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    append_postCmd(CmdType.ChangePage, m_CurrentViewID == 104 ? 105 : 104, 0, 0);
                    break;
                default:
                    break;
            }
        }

        public override bool mouseUp(Point point)
        {
            m_ListBtns.ForEach(a => a.MouseUp(point));

            return base.mouseUp(point);
        }

        public override bool mouseDown(Point point)
        {
            m_ListBtns.ForEach(a => a.MouseDown(point));

            return base.mouseDown(point);
        }

        public override void paint(Graphics g)
        {
            m_ListBtns.ForEach(a => a.Paint(g));

            base.paint(g);
        }
    }
}
