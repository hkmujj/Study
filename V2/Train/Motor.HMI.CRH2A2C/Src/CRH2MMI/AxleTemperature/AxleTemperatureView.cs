using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CRH2MMI.Common;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.AxleTemperature
{
    [GksDataType(DataType.isMMIObjectClass)]
    class AxleTemperatureView : CRH2BaseClass
    {
        public int Page
        {
            get { return m_Page; }
            set
            {
                if (m_Page == value || value < 1)
                {
                    return;
                }
                m_Page = value;
            }
        }

        private Point Location;
        private List<CommonInnerControlBase> m_Collection;
        private int m_Page;
        private static Font font12 = new Font("宋体", 14);
        private static Font font14 = new Font("宋体", 16);
        private static StringFormat centerFormat;
        private static StringFormat leftFormat;
        private static StringFormat rightFormat;
        private Dictionary<int, CRH2Button[]> m_ButtonDictionary;
        public override bool Init()
        {
            InitText();
            INitButton();
            tmpIndex = new List<int>();
            for (int i = 35; i < 60; i++)
            {
                tmpIndex.Add(i);
            }
            rom = new Random();
            return true;
        }

        private void INitButton()
        {
            m_ButtonDictionary = new Dictionary<int, CRH2Button[]>();
            m_ButtonDictionary.Add(1,
                new CRH2Button[]{
                new CRH2Button(){   OutLineRectangle = new Rectangle(580, 460, 100, 45),
                                    DownImage = GlobalResource.Instance.BtnDownImage,
                                    UpImage = GlobalResource.Instance.BtnUpImage,
                                    TextColor = Color.White,
                                    Text = "下一页面",
                                    ButtonUpEvent = (sender,args)=>Page++
                                }
            });
            m_ButtonDictionary.Add(2, new CRH2Button[]
            {
                new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(580, 460, 100, 45),
                                    DownImage = GlobalResource.Instance.BtnDownImage,
                                    UpImage = GlobalResource.Instance.BtnUpImage,
                                    TextColor = Color.White,
                                    Text = "下一页面",
                                    ButtonUpEvent = (sender,args)=>Page++
                }, 
                new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(478, 460, 100, 45),
                                    DownImage = GlobalResource.Instance.BtnDownImage,
                                    UpImage = GlobalResource.Instance.BtnUpImage,
                                    TextColor = Color.White,
                                    Text = "上一页面",
                                    ButtonUpEvent = (sender,args)=>Page--
                },
            });
            m_ButtonDictionary.Add(3,
             new CRH2Button[]{
                new CRH2Button(){   OutLineRectangle = new Rectangle(478, 460, 100, 45),
                                    DownImage = GlobalResource.Instance.BtnDownImage,
                                    UpImage = GlobalResource.Instance.BtnUpImage,
                                    TextColor = Color.White,
                                    Text = "上一页面",
                                    ButtonUpEvent = (sender,args)=>Page--
                                }
            });
        }

        private void InitText()
        {
            centerFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            leftFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            };
            rightFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Far
            };
            if (GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig != null)
            {
                Location = GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.Location.Point;
                m_Collection = new List<CommonInnerControlBase>();
                var headerHeight = GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.HeightConfig.Height;
                var contentHeight = GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.ContentWidth;
                var numWight = GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.NumWidht;
                var carWidth = GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.CarWidth;
                m_Collection.Add(new GDIRectText()
                {
                    Text = GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.HeightConfig.Num,
                    OutLineRectangle = new Rectangle(Location.X, Location.Y, numWight, headerHeight),
                    NeedDarwOutline = true,
                    BackColorVisible = false,
                    DrawFont = font14,
                    TextFormat = centerFormat
                });
                m_Collection.Add(new GDIRectText()
                {
                    Text = GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.HeightConfig.Content,
                    OutLineRectangle = new Rectangle(Location.X + numWight, Location.Y, contentHeight, headerHeight),
                    NeedDarwOutline = true,
                    BackColorVisible = false,
                    DrawFont = font14,
                    TextFormat = leftFormat
                });
                m_Collection.Add(new GDIRectText()
                {
                    Text = GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.HeightConfig.DisplayContent,
                    OutLineRectangle =
                        new Rectangle(Location.X + numWight + contentHeight, Location.Y,
                            carWidth * GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.HeightConfig.HeaderCarConfig.Count,
                            headerHeight / 2),
                    NeedDarwOutline = true,
                    BackColorVisible = false,
                    DrawFont = font12,
                    TextFormat = centerFormat
                });
                foreach (var carConfig in GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.HeightConfig.HeaderCarConfig)
                {
                    m_Collection.Add(new GDIRectText()
                    {
                        Text = carConfig.DispalyCarContent,
                        OutLineRectangle =
                            new Rectangle(Location.X + numWight + contentHeight + carWidth * (carConfig.CarNum - 1),
                                Location.Y + headerHeight / 2, carWidth, headerHeight / 2),
                        NeedDarwOutline = true,
                        BackColorVisible = false,
                        DrawFont = font12,
                        TextFormat = centerFormat
                    });
                }
                foreach (var config in GlobalInfo.CurrentCRH2Config.AxleTemperatureDetailConfig.AllContenConfig)
                {
                    m_Collection.Add(new GDIRectText()
                    {
                        Text = config.Num.ToString(),
                        OutLineRectangle =
                            new Rectangle(Location.X, Location.Y + headerHeight + config.Height * (config.Row - 1), numWight,
                                config.Height),
                        NeedDarwOutline = true,
                        BackColorVisible = false,
                        TextFormat = centerFormat,
                        RefreshAction = o => RefreshItemVisible((GDIRectText)o),
                        Tag = config.Page
                    });
                    m_Collection.Add(new GDIRectText()
                    {
                        Text = config.SettingPlace,
                        OutLineRectangle =
                            new Rectangle(Location.X + numWight, Location.Y + headerHeight + config.Height * (config.Row - 1),
                                contentHeight, config.Height),
                        NeedDarwOutline = true,
                        BackColorVisible = false,
                        TextFormat = leftFormat,
                        Tag = config.Page,
                        RefreshAction = o => RefreshItemVisible((GDIRectText)o),
                    });
                    foreach (var carConfig in config.CarConfig)
                    {
                        m_Collection.Add(new GDIRectText()
                        {
                            Text = carConfig.DispalyCarContent,
                            OutLineRectangle =
                                new Rectangle(Location.X + numWight + contentHeight + carWidth * (carConfig.CarNum - 1),
                                    Location.Y + headerHeight + config.Height * (config.Row - 1), carWidth, config.Height),
                            NeedDarwOutline = true,
                            BackColorVisible = false,
                            TextFormat = centerFormat,
                            Tag = new object[] { carConfig.CarNum, carConfig.DispalyCarContent, config.Num, config.Page },
                            RefreshAction = o => RefereshItemValue((GDIRectText)o)
                        });
                    }
                }
            }
        }

        private void RefreshItemVisible(GDIRectText item)
        {
            var name = (int)item.Tag;
            switch (Page)
            {
                case 1:
                    item.Visible = !(name == 2 || name == 3);
                    break;
                case 2:
                    item.Visible = !(name == 3 || name == 1);
                    break;
                case 3:
                    item.Visible = !(name == 1 || name == 2);
                    break;

            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                Page = 1;
            }
        }

        private void RefereshItemValue(GDIRectText item)
        {
            var names = item.Tag as object[];
            if (names == null)
            {
                return;
            }
            var carTag = (int)names[0];
            var num = (int)names[2];
            var page = (int)names[3];
            switch (Page)
            {
                case 1:
                    item.Visible = !(page == 2 || page == 3);
                    break;
                case 2:
                    item.Visible = !(page == 3 || page == 1);
                    break;
                case 3:
                    item.Visible = !(page == 1 || page == 2);
                    break;

            }
            if (temNum < 100)
            {
                temNum++;
                return;
            }

            temNum = 0;
            item.Text = tmpIndex[rom.Next(0, 14)].ToString();
        }

        private int temNum;
        private Random rom;
        private List<int> tmpIndex;
        public override void paint(Graphics dcGs)
        {
            m_Collection.ForEach(e => e.OnPaint(dcGs));
            foreach (var button in m_ButtonDictionary[Page])
            {
                button.OnDraw(dcGs);
            }
        }

        protected override bool OnMouseUp(Point point)
        {
            foreach (var button in m_ButtonDictionary[Page])
            {
                button.OnMouseUp(point);
            }
            return true;
        }
    }
}