using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class VoiceSetting : baseClass
    {
        private readonly RectText[] m_GText = new RectText[8];
        private Rectangle m_Rect;
        private Image[] m_Images;
        private readonly Point[] m_Uppoint = new Point[3];
        private readonly Point[] m_Downpoint = new Point[3];

        public const int MaxVoice = 25;

        public const int MaxLight = 31;

        public static int Voice { private set; get; }

        public static int Light { private set; get; }

        private List<Tuple<Point, Point>> m_VoiceItemPoints;

        public override string GetInfo()
        {
            return "音量设置";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            Light = MaxLight;
            Voice = 10;

            InitData();

            const int voiceHeight = 40;
            m_VoiceItemPoints = new List<Tuple<Point, Point>>();
            //m_Rect.X + 80 + i * 4,
            //        m_Rect.Y + 138, m_Rect.X + 80 + i * 4, m_Rect.Y + 156
            for (int i = 0; i < MaxVoice; i++)
            {
                var pointDown = new Point(m_Rect.X + 80 + i*4, m_Rect.Y + 156);
                var pointUp = new Point(m_Rect.X + 80 + i * 4, m_Rect.Y + 156 - i * voiceHeight / MaxVoice);
                m_VoiceItemPoints.Add(new Tuple<Point, Point>(pointUp, pointDown));
            }


            nErrorObjectIndex = -1;
            return true;
        }

        public void InitData()
        {
            m_Rect = new Rectangle(Common2D.RectB[0].Right + 5, Common2D.rectposition.Y + 45, 280, 328);

            m_Images = UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(RecPath, s))).ToArray();

            for (int i = 0; i < 8; i++)
            {
                m_GText[i] = new RectText();
                m_GText[i].SetBkColor(0, 0, 0);
                m_GText[i].SetTextColor(255, 255, 255);
                m_GText[i].SetTextStyle(16, FormatStyle.Center, true, "宋体");
                m_GText[i].SetTextRect(Common2D.RectF[i].X + 2, Common2D.RectF[i].Y + 2,
                    Common2D.RectF[i].Width - 4, Common2D.RectF[i].Height - 4);
            }
            m_GText[0].SetText("");
            m_GText[1].SetImage(ImageResource.小);
            m_GText[2].SetImage(ImageResource.大);
            m_GText[3].SetImage(ImageResource.暗);
            m_GText[4].SetImage(ImageResource.亮);
            m_GText[5].SetText("");
            m_GText[6].SetText("");
            m_GText[7].SetImage(ImageResource.返回);


        }

        public void UpdateValue()
        {
            m_Uppoint[0] = new Point(m_Rect.X + 80 + (Light - 1)*4, m_Rect.Y + 245);
            m_Uppoint[1] = new Point(m_Rect.X + 80 + (Light - 1)*4 - 5, m_Rect.Y + 235);
            m_Uppoint[2] = new Point(m_Rect.X + 80 + (Light - 1)*4 + 5, m_Rect.Y + 235);

            m_Downpoint[0] = new Point(m_Rect.X + 80 + (Light - 1)*4, m_Rect.Y + 267);
            m_Downpoint[1] = new Point(m_Rect.X + 80 + (Light - 1)*4 - 5, m_Rect.Y + 277);
            m_Downpoint[2] = new Point(m_Rect.X + 80 + (Light - 1)*4 + 5, m_Rect.Y + 277);

        }

        public override void paint(Graphics g)
        {
            UpdateValue();
            OnDraw(g);
            ButtonEvent();
        }

        public void OnDraw(Graphics g)
        {
            g.FillRectangle(Common2D.DarkBlueBrush, m_Rect);
            g.FillRectangle(Common2D.LightBlueBrush, m_Rect.X, m_Rect.Y, m_Rect.Width, 60);

            DrawTitle(g);

            DrawVoice(g);

            DrawLight(g);


            for (int i = 0; i < 8; i++)
            {
                m_GText[i].OnDraw(g);
            }
        }

        private void DrawTitle(Graphics g)
        {
            g.DrawString("音量与亮度设置", Common2D.Font14B, Common2D.WhiteBrush, m_Rect.X + 30, m_Rect.Y + 25);
            g.DrawString("音量与亮度", Common2D.Font14B, Common2D.WhiteBrush, m_Rect.X + 3, m_Rect.Y - 25);
        }

        private void DrawVoice(Graphics g)
        {
            g.DrawImage(ImageResource.蓝图2, m_Rect.X + 10, m_Rect.Y + 100, 50, 50);
            g.DrawString(Voice.ToString(), Common2D.Font10B, Common2D.YellowlightBrush, m_Rect.X + 45,
                m_Rect.Y + 138);
            for (int i = 0; i < MaxVoice; i++)
            {
                var location = m_VoiceItemPoints[i];
                g.DrawLine(i < Voice ? Common2D.VoicePen : Common2D.WhitePen2, location.Item1, location.Item2);
            }
        }

        private void DrawLight(Graphics g)
        {
            g.DrawImage(ImageResource.蓝图1, m_Rect.X + 10, m_Rect.Y + 210, 50, 50);
            g.DrawString(Light.ToString(), Common2D.Font10B, Common2D.YellowlightBrush, m_Rect.X + 45,
                m_Rect.Y + 248);
            for (int i = 0; i < MaxLight; i++)
            {
                g.DrawLine(i < Light ? Common2D.YellowlightPen2 : Common2D.WhitePen2, m_Rect.X + 80 + i*4,
                    m_Rect.Y + 247, m_Rect.X + 80 + i*4, m_Rect.Y + 265);
            }

            g.FillPolygon(Common2D.YellowlightBrush, m_Uppoint);
            g.FillPolygon(Common2D.YellowlightBrush, m_Downpoint);
        }

        public void ButtonEvent()
        {
            for (int i = 0; i < 8; i++)
            {
                if (ButtonStatus.m_IsRightButtonUp[i])
                {
                    switch (i)
                    {
                        case 1:
                            if (Voice > 0)
                            {
                                --Voice;
                            }
                            break;
                        case 2:
                            if (Voice < MaxVoice)
                            {
                                ++Voice;
                            }
                            break;
                        case 3:
                            if (Light > 0)
                            {
                                --Light;
                            }

                            break;
                        case 4:
                            if (Light < MaxLight)
                            {
                                ++Light;
                            }
                            break;
                        case 7:
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